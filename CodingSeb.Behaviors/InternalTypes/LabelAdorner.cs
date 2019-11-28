using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CodingSeb.Behaviors
{
    internal class LabelAdorner : System.Windows.Documents.Adorner
    {
        private readonly Label adornerLabel;
        private readonly Control targetControl;

        public LabelAdorner(UIElement adornedElement, string label, Control targetControl = null) : base(adornedElement)
        {
            this.targetControl = targetControl;

            adornerLabel = new Label
            {
                Content = new AccessText()
                {
                    Text = label,
                },
                Foreground = Brushes.Gray,
                Margin = new Thickness(targetControl.Padding.Left + targetControl.BorderThickness.Left + 2,
                    targetControl.Padding.Top + targetControl.BorderThickness.Top + 1,
                    targetControl.Padding.Right + targetControl.BorderThickness.Right + 2,
                    targetControl.Padding.Bottom + targetControl.BorderThickness.Bottom + 1
                ),
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new Thickness(0),
                Target = targetControl,
            };
        }

        protected override Size MeasureOverride(Size constraint)
        {
            return targetControl.RenderSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            adornerLabel.Arrange(new Rect(finalSize));
            return finalSize;
        }

        protected override Visual GetVisualChild(int index)
        {
            return adornerLabel;
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }
    }
}
