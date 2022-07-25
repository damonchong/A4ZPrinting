using A4ZPrinting.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Fonts
{
  public class ZplFontA : AbstractFont
  {
    private ZplFontA()
    {
      _letter = "A";
      _heightByWidthMatrix = FontPointSize.Font2Dimensions[_letter];
      _interCharGap = 1;
      _types = new string[] { UPPERCASE, LOWERCASE, DESCENDERS };
    }

    public ZplFontA(float dpi, int heightInDots) : this() {
      // dot/inch. This is compulsory and there should be only one default DPI for the font instance created.
      AssignDefaultDPI(dpi);
      HeightInDots = heightInDots;
    }

  }
}
