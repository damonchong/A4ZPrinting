using A4ZPrinting.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Labels
{
  public class TwentyFiveByThirteenMillimeterRoundedCornerPerforated : AbstractRoundedCornerLabel
  {
    public override float WidthInMillimeters { get => 25f; set => throw new NotSupportedException(); }
    public override float HeightInMillimeters { get => 13f; set => throw new NotSupportedException(); }
    public override float CornerRadiusInMillimeters { get => 1f; set => throw new NotSupportedException(); }
    public override bool Perforated { get => true; set => throw new NotSupportedException(); }
    public override ITemplate Template { get; set; }

    public TwentyFiveByThirteenMillimeterRoundedCornerPerforated(ITemplate t)
    {
      Template = t;
      if (!VerifyTemplate())
      {
        throw new ArgumentException("Cannot apply this template as its dimension exceed the label size!");
      }
    }
  }
}
