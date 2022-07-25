using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Fonts
{
  public static class FontPointSize
  {
    // Font point to millimeter
    public static readonly Dictionary<string, float> Point2Millimeter = new Dictionary<string, float>()
    {
      {"1", 0.353f}, {"1.5", 0.529f}, {"2", 0.706f}, {"2.5", 0.882f}, {"3", 1.058f}, {"3.5", 1.235f}, {"4", 1.411f},
      {"4.25", 1.499f}, {"4.5", 1.588f}, {"5", 1.764f}, {"5.5", 1.940f}, {"6", 2.117f}, {"6.5", 2.293f}, {"7", 2.469f}, 
      {"7.5", 2.646f}, {"8", 2.822f}, {"9", 3.175f}, {"10", 3.528f}, {"10.5", 3.704f}, {"11", 3.881f}, {"12", 4.233f}, 
      {"14", 4.939f}, {"15", 5.292f}, {"16", 5.644f}, {"18", 6.350f}, {"20", 7.056f}, {"22", 7.761f}, {"24", 8.467f}, 
      {"26", 9.172f}, {"28", 9.878f}, {"30", 10.583f}, {"32", 11.289f}, {"36", 12.7f}, {"40", 14.111f}, {"42", 14.817f}, 
      {"44", 15.522f}, {"48", 16.933f}, {"54", 19.050f}, {"56", 19.756f}, {"60", 21.167f}, {"66", 23.283f}, {"72", 25.4f}, 
      {"84", 29.633f}, {"88", 31.044f}, {"96", 33.867f}, {"100", 35.278f}, {"108", 38.1f}
    };
    // List of font sizes
    public static readonly List<string> PointSizeList = new List<string>()
    {
      "1", "1.5", "2", "2.5", "3", "3.5", "4", "4.25", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "9", "10", "10.5",
      "11", "12", "14", "15", "16", "18", "20", "22", "24", "26", "28", "30", "32", "36", "40", "42", "44", "48", "54",
      "56", "60", "66", "72", "84", "88", "96", "100", "108"
    };
    // Fonts and their dimensions (heights X width in dots)
    private static readonly int[] eighteenByTen = new int[] {18, 10};
    public static readonly Dictionary<string, int[]> Font2Dimensions = new Dictionary<string, int[]>()
    {
      {"A", new int[]{9, 5}},
      {"B", new int[]{11, 7}},
      {"C", eighteenByTen},
      {"D", eighteenByTen},
      {"E", new int[]{28, 15}},
      {"F", new int[]{26, 13}},
      {"G", new int[]{60, 40}},
      {"H", new int[]{21, 13}},
      {"GS", new int[]{24, 24}},
      {"0", new int[]{15, 12}}
    };
  }
}
