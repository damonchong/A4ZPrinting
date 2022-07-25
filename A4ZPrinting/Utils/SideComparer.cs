using A4ZPrinting.Settings;
using A4ZPrinting.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Utils
{
  public class SideComparer : Comparer<ITemplate>
  {
    private List<Dimensions> compareSides = new List<Dimensions>();

    public SideComparer(params Dimensions[] sides)
    {
      foreach (Dimensions d in sides)
      {
        compareSides.Add(d);
      }
    }

    public override int Compare(ITemplate x, ITemplate y)
    {
      foreach(Dimensions side in compareSides)
      {
        int result = 0;
        if (0 != (result = CompareSides(x, y, side)))
        {
          return result;
        }        
      }

      return 0;
    }

    private int CompareSides(ITemplate x, ITemplate y, Dimensions side)
    {
      switch (side)
      {
        case Dimensions.LEFT:
          return OneSideComparison(x.LeftInMillimeters, y.LeftInMillimeters);
        case Dimensions.RIGHT:
          return OneSideComparison(x.LeftInMillimeters + x.WidthInMillimeters, y.LeftInMillimeters + y.WidthInMillimeters);
        case Dimensions.TOP:
          return OneSideComparison(x.TopInMillimeters, y.TopInMillimeters);
        case Dimensions.BOTTOM:
          return OneSideComparison(x.TopInMillimeters + x.HeightInMillimeters, y.TopInMillimeters + y.HeightInMillimeters);
        default:
          throw new NotImplementedException("This comparison is not yet implemented!");
      }
    }

    private int OneSideComparison(float s1, float s2)
    {
      return s1.CompareTo(s2);
    }
  }
}
