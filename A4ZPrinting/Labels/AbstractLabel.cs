using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A4ZPrinting.Templates;

namespace A4ZPrinting.Labels
{
  public abstract class AbstractLabel : ILabel
  {
    public abstract float WidthInMillimeters { get; set; }
    public abstract float HeightInMillimeters { get; set; }
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
      if (0 <= Template.LeftInMillimeters && 0 <= Template.TopInMillimeters &&
        tmplLeftPlusWidth <= WidthInMillimeters && tmplTopPlusHeight <= HeightInMillimeters)
      {
        return true; // Ok.
      }
      else
      {
        if (Template.LeftInMillimeters <= 0 && Template.TopInMillimeters >= 0 &&
          tmplLeftPlusWidth <= WidthInMillimeters && tmplTopPlusHeight <= HeightInMillimeters)
        {
          return true; // Ok.
        }

        if (Template.LeftInMillimeters >= 0 && tmplTopPlusHeight <= HeightInMillimeters && tmplLeftPlusWidth <= WidthInMillimeters)
        {
          return true;
        }
        return false; // Not ok.
      }
    }
  }
}
