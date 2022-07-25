using A4ZPrinting.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Templates
{
  /**
   * This class implements a code 128 1D barcode using a number of default in order to 
   * estimate the width for the data to be encoded. Defaults as follows:
   * 1) Subset B
   * 2) No mod 10 check digit
   * 3) No UCC case mode    
   */
  public class Code128Field : AbstractFieldTemplate
  {
    // Mod 103 check digit is always on and takes up 1 character.
    private const int MOD_103_CHECK_DIGIT = 1;
    private const string START_CODE_B = @">:";
    private const int NUM_OF_SPACES_BARS_PER_CHAR = 11;
    private const int NUM_OF_SPACES_BARS_FOR_STOP_CHAR = 13;
    private int narrowBarModuleWidth = 0;
    private int totalDotsForData = 0;
    private int _dpmm = 0;
    private string _originalText;
    public Code128Field(float left, float top, float height, string data, int narrowBarModuleWidth) : base(left, top, data)
    {
      this.narrowBarModuleWidth = narrowBarModuleWidth;
      this._heightInMillimeters = height;
      _originalText = data;
      if (!data.StartsWith(@">"))
      {
        // Always a good idea to add the start code if none exists.
        this._fieldText = START_CODE_B + this._fieldText; // Default to subset B.
      }      
    }

    public int NarrowBarModuleWidth { get => narrowBarModuleWidth; }

    public int DotsPerMillimeter { get => _dpmm; set => _dpmm = value; }

    public override object Clone()
    {
      var clone = new Code128Field(LeftInMillimeters, TopInMillimeters, HeightInMillimeters, _originalText, narrowBarModuleWidth);
      clone.DotsPerMillimeter = DotsPerMillimeter;
      return clone;
    }

    public override bool Draw()
    {
      if(_dpmm == 0)
      {
        throw new ArgumentException("The dots per millimeter has not yet been set!");
      }
      int len = CalculateEncodedLength(_fieldText);      
      // Number of characters + check digit for number of characters i.e. len + MOD_103_CHECK_DIGIT 
      int numChars = len + MOD_103_CHECK_DIGIT; 
      // Total number of dots occupied
      totalDotsForData = (numChars * NUM_OF_SPACES_BARS_PER_CHAR + NUM_OF_SPACES_BARS_FOR_STOP_CHAR) * narrowBarModuleWidth;
      _widthInMillimeters = totalDotsForData / _dpmm;
      return true;
    }

    private int CalculateEncodedLength(string data)
    {
      string noCodeData = data;
      List<string> invocationList = new List<string>() { ">9", ">:", ">;", "><", ">0", ">=", ">1", ">2", ">3", ">4", ">5", ">6", ">7", ">8" };
      int count = 0;
      foreach(string code in invocationList)
      {
        // Note: each invocation code is counted as 1 character.
        count += data.CountSubstring(code);
        noCodeData = noCodeData.Replace(code, "");
      }

      return count + noCodeData.Length;
    }
  }
}
