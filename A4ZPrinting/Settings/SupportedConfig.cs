using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static A4ZPrinting.Settings.GlobalConstants;

namespace A4ZPrinting.Settings
{
  public static class SupportedConfig
  {
    public static readonly float DPI_153 = 152.4f;
    public static readonly float DPI_203 = 203.2f;
    public static readonly float DPI_300 = 304.8f;
    public static readonly float DPI_600 = 609.6f;
    public static readonly float[] DPI_LIST = new float[] { DPI_153, DPI_203, DPI_300, DPI_600 };

    public static int DefaultDotsPerMilliMeter(float dpi)
    {
      if (DPI_153 == dpi)
      {
        return (int)Math.Round((DPI_153 / MILLIMETERS_PER_INCH), MidpointRounding.AwayFromZero);
      }
      else if (DPI_203 == dpi)
      {
        return (int)Math.Round((DPI_203 / MILLIMETERS_PER_INCH), MidpointRounding.AwayFromZero);
      }
      else if (DPI_300 == dpi)
      {
        return (int)Math.Round((DPI_300 / MILLIMETERS_PER_INCH), MidpointRounding.AwayFromZero);
      }
      else if (DPI_600 == dpi)
      {
        return (int)Math.Round((DPI_600 / MILLIMETERS_PER_INCH), MidpointRounding.AwayFromZero);
      }

      throw new ArgumentException(String.Format("Unsupported {0} DPI!", dpi));
    }
  }
}
