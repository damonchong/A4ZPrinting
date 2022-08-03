using A4ZPrinting.Fonts;
using A4ZPrinting.Labels;
using A4ZPrinting.PrinterModels;
using A4ZPrinting.Templates;
using A4ZPrinting.Utils;
using SharpZebra;
using SharpZebra.Commands;
using SharpZebra.Printing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A4ZPrinting
{
  public partial class frmPrint : Form
  {
    public frmPrint()
    {
      InitializeComponent();
    }

    private void btnStandard_Click(object sender, EventArgs e)
    {
      // Create a printer model.
      var printerModel = GX430t.GetInstance();
      // Create a 2D barcode.
      var matrixBarcode = new DataMatrixField(2, 3, "0123456789ABCDEF", printerModel.DotPerInch);
      matrixBarcode.Draw(); // This will compute the dimensions for the matrix barcode and allow us to re-use later
      // Create font type A with a 300 DPI and heights of 20 dots.
      var defaultFont = new ZplFontA(printerModel.DotPerInch, 20);
      // Create a text field using the previous matrix barcode to align the left and top dimensions.
      var field = new TextField(matrixBarcode.LeftInMillimeters + matrixBarcode.WidthInMillimeters, matrixBarcode.TopInMillimeters, "Hello, this is");
      // The below is unnecessary as the template instance will initiate a Draw() for all child components within it.
      // field.Draw();
      // Create a template for holding various components
      var template = new StandardHolder(defaultFont);
      template.Add(matrixBarcode);
      template.Add(field);
      field = new TextField(matrixBarcode.LeftInMillimeters + matrixBarcode.WidthInMillimeters, field.TopInMillimeters + field.HeightInMillimeters, "A4ZPrinting!!");
      template.Add(field);
      // Code 128 1D barcode with a 0.5 millimeter padding to the left for better visual effect.
      var c128Barcode = new Code128Field(.5f, matrixBarcode.TopInMillimeters + matrixBarcode.HeightInMillimeters, 2.5f, "0123456789", 2); 
      c128Barcode.DotsPerMillimeter = printerModel.DotPerMillimeter;
      template.Add(c128Barcode);
      // for standard template, the below doesn't do anything
      template.Draw();
      // Create a label
      var label = new OneByPointSevenFiveInchNotPerforated(template);
      printerModel.Label = label;
      // Set as a USB printer.
      var printer = new SharpZebra.Printing.USBPrinter(printerModel);
      // Assign to a print command to generate the actual printing instructions.
      var command = new PrintCommand(printer);
      byte[] asciiBytes = command.GeneratePrintCommands();
      Console.WriteLine(Encoding.UTF8.GetString(asciiBytes));
      // Uncomment the line below to send for actual printing
      //PrintLabel(printer, asciiBytes);
    }
    private void btnLeftJustified_Click(object sender, EventArgs e)
    {
      // Create a printer model.
      var printerModel = GX430t.GetInstance();
      // Create a 2D barcode.
      var matrixBarcode = new DataMatrixField(2, 3, "0123456789ABCDEF", printerModel.DotPerInch);
      matrixBarcode.Draw(); // This will compute the dimensions for the matrix barcode and allow us to re-use later
      // As Font 0 type is scalable, in addition to specifying a 300 DPI, we need to set the font point size i.e. 8.
      var defaultFont = new ZplFont0(printerModel.DotPerInch, "8"); 
      // Create a text field using the previous matrix barcode to align the left and top dimensions.      
      var field = new TextField(matrixBarcode.LeftInMillimeters + matrixBarcode.WidthInMillimeters, matrixBarcode.TopInMillimeters, "Hello, this is");
      // You can override the font type by changing the default font for the text field. Otherwise, it will follow whatever default font of the template it is added to.
      field.DefaultFont = new ZplFontA(printerModel.DotPerInch, 20); 
      // The below is unnecessary as the template instance will initiate a Draw() for all child components within it.
      //field.Draw();
      // Create a template with a default font for holding various components
      var template = new LeftJustifiedHolder(defaultFont);
      template.Add(matrixBarcode);
      template.Add(field);
      // We add 0.01 millimeter to create a slight gap between the next text field and the one before. This is needed because we are storing the bottom edge of the top field
      // and the top edge of the lower field in float variables. When subtracted, you sometimes get a negative -0.000000001 which the program sees as an overlap and throws
      // an error.
      field = new TextField(matrixBarcode.LeftInMillimeters + matrixBarcode.WidthInMillimeters, field.TopInMillimeters + field.HeightInMillimeters + 0.01f, "A4ZPrinting!!");
      template.Add(field);
      // Code 128 1D barcode, note this is shorten as its width will exceed otherwise.
      var c128Barcode = new Code128Field(3, matrixBarcode.TopInMillimeters + matrixBarcode.HeightInMillimeters + 0.02f, 2.5f, "012345678", 2); 
      c128Barcode.DotsPerMillimeter = printerModel.DotPerMillimeter;
      template.Add(c128Barcode);
      // For left justified template, this aligns as well as check for layout errors.
      template.Draw();
      // Create a label
      var label = new OneByPointSevenFiveInchNotPerforated(template);
      printerModel.Label = label;
      // Set as a USB printer.
      var printer = new SharpZebra.Printing.USBPrinter(printerModel);
      // Assign to a print command to generate the actual printing instructions.
      var command = new PrintCommand(printer);
      byte[] asciiBytes = command.GeneratePrintCommands();
      Console.WriteLine(Encoding.UTF8.GetString(asciiBytes));
      // Uncomment the line below to send for actual printing
      //PrintLabel(printer, asciiBytes);
    }
    private void PrintLabel(IZebraPrinter usbPrinter, byte[] asciiBytes)
    {
      usbPrinter.Print(asciiBytes);
    }

    private void btnZD420Standard_Click(object sender, EventArgs e)
    {
      var printerModel = ZD420t.GetInstance();
      var field = new TextField(1, 1, "Hello, this is A4ZPrinting!!");
      var defaultFont = new ZplFont0(printerModel.DotPerInch, "6");
      var template = new StandardHolder(defaultFont);
      template.Add(field);
      template.Draw();
      var label = new TwentyFiveByThirteenMillimeterPerforated(template);
      printerModel.Label = label;
      var printer = new SharpZebra.Printing.USBPrinter(printerModel);
      var command = new PrintCommand(printer);
      byte[] asciiBytes = command.GeneratePrintCommands();
      Console.WriteLine(Encoding.UTF8.GetString(asciiBytes));
      // Uncomment the line below to send for actual printing
      //PrintLabel(printer, asciiBytes);
    }
  }

}
