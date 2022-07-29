using A4ZPrinting.Fonts;
using A4ZPrinting.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Labels
{
  /**
   * This class implements a label that is 1 inch by 0.75 inch with rounded corners and not perforated.
   */
  public class OneByPointSevenFiveInchNotPerforated : AbstractRoundedCornerLabel
  {
    public override float WidthInMillimeters { get => 25.4f; set => throw new NotSupportedException(); }
    public override float HeightInMillimeters { get => 19.1f; set => throw new NotSupportedException(); }
    /* This label is rounded at the 4 corners, starting at 0 will caused an error with no printing performed.
     * so align left and align top must not start at 0. Rounded corners appears to be 2mm by 2mm i.e 23.6 dots
     * by 23.6 dots at 300 DPI. Min should only start at 24 dots. Note the default encoding for 8bit ASCII should 
     * be either 1252 or 28591 (for .NET) and not 850. We will use 1252 as it's supported in both .NET and ZPL.
     */
    public override float CornerRadiusInMillimeters { get => 2f; set => throw new NotSupportedException(); }
    public override bool Perforated { get => false; set => throw new NotSupportedException(); }
    public override ITemplate Template { get; set; }
    public OneByPointSevenFiveInchNotPerforated(ITemplate t)
    {
      Template = t;
      if (!VerifyTemplate())
      {
        throw new ArgumentException("Cannot apply this template as its dimension exceed the label size!");
      }
    }    
  }
}
