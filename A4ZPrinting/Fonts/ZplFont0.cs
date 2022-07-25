using A4ZPrinting.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Fonts
{
  public class ZplFont0 : AbstractScalableFont
  {
    private ZplFont0()
    {
      _letter = "0";
      _heightByWidthMatrix = FontPointSize.Font2Dimensions[_letter];
      _interCharGap = -1; // Proportional
      _types = new string[] { UPPERCASE, LOWERCASE, DESCENDERS };
      _fontFormat = FontFormat.SCALABLE;
    }

    public ZplFont0(float dpi, string pointSize) : this()
    {
      if( !FontPointSize.PointSizeList.Contains(pointSize))
      {
        throw new ArgumentException(String.Format("The point size: {0} is not supported!!", pointSize));
      }

      AssignDefaultDPI(dpi);
      fontPoint = pointSize;
      _heightInDots = (int)(FontPointSize.Point2Millimeter[fontPoint] * (float)_dpmm);
      _widthInDots = WidthFromHeightByDots(_heightInDots);
    }
  }
}
