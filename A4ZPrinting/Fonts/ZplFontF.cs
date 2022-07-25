using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Fonts
{
  public class ZplFontF : AbstractFont
  {
    private ZplFontF()
    {
      _letter = "F";
      _heightByWidthMatrix = FontPointSize.Font2Dimensions[_letter];
      _interCharGap = 3;
      _types = new string[] { UPPERCASE, LOWERCASE, DESCENDERS };
    }

    public ZplFontF(float dpi, int heightInDots) : this()
    {
      AssignDefaultDPI(dpi);
      HeightInDots = heightInDots;
    }
  }
}
