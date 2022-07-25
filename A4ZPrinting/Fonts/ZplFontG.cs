using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Fonts
{
  public class ZplFontG : AbstractFont
  {
    private ZplFontG()
    {
      _letter = "G";
      _heightByWidthMatrix = FontPointSize.Font2Dimensions[_letter];
      _interCharGap = 8;
      _types = new string[] { UPPERCASE, LOWERCASE, DESCENDERS };
    }

    public ZplFontG(float dpi, int heightInDots) : this()
    {
      AssignDefaultDPI(dpi);
      HeightInDots = heightInDots;
    }
  }
}
