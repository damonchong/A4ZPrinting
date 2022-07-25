using A4ZPrinting.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static A4ZPrinting.Settings.SupportedConfig;

namespace A4ZPrinting.Templates
{
  /**
    * This class implements the dimensions for 2D matrix barcode using a setting of BXN,4,200. For 1 char should be 0.16 x 0.16 inch but 
    * tested was about 0.13 x 0.13 inch (3.3 mm). However, if a margin is added to the 2D barcode then it's approximately correct.
    * Note 1: the maximum alphanumeric chars that can be encoded using a 144 by 144 modules is 2335 chars or 1555 8bit ASCII chars as below.
    * Note 2: 8bit ASCII needs Encoding.GetEncoding(1252) or Encoding.GetEncoding(28591) and not the default 850. We will use 1252 as it's 
    * supported in both .NET and ZPL.
    */
  public class DataMatrixField : AbstractFieldTemplate
  {
    public enum Encodings
    {
      NUMERIC = 1,
      ALPHANUMERIC = 2,
      ASCII = 3
    }

    public static readonly int MAX_ALPHANUMERIC_CHARS = 2335;
    public static readonly int MAX_ASCII_CHARS = 1555;
    public static readonly int MAX_NUMERIC_CHARS = 3116;
    private static readonly int[] NUMERIC_CHAR_SIZINGS = new int[] { 6, 10, 16, 24, 36, 44, 60, 72, 88, 124, 172, 228, 288, 348, 408, 560, 736, 912, 1152, 1392, 1632, 2100, 2608, 3116 };
    private static readonly int[] ALPHANUMERIC_CHAR_SIZINGS = new int[] { 3, 6, 10, 16, 25, 31, 43, 52, 64, 91, 127, 169, 214, 259, 304, 418, 550, 682, 862, 1042, 1222, 1573, 1954, 2335 };
    private static readonly int[] ASCII_CHAR_SIZINGS = new int[] { 1, 3, 6, 10, 16, 20, 28, 34, 42, 60, 84, 112, 142, 172, 202, 277, 365, 453, 573, 693, 813, 1047, 1301, 1555 };
    private static readonly Dictionary<int[], float> NUMERIC_SQ_DIMENS = new Dictionary<int[], float>(new IntegerArrayComparer())
    {
      {new int[]{200, 6}, 0.24f}, {new int[]{200, 10}, 0.28f}, {new int[]{200, 16}, 0.32f},
      {new int[]{200, 24}, 0.36f}, {new int[]{200, 36}, 0.40f}, {new int[]{200, 44}, 0.44f},
      {new int[]{200, 60}, 0.48f}, {new int[]{200, 72}, 0.52f}, {new int[]{200, 88}, 0.56f},
      {new int[]{200, 124}, 0.68f}, {new int[]{200, 172}, 0.76f}, {new int[]{200, 228}, 0.84f},
      {new int[]{200, 288}, 0.92f}, {new int[]{200, 348}, 1.00f}, {new int[]{200, 408}, 1.08f},
      {new int[]{200, 560}, 1.32f}, {new int[]{200, 736}, 1.48f}, {new int[]{200, 912}, 1.64f},
      {new int[]{200, 1152}, 1.80f}, {new int[]{200, 1392}, 1.96f}, {new int[]{200, 1632}, 2.12f},
      {new int[]{200, 2100}, 2.44f}, {new int[]{200, 2608}, 2.68f}, {new int[]{200, 3116}, 2.92f},
      {new int[]{300, 6}, 0.16f}, {new int[]{300, 10}, 0.19f}, {new int[]{300, 16}, 0.21f},
      {new int[]{300, 24}, 0.24f}, {new int[]{300, 36}, 0.27f}, {new int[]{300, 44}, 0.29f},
      {new int[]{300, 60}, 0.32f}, {new int[]{300, 72}, 0.35f}, {new int[]{300, 88}, 0.37f},
      {new int[]{300, 124}, 0.45f}, {new int[]{300, 172}, 0.51f}, {new int[]{300, 228}, 0.56f},
      {new int[]{300, 288}, 0.61f}, {new int[]{300, 348}, 0.67f}, {new int[]{300, 408}, 0.72f},
      {new int[]{300, 560}, 0.88f}, {new int[]{300, 736}, 0.99f}, {new int[]{300, 912}, 1.09f},
      {new int[]{300, 1152}, 1.20f}, {new int[]{300, 1392}, 1.31f}, {new int[]{300, 1632}, 1.41f},
      {new int[]{300, 2100}, 1.63f}, {new int[]{300, 2608}, 1.79f}, {new int[]{300, 3116}, 1.95f},
      {new int[]{600, 6}, 0.08f}, {new int[]{600, 10}, 0.09f}, {new int[]{600, 16}, 0.11f},
      {new int[]{600, 24}, 0.12f}, {new int[]{600, 36}, 0.13f}, {new int[]{600, 44}, 0.15f},
      {new int[]{600, 60}, 0.16f}, {new int[]{600, 72}, 0.17f}, {new int[]{600, 88}, 0.19f},
      {new int[]{600, 124}, 0.23f}, {new int[]{600, 172}, 0.25f}, {new int[]{600, 228}, 0.28f},
      {new int[]{600, 288}, 0.31f}, {new int[]{600, 348}, 0.33f}, {new int[]{600, 408}, 0.36f},
      {new int[]{600, 560}, 0.44f}, {new int[]{600, 736}, 0.49f}, {new int[]{600, 912}, 0.55f},
      {new int[]{600, 1152}, 0.60f}, {new int[]{600, 1392}, 0.65f}, {new int[]{600, 1632}, 0.71f},
      {new int[]{600, 2100}, 0.81f}, {new int[]{600, 2608}, 0.89f}, {new int[]{600, 3116}, 0.97f}
    };
    private static readonly Dictionary<int[], float> ALPHANUMERIC_SQ_DIMENS = new Dictionary<int[], float>(new IntegerArrayComparer())
    {
      {new int[]{200, 3}, 0.24f}, {new int[]{200, 6}, 0.28f}, {new int[]{200, 10}, 0.32f},
      {new int[]{200, 16}, 0.36f}, {new int[]{200, 25}, 0.40f}, {new int[]{200, 31}, 0.44f},
      {new int[]{200, 43}, 0.48f}, {new int[]{200, 52}, 0.52f}, {new int[]{200, 64}, 0.56f},
      {new int[]{200, 91}, 0.68f}, {new int[]{200, 127}, 0.76f}, {new int[]{200, 169}, 0.84f},
      {new int[]{200, 214}, 0.92f}, {new int[]{200, 259}, 1.00f}, {new int[]{200, 304}, 1.08f},
      {new int[]{200, 418}, 1.32f}, {new int[]{200, 550}, 1.48f}, {new int[]{200, 682}, 1.64f},
      {new int[]{200, 862}, 1.80f}, {new int[]{200, 1042}, 1.96f}, {new int[]{200, 1222}, 2.12f},
      {new int[]{200, 1573}, 2.44f}, {new int[]{200, 1954}, 2.68f}, {new int[]{200, 2335}, 2.92f},
      {new int[]{300, 3}, 0.16f}, {new int[]{300, 6}, 0.19f}, {new int[]{300, 10}, 0.21f},
      {new int[]{300, 16}, 0.24f}, {new int[]{300, 25}, 0.27f}, {new int[]{300, 31}, 0.29f},
      {new int[]{300, 43}, 0.32f}, {new int[]{300, 52}, 0.35f}, {new int[]{300, 64}, 0.37f},
      {new int[]{300, 91}, 0.45f}, {new int[]{300, 127}, 0.51f}, {new int[]{300, 169}, 0.56f},
      {new int[]{300, 214}, 0.61f}, {new int[]{300, 259}, 0.67f}, {new int[]{300, 304}, 0.72f},
      {new int[]{300, 418}, 0.88f}, {new int[]{300, 550}, 0.99f}, {new int[]{300, 682}, 1.09f},
      {new int[]{300, 862}, 1.20f}, {new int[]{300, 1042}, 1.31f}, {new int[]{300, 1222}, 1.41f},
      {new int[]{300, 1573}, 1.63f}, {new int[]{300, 1954}, 1.79f}, {new int[]{300, 2335}, 1.95f},
      {new int[]{600, 3}, 0.08f}, {new int[]{600, 6}, 0.09f}, {new int[]{600, 10}, 0.11f},
      {new int[]{600, 16}, 0.12f}, {new int[]{600, 25}, 0.13f}, {new int[]{600, 31}, 0.15f},
      {new int[]{600, 43}, 0.16f}, {new int[]{600, 52}, 0.17f}, {new int[]{600, 64}, 0.19f},
      {new int[]{600, 91}, 0.23f}, {new int[]{600, 127}, 0.25f}, {new int[]{600, 169}, 0.28f},
      {new int[]{600, 214}, 0.31f}, {new int[]{600, 259}, 0.33f}, {new int[]{600, 304}, 0.36f},
      {new int[]{600, 418}, 0.44f}, {new int[]{600, 550}, 0.49f}, {new int[]{600, 682}, 0.55f},
      {new int[]{600, 862}, 0.60f}, {new int[]{600, 1042}, 0.65f}, {new int[]{600, 1222}, 0.71f},
      {new int[]{600, 1573}, 0.81f}, {new int[]{600, 1954}, 0.89f}, {new int[]{600, 2335}, 0.97f}
    };
    private static readonly Dictionary<int[], float> ASCII_SQ_DIMENS = new Dictionary<int[], float>(new IntegerArrayComparer())
    {
      {new int[]{200, 1}, 0.24f}, {new int[]{200, 3}, 0.28f}, {new int[]{200, 6}, 0.32f},
      {new int[]{200, 10}, 0.36f}, {new int[]{200, 16}, 0.40f}, {new int[]{200, 20}, 0.44f},
      {new int[]{200, 28}, 0.48f}, {new int[]{200, 34}, 0.52f}, {new int[]{200, 42}, 0.56f},
      {new int[]{200, 60}, 0.68f}, {new int[]{200, 84}, 0.76f}, {new int[]{200, 112}, 0.84f},
      {new int[]{200, 142}, 0.92f}, {new int[]{200, 172}, 1.00f}, {new int[]{200, 202}, 1.08f},
      {new int[]{200, 277}, 1.32f}, {new int[]{200, 365}, 1.48f}, {new int[]{200, 453}, 1.64f},
      {new int[]{200, 573}, 1.80f}, {new int[]{200, 693}, 1.96f}, {new int[]{200, 813}, 2.12f},
      {new int[]{200, 1047}, 2.44f}, {new int[]{200, 1301}, 2.68f}, {new int[]{200, 1555}, 2.92f},
      {new int[]{300, 1}, 0.16f}, {new int[]{300, 3}, 0.19f}, {new int[]{300, 6}, 0.21f},
      {new int[]{300, 10}, 0.24f}, {new int[]{300, 16}, 0.27f}, {new int[]{300, 20}, 0.29f},
      {new int[]{300, 28}, 0.32f}, {new int[]{300, 34}, 0.35f}, {new int[]{300, 42}, 0.37f},
      {new int[]{300, 60}, 0.45f}, {new int[]{300, 84}, 0.51f}, {new int[]{300, 112}, 0.56f},
      {new int[]{300, 142}, 0.61f}, {new int[]{300, 172}, 0.67f}, {new int[]{300, 202}, 0.72f},
      {new int[]{300, 277}, 0.88f}, {new int[]{300, 365}, 0.99f}, {new int[]{300, 453}, 1.09f},
      {new int[]{300, 573}, 1.20f}, {new int[]{300, 693}, 1.31f}, {new int[]{300, 813}, 1.41f},
      {new int[]{300, 1047}, 1.63f}, {new int[]{300, 1301}, 1.79f}, {new int[]{300, 1555}, 1.95f},
      {new int[]{600, 1}, 0.08f}, {new int[]{600, 3}, 0.09f}, {new int[]{600, 6}, 0.11f},
      {new int[]{600, 10}, 0.12f}, {new int[]{600, 16}, 0.13f}, {new int[]{600, 20}, 0.15f},
      {new int[]{600, 28}, 0.16f}, {new int[]{600, 34}, 0.17f}, {new int[]{600, 42}, 0.19f},
      {new int[]{600, 60}, 0.23f}, {new int[]{600, 84}, 0.25f}, {new int[]{600, 112}, 0.28f},
      {new int[]{600, 142}, 0.31f}, {new int[]{600, 172}, 0.33f}, {new int[]{600, 202}, 0.36f},
      {new int[]{600, 277}, 0.44f}, {new int[]{600, 365}, 0.49f}, {new int[]{600, 453}, 0.55f},
      {new int[]{600, 573}, 0.60f}, {new int[]{600, 693}, 0.65f}, {new int[]{600, 813}, 0.71f},
      {new int[]{600, 1047}, 0.81f}, {new int[]{600, 1301}, 0.89f}, {new int[]{600, 1555}, 0.97f}
    };

    private readonly int approxDPI;
    private float _dpi;
    private Encodings encodeType;
    // The above measurements uses 4x4 dots per matrix cells. Changing this will affect the calculation.
    private readonly int dotsPerMatrixCell = 4; 

    public DataMatrixField(float left, float top, string data, float dpi, Encodings type = Encodings.ASCII) : base(left, top, data)
    {
      _dpi = dpi;
      approxDPI = ApproximateDPI(dpi);
      // Note: the encoding should match the data. If the data contains ASCII characters but a numeric encoding was chosen, 
      // the result will be unexpected.
      encodeType = type;
    }

    public int DotsPerMatrixCell { get => dotsPerMatrixCell; }

    private int ApproximateDPI(float dpi)
    {
      // Note this will only work if the argument given to the constructor is the same float reference from SupportedConfig.
      if(dpi == DPI_153)
      {
        throw new NotSupportedException("This configuration is not supported!");
      }
      else if(dpi == DPI_203)
      {
        return 200;
      }
      else if(dpi == DPI_300)
      {
        return 300;
      }
      else if(dpi == DPI_600)
      {
        return 600;
      }
      else
      {
        throw new NotSupportedException("This configuration is not supported either!");
      }
    }

    public override object Clone()
    {
      var clone = new DataMatrixField(LeftInMillimeters, TopInMillimeters, Contents, _dpi, encodeType);
      clone._heightInMillimeters = WidthInMillimeters;
      clone._widthInMillimeters = HeightInMillimeters;
      return clone;
    }

    public override bool Draw()
    {
      if (String.IsNullOrEmpty(_fieldText))
      {
        throw new ArgumentNullException("Empty field or data for matrix barcode!");
      }

      int len = _fieldText.Length;
      int estimatedCharGroup = CharSizeGroup(len, encodeType);
      int[] dimens = new int[] { approxDPI, estimatedCharGroup };
      // Get the estimated inch for the amount of text to be encoded.
      float estInch = EstimateInchForCharSize(dimens, encodeType);
      _widthInMillimeters = estInch * GlobalConstants.MILLIMETERS_PER_INCH;
      _heightInMillimeters = _widthInMillimeters;
      return true;
    }

    public static float EstimateInchForCharSize(int[] dimens, Encodings encodingType)
    {
      switch (encodingType)
      {
        case Encodings.NUMERIC:
          return NUMERIC_SQ_DIMENS[dimens];
        case Encodings.ALPHANUMERIC:
          return ALPHANUMERIC_SQ_DIMENS[dimens];
        case Encodings.ASCII:
          return ASCII_SQ_DIMENS[dimens];
        default:
          throw new NotSupportedException(String.Format("Unsupported encoding type: {0}!", encodingType));
      }
    }

    public static int CharSizeGroup(int charSize, Encodings encodingType)
    {
      switch (encodingType)
      {
        case Encodings.NUMERIC:
          return NextEnclosingSize(charSize, NUMERIC_CHAR_SIZINGS);
        case Encodings.ALPHANUMERIC:
          return NextEnclosingSize(charSize, ALPHANUMERIC_CHAR_SIZINGS);
        case Encodings.ASCII:
          return NextEnclosingSize(charSize, ASCII_CHAR_SIZINGS);
        default:
          throw new NotSupportedException(String.Format("Unsupported encoding type: {0}!", encodingType));
      }
    }

    private static int NextEnclosingSize(int charSize, int[] sizings)
    {
      if (charSize == 0)
      {
        throw new ArgumentOutOfRangeException("Character size is zero!");
      }

      if((null == sizings) || sizings.Length == 0)
      {
        throw new ArgumentOutOfRangeException("The sizings list cannot be empty!");
      }

      int prevSize = 0;
      foreach(int size in sizings){
        if (size < charSize)
        {
          prevSize = size;
          continue;
        }
        else
        {
          if(prevSize < charSize)
          {
            return size;
          }
          else
          {
            throw new ApplicationException("This should not happened!");
          }
        }
      }

      throw new ApplicationException("Total characters exceed the maximum encoding limit!");
    }
    class IntegerArrayComparer : IEqualityComparer<int[]>
    {
      public bool Equals(int[] x, int[] y)
      {
        if (x.Length != y.Length)
        {
          return false;
        }
        for (int i = 0; i < x.Length; i++)
        {
          if (x[i] != y[i])
          {
            return false;
          }
        }
        return true;
      }

      public int GetHashCode(int[] obj)
      {
        int result = 17;
        for (int i = 0; i < obj.Length; i++)
        {
          unchecked
          {
            result = result * 23 + obj[i];
          }
        }
        return result;
      }
    }
  }
}
