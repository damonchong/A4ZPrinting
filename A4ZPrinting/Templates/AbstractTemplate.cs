using A4ZPrinting.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Templates
{
  public abstract class AbstractTemplate : ITemplate
  {    
    protected int _darkness = 100; // Maximum darkness i.e. 100%
    protected float _leftInMillimeters = 0f;
    protected float _topInMillimeters = 0f;
    protected float _heightInMillimeters = 0f;
    protected float _widthInMillimeters = 0f;
    public int DarknessLevel { get => _darkness; set => _darkness = value; }
    public float LeftInMillimeters { get => _leftInMillimeters; set => _leftInMillimeters = value; }
    public float TopInMillimeters { get => _topInMillimeters; set => _topInMillimeters = value; }
    public float HeightInMillimeters { get => _heightInMillimeters; }
    public float WidthInMillimeters { get => _widthInMillimeters; }
    public abstract string Contents { get; }
    public abstract object Clone();
    public abstract bool Draw();
    
  }
}
