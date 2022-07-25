using A4ZPrinting.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Templates
{
  public interface ITemplate : ICloneable
  { 
    // To retrieve the contents of the template.
    string Contents { get; }
    // A convenient method to initialize the template and check if there'll be any issues. 
    bool Draw();
    // Darkness level: 0 to 100 percent
    int DarknessLevel { get; set; }
    // Using millimeters so that it's the same as the print labels, besides, normal humans don't measure with dots or pixels ;-P
    float LeftInMillimeters { get; set; }
    // Using millimeters so that it's the same as the print labels, besides, normal humans don't measure with dots or pixels ;-P
    float TopInMillimeters { get; set; }
    float HeightInMillimeters { get; }
    float WidthInMillimeters { get; }
    //void Add(ITemplate childTemplate);
    //bool Remove(ITemplate childTempolate);
    //ITemplate Get(int index);
    //int Count { get; }
  }
}
