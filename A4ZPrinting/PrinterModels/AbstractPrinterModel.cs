using A4ZPrinting.Labels;
using A4ZPrinting.Templates;
using SharpZebra.Printing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.PrinterModels
{
  public abstract class AbstractPrinterModel : PrinterSettings
  {
    protected ILabel _label;
    protected int _dpmm;
    protected float _dpi;
    protected float _maxDarkness;

    public ILabel Label
    {
      get => _label;
      set
      {
        _label = value;       
        if (_label.Template.DarknessLevel != 0)
        {
          int darknessLevel = Convert.ToInt32(Math.Round((_label.Template.DarknessLevel / 100.0f) * _maxDarkness, MidpointRounding.AwayFromZero));
          Darkness = darknessLevel;
        }

        if (_label.Template is AbstractTextFieldTemplate)
        {
          ((AbstractTextFieldTemplate)_label.Template).DefaultFont.ChangeDPI(_dpi);
        }

        if (_label.Template is AbstractHolderTemplate)
        {
          ((AbstractHolderTemplate)_label.Template).DefaultFont.ChangeDPI(_dpi);
        }
        // Will round down. Possible values: 152, 203, 304, 609.
        Width = (int)(_label.WidthInMillimeters * _dpmm);
      }
    }
    public int DotPerMillimeter { get => _dpmm; }
    public float DotPerInch { get => _dpi; }
  }
}
