using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Fonts
{
  public class ZplFontC : AbstractFont
  {
    private ZplFontC()
    {
      _letter = "C";
      _heightByWidthMatrix = FontPointSize.Font2Dimensions[_letter];
      _interCharGap = 2;
      _types = new string[] { UPPERCASE, LOWERCASE, DESCENDERS };
    }

    public ZplFontC(float dpi, int heightInDots) : this()
    {
      AssignDefaultDPI(dpi);
      HeightInDots = heightInDots;
    }

  }
}
