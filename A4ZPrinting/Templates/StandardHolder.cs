using A4ZPrinting.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Templates
{
  // This is a normal holding template that does not align, justifiy or adjust the child components under it.
  public class StandardHolder : AbstractHolderTemplate
  {
    public StandardHolder(AbstractFont df)
    {
      _defaultFont = df;
    }

    public override string Contents => throw new NotImplementedException();

    public override object Clone()
    {
      throw new NotImplementedException();
    }

    public override bool Draw()
    {
      return true;
    }
  }
}
