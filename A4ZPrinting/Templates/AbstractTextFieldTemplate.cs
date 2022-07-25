using A4ZPrinting.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Templates
{
  public abstract class AbstractTextFieldTemplate : AbstractFieldTemplate
  {
    protected AbstractFont _defaultFont = null;
    public AbstractTextFieldTemplate(float left, float top, string data) : base(left, top, data) { }
    public virtual AbstractFont DefaultFont { get => _defaultFont; set => _defaultFont = value; }
  }
}
