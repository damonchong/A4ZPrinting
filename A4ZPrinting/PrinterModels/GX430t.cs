using A4ZPrinting.Labels;
using A4ZPrinting.PrinterModels;
using A4ZPrinting.Templates;
using SharpZebra.Printing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static A4ZPrinting.Settings.SupportedConfig;

namespace A4ZPrinting.PrinterModels
{
  public class GX430t : AbstractPrinterModel
  {
    private static GX430t _instance;
    private static readonly object _lock = new object();    

    private GX430t()
    {
      // Default dpi for this printer is 12 dots/mm i.e. 304.8 dots for 1 inch or 25.4 mm.
      _dpmm = 12;
      _dpi = DPI_300;
      _maxDarkness = 30f;
    }

    public static GX430t GetInstance()
    {
      if (_instance == null)
      {
        lock (_lock)
        {
          if (_instance == null)
          {
            _instance = new GX430t();
            _instance.Initialize();
          }
        }
      }
      return _instance;
    }
    private void Initialize()
    {
      Id = 1;
      PrinterName = "ZDesigner GX430t";
      PrinterType = 'G';
      PrinterPort = 1;
      AlignLeft = 0;
      AlignTop = 0;
      Darkness = 25; // Max 30
      PrintSpeed = 2; // Supported speeds are 2, 3, 4 inches per second according to specs for GX430t.
      // Default print speed settings
      SlewSpeed = 4; // Assuming it's 4 inches per second by default for this model.
      BackFeedSpeed = 2;      
      MediaType = 'T'; // Thermal transfer media is the default.
    }
  }
}
