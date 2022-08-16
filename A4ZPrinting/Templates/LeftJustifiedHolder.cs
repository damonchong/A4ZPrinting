using A4ZPrinting.Fonts;
using A4ZPrinting.Settings;
using A4ZPrinting.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Templates
{
  public class LeftJustifiedHolder : AbstractHolderTemplate
  {
    /* This is the 2nd alignment if a component straddles 2 or more components to its left i.e. it will shift upwards to align
     * with the topmost component on its left.
     */
    private const Location _2ndAlignPriority = Location.TOP;
    private List<ITemplate> workComponents = new List<ITemplate>();
    
    public LeftJustifiedHolder(AbstractFont df)
    {
      _defaultFont = df; // This will apply to all children if they did not have any default font.
    }

    public override bool Draw()
    {
      AlignTemplateComponents();
      // If alignment is done successfully, we can replace with the working components.
      components.Clear();
      components.AddRange(workComponents);
      return true;
    }

    public override void Add(ITemplate childTemplate, bool readjust=true)
    {
      base.Add(childTemplate, false);
      ITemplate clone = (ITemplate)childTemplate.Clone();
      clone.Draw();
      workComponents.Add(clone);
    }

    /**
* This will align the components in the templates automatically.
*/
    private void AlignTemplateComponents()
    {
      var comparer = new SideComparer(Location.LEFT, _2ndAlignPriority);
      SortedList<ITemplate, float[]> rawList = new SortedList<ITemplate, float[]>(comparer);
      foreach (ITemplate component in workComponents)
      {
        rawList.Add(component, new float[] { component.TopInMillimeters, component.HeightInMillimeters });
      }

      var alignedList = new SortedList<ITemplate, float[]>(comparer);
      // Use the most left component's dimension to align the other components
      var leftmost = rawList.First().Key;
      float[] leftSides = rawList.First().Value;
      rawList.Remove(leftmost);
      SortedList<ITemplate, float[]> nonAlignedList = new SortedList<ITemplate, float[]>(rawList, comparer);

      alignedList.Add(leftmost, leftSides);
      while (nonAlignedList.Any())
      {
        // Iterate from top most components downward and try to justify left if possible. 
        JustifyLeftFromTopDown(leftmost, nonAlignedList, alignedList);
      }

      // If the re-alignment succeed, best to re-calculate the total dimensions again.
      _leftInMillimeters = leftmost.LeftInMillimeters;
      _topInMillimeters = leftmost.TopInMillimeters;
      _heightInMillimeters = 0f;
      _widthInMillimeters = 0f;
      foreach(ITemplate component in alignedList.Keys)
      {
        ReadjustDimensions(component);
      }
    }

    private void JustifyLeftFromTopDown(ITemplate leftmost, SortedList<ITemplate, float[]> nonAlignedList, SortedList<ITemplate, float[]> alignedList)
    {
      var component = nonAlignedList.First().Key;
      ITemplate blockParent = null;
      List<ITemplate> blockParentList = new List<ITemplate>();

      foreach (ITemplate alignPart in alignedList.Keys)
      {
        if ((component.TopInMillimeters + component.HeightInMillimeters + _defaultComponentGap) <= alignPart.TopInMillimeters ||
          (alignPart.TopInMillimeters + alignPart.HeightInMillimeters + _defaultComponentGap) <= component.TopInMillimeters)
        {
          // No crash.
        }
        else
        {
          blockParentList.Add(alignPart);
        }
      }

      if (blockParentList.Any())
      {
        blockParent = GetFattestParent(blockParentList);
        // move as close to the right side of the blocking parent.
        component.LeftInMillimeters = (blockParent.LeftInMillimeters + blockParent.WidthInMillimeters + _defaultComponentGap);
      }
      else
      {
        component.LeftInMillimeters = leftmost.LeftInMillimeters; // Align to the left.
      }

      nonAlignedList.Remove(component);
      alignedList.Add(component, new float[] { component.TopInMillimeters, component.HeightInMillimeters });
    }

    private ITemplate GetFattestParent(List<ITemplate> blockParentList)
    {
      ITemplate fattest = blockParentList[0];
      blockParentList.Remove(fattest);
      foreach(ITemplate parent in blockParentList)
      {
        if( parent.WidthInMillimeters > fattest.WidthInMillimeters)
        {
          fattest = parent;
        }
      }

      return fattest;
    }

    public override object Clone()
    {
      throw new NotImplementedException();
    }

    public override string Contents => throw new NotImplementedException();
  }
}
