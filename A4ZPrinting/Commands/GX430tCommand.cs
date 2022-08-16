using A4ZPrinting.Fonts;
using A4ZPrinting.Labels;
using A4ZPrinting.Templates;
using A4ZPrinting.Utils;
using SharpZebra;
using SharpZebra.Commands;
using SharpZebra.Printing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting
{
  public class GX430tCommand : ZPLCommands
  {
    private IZebraPrinter printer;
    private GX430t printerModel;
    public GX430tCommand(IZebraPrinter p)
    {
      this.printer = p;
      if( printer.Settings is GX430t)
      {
        printerModel = (GX430t)printer.Settings;
      }
    }

    protected static string FixTilde(string text)
    {
      if (string.IsNullOrEmpty(text)) return text;
      if (!text.Contains("~"))
        return $"^FD{text}^FS";
      if (text.Contains("_"))
        throw new ApplicationException("Tilde character is not supported with underscore in same command");
      return $"^FH^FD{text.Replace("~", "_7e")}^FS";
    }

    public static byte[] TextWrite(int left, int top, ElementDrawRotation rotation, AbstractFont font, int heightInDots, string text = "", int codepage = 850)
    {
      int widthInDots = font.WidthFromHeightByDots(heightInDots);
      return string.IsNullOrEmpty(text)
          ? new byte[0]
          : Encoding.GetEncoding(codepage)
              .GetBytes($"^FO{left},{top}^A{font.Letter}{(char)rotation},{heightInDots},{widthInDots}{FixTilde(text)}");
    }

    public static new byte[] DataMatrixWrite(int left, int top, ElementDrawRotation rotation, int height, string text, QualityLevel qualityLevel = QualityLevel.ECC_200, AspectRatio aspectRatio = AspectRatio.SQUARE)
    {
      var rotationValue = (char)rotation;
      var qualityLevelValue = (int)qualityLevel;
      var aspectRatioValue = (int)aspectRatio;

      return Encoding.GetEncoding(1252).GetBytes($"^FO{left},{top}^BX{rotationValue}, {height},{qualityLevelValue},,,,,{aspectRatioValue},^FD{text}^FS");
    }

    public static byte[] Code128Write(int left, int top, int height, ElementDrawRotation rotation, int narrowBarModuleWidth, bool readable, string barcodeData)
    {
      var encodedReadable = readable ? "Y" : "N";
      return Encoding.GetEncoding(850).GetBytes(
                        $"^FO{left},{top}^BY{narrowBarModuleWidth}^BC{(char)rotation},{height},{encodedReadable}^FD{barcodeData}^FS");
    }

    public static byte[] AlignWrite(int left, int top, ElementDrawRotation rotation, AbstractFont font, int heightInDots, int width, Alignment alignment, 
      int maxLines, int lineSpacing, int indentSize, string text = "", int codepage = 1252)
    {
      int widthInDots = font.WidthFromHeightByDots(heightInDots);
      var alignmentValue = (char)alignment;
      return string.IsNullOrEmpty(text)
          ? new byte[0]
          : Encoding.GetEncoding(codepage)
              .GetBytes($"^FO{left},{top}^A{font.Letter}{(char)rotation},{heightInDots},{widthInDots}^FB{width},{maxLines},{lineSpacing},{alignmentValue},{indentSize}^FD{text}\\&^FS");
    }

    public byte[] ClearPrinter()
    {
      //^MMa: Media type: (T for thermal transfer media or D for direct thermal media, default T value thermal transfer).
      //^PRp,s,b: print rate or speed (print, slew, backfeed) (2,3,4).  
      //~TA###: Tear off position (must be 3 digits). ^LS####: Left shift.  ^LHx,y: Label home. ^SD##x: Set Darkness (00 to 30). ^PWx: Label width
      //^XA^MMT^PR4,12,12~TA000^LS-20^LH0,12~SD19^PW750
      //_stringCounter = 0;
      if (printerModel.CurrentLabel.Perforated)
      {
        return Encoding.GetEncoding(850).GetBytes(string.Format("^XA^MMT^PR{0},{0},{0}~TA{1:000}^LH{2},{3}~SD{4:00}^PW{5}", printerModel.PrintSpeed,
          printerModel.AlignTearOff, printerModel.AlignLeft, printerModel.AlignTop, printerModel.Darkness, printerModel.Width));
      }
      else
      {
        return Encoding.GetEncoding(850).GetBytes(string.Format("^XA^PR{0},{0},{0}^LH{1},{2}~SD{3:00}^PW{4}", printerModel.PrintSpeed,
          printerModel.AlignLeft, printerModel.AlignTop, printerModel.Darkness, printerModel.Width));
      }
    }

    private List<AbstractFieldTemplate> ExtractFieldsFromTemplate(ITemplate holderTemplate)
    {
      List<AbstractFieldTemplate> fieldList = new List<AbstractFieldTemplate>();
      if (holderTemplate is AbstractHolderTemplate)
      {
        ExtractAllFields(holderTemplate as AbstractHolderTemplate, fieldList);
      }
      else if (holderTemplate is AbstractFieldTemplate)
      {
        fieldList.Add(holderTemplate as AbstractFieldTemplate);
      }
      else
      {
        throw new ArgumentException("Unexpected instance. Neither a field nor a field holder!");
      }

      return fieldList;
    }

    public byte[] GeneratePrintCommands()
    {
      byte[] asciiBytes = ClearPrinter();
      //^FT250,600^A0B,28,28^FB600,1,0,C^FH\^FDTEXT_TO_REPLACE^FS
      ILabel label = printerModel.CurrentLabel;
      ITemplate holderTemplate = label.Template;
      List<AbstractFieldTemplate> fieldList = ExtractFieldsFromTemplate(holderTemplate);
      
      byte[] nextAsciiBytes = new byte[0];
      int size = fieldList.Count;
      for (int i = 0; i < size; i++)
      {
        AbstractFieldTemplate t = fieldList[i];
        int left = (int)Math.Round(t.LeftInMillimeters * printerModel.DotPerMillimeter, MidpointRounding.AwayFromZero);
        int top = (int)Math.Round(t.TopInMillimeters * printerModel.DotPerMillimeter, MidpointRounding.AwayFromZero);
        int height = (int)Math.Round(t.HeightInMillimeters * printerModel.DotPerMillimeter, MidpointRounding.AwayFromZero);
        if (t is TextField)
        {
          if (holderTemplate is StandardHolder)
          {
            nextAsciiBytes = GX430tCommand.TextWrite(left, top, t.Orientation, ((TextField)t).DefaultFont, height, t.Contents);
          }
          else if(holderTemplate is LeftJustifiedHolder)
          {
            // * 1.2 to ensure width set for ^FB is sufficiently wide.
            int width = (int)Math.Round(t.WidthInMillimeters * printerModel.DotPerMillimeter * 1.15, MidpointRounding.AwayFromZero);
            nextAsciiBytes = GX430tCommand.AlignWrite(left, top, t.Orientation, ((TextField)t).DefaultFont, height, width,
              Alignment.LEFT, 1, 0, 0, t.Contents);
          }
        }
        else if(t is DataMatrixField)
        {
          // For barcode there's no alignment ZPL commands
          nextAsciiBytes = GX430tCommand.DataMatrixWrite(left, top, t.Orientation, ((DataMatrixField)t).DotsPerMatrixCell, t.Contents);          
        }
        else if(t is Code128Field)
        {
          nextAsciiBytes = GX430tCommand.Code128Write(left, top, height, t.Orientation, ((Code128Field)t).NarrowBarModuleWidth, true, t.Contents);
        }
        asciiBytes = Utilities.Combine(asciiBytes, nextAsciiBytes);
      }

      nextAsciiBytes = GX430tCommand.PrintBuffer();
      asciiBytes = Utilities.Combine(asciiBytes, nextAsciiBytes);
      return asciiBytes;
    }
    private void ExtractAllFields(AbstractHolderTemplate holder, List<AbstractFieldTemplate> fieldList)
    {
      List<AbstractHolderTemplate> childHolders = new List<AbstractHolderTemplate>();
      while (holder.MoveNext())
      {
        if(holder.Current is AbstractFieldTemplate)
        {
          fieldList.Add(holder.Current as AbstractFieldTemplate);
        }
        else if(holder.Current is AbstractHolderTemplate)
        {
          childHolders.Add(holder.Current as AbstractHolderTemplate);
        }
        else
        {
          throw new ArgumentException("Unexpected instance! Not a field or field holder!!");
        }
      }

      foreach(AbstractHolderTemplate h in childHolders)
      {
        ExtractAllFields(h, fieldList);
      }
    }
  }
}
