using SharpZebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Templates
{
  public abstract class AbstractFieldTemplate : AbstractTemplate, ICloneable
  {
    protected string _fieldText;
    protected int _fieldRotation = 0;

    public AbstractFieldTemplate(float left, float top, string data)
    {
      if (left < 0)
      {
        throw new ArgumentException(String.Format("The gap from left: {0} cannot be negative", left));
      }
      _leftInMillimeters = left;
      if (top < 0)
      {
        throw new ArgumentException(String.Format("The gap from top: {0} cannot be negative", top));
      }
      _topInMillimeters = top;
      if (data == null)
      {
        throw new ArgumentException("The text for the field cannot be null!");
      }
      _fieldText = data;
    }

    public virtual void RotateClockwise90Degrees(int times)
    {
      int rotations = times % 4;
      _fieldRotation += rotations;
    }
    public ElementDrawRotation Orientation
    {
      get
      {
        switch (_fieldRotation)
        {
          case 0:
            return ElementDrawRotation.NO_ROTATION;
          case 1:
            return ElementDrawRotation.ROTATE_90_DEGREES;
          case 2:
            return ElementDrawRotation.ROTATE_180_DEGREES;
          default:
            return ElementDrawRotation.ROTATE_270_DEGREES;
        }
      }
    }

    public override string Contents { get => _fieldText; }
  }
}
