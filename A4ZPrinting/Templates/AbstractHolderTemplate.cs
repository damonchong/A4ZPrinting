using A4ZPrinting.Fonts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4ZPrinting.Templates
{
  /**
   * This abstract class holds other template class instances.
   */
  public abstract class AbstractHolderTemplate : AbstractTemplate, IEnumerator<ITemplate>
  {
    protected List<ITemplate> components = new List<ITemplate>();
    protected int _idx = -1;
    protected AbstractFont _defaultFont;
    protected float _defaultComponentGap = 0f; // Default gap between child components is 0 millimeter.

    public ITemplate Current { get => components[_idx]; }

    object IEnumerator.Current { get => components[_idx]; }

    public virtual AbstractFont DefaultFont { get => _defaultFont; set => _defaultFont = value; }

    public virtual float DefaultComponentGap { get => _defaultComponentGap; set => _defaultComponentGap = value; }

    public virtual void Add(ITemplate childTemplate, bool readjust=true)
    {
      if (childTemplate != null)
      {
        if (childTemplate is TextField)
        {
          var childText = childTemplate as TextField;
          if( childText.DefaultFont == null)
          {
            childText.DefaultFont = _defaultFont; // Follow the holding template's default font.
          }
        }

        if (!childTemplate.Draw())
        {
          throw new ApplicationException("Unable to initialize the child component!");
        }

        float targetLeftPlusWidth = (childTemplate.LeftInMillimeters == _leftInMillimeters ? 0 : _defaultComponentGap) + childTemplate.LeftInMillimeters + childTemplate.WidthInMillimeters;
        float targetTopPlusHeight = (childTemplate.TopInMillimeters == _topInMillimeters ? 0 : _defaultComponentGap) + childTemplate.TopInMillimeters + childTemplate.HeightInMillimeters;
        // Check that newly added template do not overlap or intersect others.
        foreach (ITemplate component in components)
        {
          float comLeftPlusWidth = (component.LeftInMillimeters == _leftInMillimeters ? 0 : _defaultComponentGap)
            + component.LeftInMillimeters + component.WidthInMillimeters;
          float comTopPlusHeight = (component.TopInMillimeters == _topInMillimeters ? 0 : _defaultComponentGap)
            + component.TopInMillimeters + component.HeightInMillimeters;
          if (targetLeftPlusWidth <= component.LeftInMillimeters || comLeftPlusWidth <= childTemplate.LeftInMillimeters ||
            targetTopPlusHeight <= component.TopInMillimeters || comTopPlusHeight <= childTemplate.TopInMillimeters)
          {
            // No intersection and minimum gap maintained
          }
          else
          {
            // DeMorgan's law means we have an overlapping of these 2 components.
            throw new ArgumentException("Could not add the child component as it overlaps with an existing component!");
          }
        }

        if (readjust)
        {
          ReadjustDimensions(childTemplate);
        }
        components.Add(childTemplate);

      }
    }

    protected void ReadjustDimensions(ITemplate newComponent)
    {      
      float targetLeftPlusWidthPlusGap = (newComponent.LeftInMillimeters == _leftInMillimeters ? 0 : _defaultComponentGap) + newComponent.LeftInMillimeters + newComponent.WidthInMillimeters;
      float targetTopPlusHeightPlusGap = (newComponent.TopInMillimeters == _topInMillimeters ? 0 : _defaultComponentGap) + newComponent.TopInMillimeters + newComponent.HeightInMillimeters;

      // This captures the latest maximum width. This may change when a new child component is added.
      if (targetLeftPlusWidthPlusGap <= _leftInMillimeters)
      {
        _widthInMillimeters += (targetLeftPlusWidthPlusGap + (_leftInMillimeters - targetLeftPlusWidthPlusGap) + _defaultComponentGap);
      }
      else
      {
        float currentLeftPlusWidthPlusGap = _leftInMillimeters + _widthInMillimeters + _defaultComponentGap;
        if (currentLeftPlusWidthPlusGap <= (newComponent.LeftInMillimeters))
        {
          _widthInMillimeters += (newComponent.LeftInMillimeters - currentLeftPlusWidthPlusGap) + newComponent.WidthInMillimeters;
        }
        else
        {
          if (targetLeftPlusWidthPlusGap > currentLeftPlusWidthPlusGap)
          {
            _widthInMillimeters += targetLeftPlusWidthPlusGap - currentLeftPlusWidthPlusGap;
          }
        }
      }
      
      // This captures the latest maximum height. This may change with a new child component added.
      if (targetTopPlusHeightPlusGap <= _topInMillimeters)
      {
        _heightInMillimeters += (targetTopPlusHeightPlusGap + (_topInMillimeters - targetTopPlusHeightPlusGap) + _defaultComponentGap);
      }
      else
      {
        float currentTopPlusHeightPlusGap = _topInMillimeters + _heightInMillimeters + _defaultComponentGap;
        if (currentTopPlusHeightPlusGap <= newComponent.TopInMillimeters)
        {
          _heightInMillimeters += (newComponent.TopInMillimeters - currentTopPlusHeightPlusGap) + newComponent.HeightInMillimeters;
        }
        else
        {
          if (targetTopPlusHeightPlusGap > currentTopPlusHeightPlusGap)
          {
            _heightInMillimeters += targetTopPlusHeightPlusGap - currentTopPlusHeightPlusGap;
          }
        }
      }

      if (!components.Any())
      {
        _leftInMillimeters = newComponent.LeftInMillimeters;
        _topInMillimeters = newComponent.TopInMillimeters;
      }

      // This captures the latest left as a result of all the child components added.
      if (newComponent.LeftInMillimeters < _leftInMillimeters)
      {
        _leftInMillimeters = newComponent.LeftInMillimeters + _defaultComponentGap;
      }
      // This captures the latest top as a result of all the child components added.
      if (newComponent.TopInMillimeters < _topInMillimeters)
      {
        _topInMillimeters = newComponent.TopInMillimeters + _defaultComponentGap;
      }
    }

    public virtual bool Remove(ITemplate childTempolate)
    {      
      return components.Remove(childTempolate);
    }

    public virtual ITemplate Get(int index)
    {
      if (components.Count <= (index + 1))
      {
        throw new IndexOutOfRangeException(String.Format("Index exceeded the list size: {0}", components.Count));
      }
      return components[index];
    }

    public virtual int Count
    {
      get => components.Count();
    }

    public void Dispose()
    {
      components.Clear();
    }

    public bool MoveNext()
    {
      _idx += 1;
      return (_idx < Count);
    }

    public void Reset()
    {
      _idx = -1;
    }
  }
}
