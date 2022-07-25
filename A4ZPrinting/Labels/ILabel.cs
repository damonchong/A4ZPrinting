using A4ZPrinting.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Labels
{
  public interface ILabel
  {
    float WidthInMillimeters { get; set; }
    float HeightInMillimeters { get; set; }
    float CornerRadiusInMillimeters { get; set;  }
    bool Perforated { get; set; }
    ITemplate Template { get; set; }
  }
}
