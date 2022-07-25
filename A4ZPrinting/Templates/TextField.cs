using A4ZPrinting.Fonts;
using A4ZPrinting.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Templates
{
  public class TextField : AbstractTextFieldTemplate
  {
    public TextField(float left, float top, string data) : base(left, top, data) { }

    public override object Clone()
    {
      var clone = new TextField(LeftInMillimeters, TopInMillimeters, Contents);
      clone._widthInMillimeters = WidthInMillimeters;
      clone._heightInMillimeters = HeightInMillimeters;
      clone._defaultFont = DefaultFont;
      return clone;
    }

    public override bool Draw()
    {
      if (_defaultFont.FormatType == FontFormat.BIT_MAPPED)
      {
        int len = _fieldText.Length;
        _widthInMillimeters = (float)((len * _defaultFont.WidthInDots) + ((len - 1) * _defaultFont.InterCharacterGap)) / (float)_defaultFont.DotsPerMillimeter;
        if (_widthInMillimeters < 0)
        {
          throw new ArgumentOutOfRangeException("Negative width detected!");
        }

        _heightInMillimeters = (float)_defaultFont.HeightInDots / (float)_defaultFont.DotsPerMillimeter;
        if (_heightInMillimeters < 0)
        {
          throw new ArgumentOutOfRangeException("Negative height detected!");
        }
      }
      else
      {
        // Scalable font.
        var scalableFont = (AbstractScalableFont)_defaultFont;
        float fps = float.Parse(scalableFont.FontPoint);
        var measureStr = new MeasureString(fps);
        _widthInMillimeters = measureStr.Measure(_fieldText, scalableFont.DotsPerInch) / (float)scalableFont.DotsPerMillimeter;
        if (_widthInMillimeters < 0)
        {
          throw new ArgumentOutOfRangeException("Negative width detected!");
        }
        _heightInMillimeters = (float)scalableFont.HeightInDots / (float)scalableFont.DotsPerMillimeter;
        if (_heightInMillimeters < 0)
        {
          throw new ArgumentOutOfRangeException("Negative height detected!");
        }
      }

      return true;
    }  

  }
}
