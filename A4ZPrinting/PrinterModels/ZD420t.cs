using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static A4ZPrinting.Settings.SupportedConfig;

namespace A4ZPrinting.PrinterModels
{
  public class ZD420t : AbstractPrinterModel
  {
    private static ZD420t _instance;
    private static readonly object _lock = new object();
    private ZD420t()
    {
      _dpmm = 12;
      _dpi = DPI_300;
      _maxDarkness = 30f;
    }

    public static ZD420t GetInstance()
    {
      if (_instance == null)
      {
        lock (_lock)
        {
          if (_instance == null)
          {
            _instance = new ZD420t();
            _instance.Initialize();
          }
        }
      }
      return _instance;
    }

    private void Initialize()
    {
      Id = 1;
      PrinterName = "ZDesigner ZD420-300dpi ZPL";
      PrinterType = 'Z';
      PrinterPort = 1;
      AlignLeft = 0;
      AlignTop = 0;
      Darkness = 15; // Max 30
      PrintSpeed = 4; // Max print speed 4 inch per sec for 300 DPI and 6 inch per sec for 203 DPI
      // Default print speed settings
      SlewSpeed = 4; 
      BackFeedSpeed = 3;
      MediaType = 'T'; // Thermal transfer media is the default.
    }
  }
}
