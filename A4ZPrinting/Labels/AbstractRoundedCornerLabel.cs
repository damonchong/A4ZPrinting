using A4ZPrinting.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Labels
{
  public abstract class AbstractRoundedCornerLabel : ILabel
  {
    public abstract float WidthInMillimeters { get; set; }
    public abstract float HeightInMillimeters { get; set; }
    public abstract float CornerRadiusInMillimeters { get; set; }
    public abstract bool Perforated { get; set; }
    public abstract ITemplate Template { get; set; }
    /**
     * Verification to see if the template setup is ok. This will fail if the template dimensions exceed the physical label's
     * dimensions.
     */
    public virtual bool VerifyTemplate()
    {
      float tmplLeftPlusWidth = Template.LeftInMillimeters + Template.WidthInMillimeters;
      float tmplTopPlusHeight = Template.TopInMillimeters + Template.HeightInMillimeters;
      // Due to the 4 curved corners, a gap of 2mm top, bottom, left and right will be intentionally left blank.
      if (CornerRadiusInMillimeters <= Template.LeftInMillimeters &&
        CornerRadiusInMillimeters <= Template.TopInMillimeters &&
        tmplLeftPlusWidth <= (WidthInMillimeters - (CornerRadiusInMillimeters * 2)) &&
        tmplTopPlusHeight <= (HeightInMillimeters - (CornerRadiusInMillimeters * 2)))
      {
        return true; // Ok.
      }
      else
      {
        if (Template.LeftInMillimeters <= CornerRadiusInMillimeters && Template.TopInMillimeters >= CornerRadiusInMillimeters &&
          tmplLeftPlusWidth <= WidthInMillimeters && tmplTopPlusHeight <= (HeightInMillimeters - (CornerRadiusInMillimeters * 2)))
        {
          return true; // Ok.
        }

        if (Template.LeftInMillimeters >= CornerRadiusInMillimeters && tmplTopPlusHeight <= HeightInMillimeters &&
          tmplLeftPlusWidth <= (WidthInMillimeters - (CornerRadiusInMillimeters * 2)))
        {
          return true;
        }
        return false; // Not ok.
      }
    }
  }
}
