using A4ZPrinting.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static A4ZPrinting.Settings.GlobalConstants;
using static A4ZPrinting.Settings.SupportedConfig;

namespace A4ZPrinting.Fonts
{
  public abstract class AbstractFont : IFont
  {
    public const string UPPERCASE = "U";
    public const string LOWERCASE = "L";
    public const string DESCENDERS = "D";
    public const string OCR_B = "OCR-B";
    public const string OCR_A = "OCR-A";
    public const string SYMBOL = "SYMBOL";

    // The letter representing the Zebra font type e.g. 0 for scalable font type.
    protected string _letter;
    // Default DPI needed as subsequent calculation will fail otherwise.
    protected float _dpi = 0f; 
    protected int _dpmm;
    // Height and width matrix in dots for each font type.
    protected int[] _heightByWidthMatrix = null;
    // Actual height desired, setting this automatically set the width
    protected int _heightInDots;
    // Actual width desired, setting this automatically set the height
    protected int _widthInDots;
    protected int _interCharGap;
    protected string[] _types;
    protected FontFormat _fontFormat = FontFormat.BIT_MAPPED; // Default fixed size font.
    public string Letter { get => _letter; protected set => _letter = value; }    
    public int InterCharacterGap { get => _interCharGap; set => _interCharGap = value; }
    public int DotsPerMillimeter { get => _dpmm; }
    public float DotsPerInch { get => _dpi; }
    public virtual int HeightInDots
    {
      get => _heightInDots;
      set
      {
        if (value < _heightByWidthMatrix[0])
        {
          throw new ArgumentOutOfRangeException(String.Format("Minimal height in dots must be {0} or more!", _heightByWidthMatrix[0]));
        }
        // Fonts sizing should be in multiples to work best. e.g if 9 by 5 then use 18 by 10 or 27 by 15
        _heightInDots = value;
        _widthInDots = WidthFromHeightByDots(_heightInDots);
      }
    }
    protected void AssignDefaultDPI(float dpi)
    {
      _dpi = dpi;
      if (!SupportedConfig.DPI_LIST.Contains(_dpi))
      {
        throw new ArgumentException(String.Format("Unsupported dots per millimeter value: {0}!", _dpi.ToString()));
      }
      _dpmm = DefaultDotsPerMilliMeter(_dpi);
    }
    public virtual int WidthInDots
    {
      get => _widthInDots;
      set
      {
        if(value < _heightByWidthMatrix[1])
        {
          throw new ArgumentOutOfRangeException(String.Format("Minimal width in dots must be {0} or more!", _heightByWidthMatrix[1]));
        }
        _widthInDots = value;
        _heightInDots = HeightFromWidthByDots(_widthInDots);
      }
    }
    public FontFormat FormatType { get => _fontFormat; }
    public bool ContainsUppercase() { return _types.Contains(UPPERCASE); }
    public bool ContainsLowercase() { return _types.Contains(LOWERCASE); }
    public bool ContainsDescenders() { return _types.Contains(DESCENDERS); }
    public bool IsOcrB() { return _types.Contains(OCR_B); }
    public bool IsOcrA() { return _types.Contains(OCR_A); }
    public bool isSymbol() { return _types.Contains(SYMBOL); }
    public virtual void ChangeDPI(float dpi)
    {
      AssignDefaultDPI(dpi);
    }
    public int WidthFromHeightByDots(int heightInDots)
    {
      if (_heightInDots == 0)
      {
        throw new ArgumentNullException("Default height and width ratio is not set properly as either one or both dimensions are missing!");
      }

      return (int)(((float)heightInDots / _heightByWidthMatrix[0]) * _heightByWidthMatrix[1]);
    }

    public int HeightFromWidthByDots(int widthInDots)
    {
      if (_widthInDots == 0)
      {
        throw new ArgumentNullException("Default height and width ratio is not set properly as either one or both dimensions are missing!");
      }

      return (int)((widthInDots / _heightByWidthMatrix[1]) * _heightByWidthMatrix[0]);
    }
  }
}
