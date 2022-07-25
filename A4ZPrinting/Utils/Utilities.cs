using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Utils
{
  public static class Utilities
  {
    public static byte[] Combine(byte[] first, byte[] second)
    {
      byte[] ret = new byte[first.Length + second.Length];
      Buffer.BlockCopy(first, 0, ret, 0, first.Length);
      Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
      return ret;
    }

    public static int CountSubstring(this string text, string value)
    {
      int count = 0, minIndex = text.IndexOf(value, 0);
      while (minIndex != -1)
      {
        minIndex = text.IndexOf(value, minIndex + value.Length);
        count++;
      }
      return count;
    }
  }
}
