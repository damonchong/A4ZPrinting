using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Fonts
{
  public enum FontFormat
  {
    BIT_MAPPED,
    SCALABLE,
  }
  public interface IFont
  {
    bool ContainsUppercase();
    bool ContainsLowercase();
    bool ContainsDescenders();
    bool IsOcrB();
    bool IsOcrA();
    bool isSymbol();
    int InterCharacterGap { get; set; }
    int DotsPerMillimeter { get; }
    float DotsPerInch { get; }
    int HeightInDots { get; set; }
    int WidthInDots { get; set; }
    FontFormat FormatType { get; }
    int WidthFromHeightByDots(int heightInDots);
    int HeightFromWidthByDots(int widthInDots);
    void ChangeDPI(float dpi);
    string Letter { get; }
  }
}
