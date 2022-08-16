using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Settings
{
  public enum Location
  {
    LEFT, RIGHT, TOP, BOTTOM, CENTER, UPPER_LEFT_CORNER, UPPER_RIGHT_CORNER, BOTTOM_LEFT_CORNER, BOTTOM_RIGHT_CORNER
  };
  public static class GlobalConstants
  {
    public const float MILLIMETERS_PER_INCH = 25.4f;
    
  }
}
