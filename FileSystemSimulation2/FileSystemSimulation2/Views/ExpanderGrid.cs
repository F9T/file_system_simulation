using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

/// <summary>
/// source : http://www.cenito.se/2014/11/gridsplitter-expander-dont-work-together/
/// </summary>
namespace FileSystemSimulation2.Views
{
    public class ExpanderGrid : Grid
    {
        private readonly Dictionary<UIElement, double> mElementSizes;

        public ExpanderGrid()
        {
            mElementSizes = new Dictionary<UIElement, double>();
        }

        protected override Size ArrangeOverride(Size _arrangeSize)
        {
            foreach (var child in Children.OfType<Expander>().Where(_expander => !_expander.IsExpanded))
            {
                if (child.ExpandDirection == ExpandDirection.Down || child.ExpandDirection == ExpandDirection.Up)
                {
                    var row = GetRow(child);
                    if (!RowDefinitions.Any())
                        continue;

                    RowDefinitions[row].Height = new GridLength(0, GridUnitType.Auto);
                }
                else
                {
                    var column = GetColumn(child);
                    if (!ColumnDefinitions.Any())
                        continue;

                    ColumnDefinitions[column].Width = new GridLength(0, GridUnitType.Auto);
                }
            }
            return base.ArrangeOverride(_arrangeSize);
        }

        protected override void OnChildDesiredSizeChanged(UIElement _child)
        {
            if (_child is Expander expander)
            {
                bool verticalExpansion = expander.ExpandDirection == ExpandDirection.Down ||
                                         expander.ExpandDirection == ExpandDirection.Up;

                int row = GetRow(expander);
                int column = GetColumn(expander);

                if (expander.IsExpanded)
                {
                    if (!mElementSizes.TryGetValue(_child, out var oldSize))
                    {
                        oldSize = 1;
                    }
                    var gridLength = new GridLength(oldSize, GridUnitType.Star);
                    if (verticalExpansion)
                    {
                        RowDefinitions[row].Height = gridLength;
                    }
                    else
                    {
                        ColumnDefinitions[column].Width = gridLength;
                    }
                }
                else
                {
                    var gridLength = new GridLength(0, GridUnitType.Auto);
                    if (verticalExpansion)
                    {
                        mElementSizes[_child] = RowDefinitions[row].Height.Value;
                        RowDefinitions[row].Height = gridLength;
                    }
                    else
                    {
                        mElementSizes[_child] = ColumnDefinitions[column].Width.Value;
                        ColumnDefinitions[column].Width = gridLength;
                    }
                }
            }
            base.OnChildDesiredSizeChanged(_child);
        }
    }
}
