using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Fonts
{
  public abstract class AbstractScalableFont : AbstractFont
  {
    protected string fontPoint;

    public string FontPoint { get => fontPoint; }

    public override int HeightInDots
    {
      get => _heightInDots;
      //set => throw new NotSupportedException("Cannot set the heights in dots for this scalable font!");
    }

    public override int WidthInDots
    {
      get => _widthInDots;
      //set => throw new NotSupportedException("Cannot set the width in dots for scalable font!");
    }
  }
}
