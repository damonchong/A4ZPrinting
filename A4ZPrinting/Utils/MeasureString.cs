using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A4ZPrinting.Utils
{
  /**
   * Credits and source code from https://gist.github.com/roccivic/9f8a6f44a169c3de3504. Extended for further functionality.
   */
  public class MeasureString : Form, IDisposable
  {
    /**
     * The Font 0 in Zebra is quite similar to "Helvetica" unfortunately Microsoft doesn't come with this font. The next closest
     * font is Arial. We use this as a proxy for calculating font 0 instead. It's a poor substitution but beats nothing.
     */
    private const string FONT_NAME = "Arial"; 
    private Font font;
    private int _dpi;
    public MeasureString(float fontSize)
    {
      font = new Font(
          new FontFamily(FONT_NAME),
          fontSize,
          FontStyle.Regular,
          GraphicsUnit.Pixel
      );
      int comparison = string.Compare(
          FONT_NAME,
          font.Name,
          StringComparison.InvariantCultureIgnoreCase
      );
      if (comparison != 0)
      {
        throw new ApplicationException("Unsupported font: " + FONT_NAME);
      }
      _dpi = this.DeviceDpi;
    }
    // Returns the width of the string provided in pixels or dots.
    public int Measure(string str)
    {
      return TextRenderer.MeasureText(str, font).Width;
    }
    // Returns the width of string adjusted for DPI of Zebra printer.
    public float Measure(string str, float dpi)
    {
      int nativeWidth = Measure(str);
      return ((float)nativeWidth / _dpi) * dpi;
    }
    public new void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
    public virtual new void Dispose(bool disposing)
    {
      if (disposing)
      {
        font.Dispose();
      }

      base.Dispose(disposing);
    }
  }
}
