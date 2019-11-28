using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace CodingSeb.Behaviors
{
    public class TextBoxWatermarkBehavior : Behavior<TextBox>
    {
        private LabelAdorner adorner;
        private WeakPropertyChangeNotifier notifier;

        #region DependencyProperty's

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.RegisterAttached("Label", typeof(string), typeof(TextBoxWatermarkBehavior));

        public string Label
        {
            get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty LabelStyleProperty =
            DependencyProperty.RegisterAttached("LabelStyle", typeof(Style), typeof(TextBoxWatermarkBehavior));

        public Style LabelStyle
        {
            get => (Style)GetValue(LabelStyleProperty); set => SetValue(LabelStyleProperty, value);
        }

        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObjectLoaded;
            AssociatedObject.TextChanged += AssociatedObjectTextChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Loaded -= AssociatedObjectLoaded;
            AssociatedObject.TextChanged -= AssociatedObjectTextChanged;

            notifier = null;
        }

        private void AssociatedObjectTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAdorner();
        }

        private void AssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            adorner = new LabelAdorner(AssociatedObject, Label, AssociatedObject);

            UpdateAdorner();

            //AddValueChanged for IsFocused in a weak manner
            notifier = new WeakPropertyChangeNotifier(AssociatedObject, UIElement.IsFocusedProperty);
            notifier.ValueChanged += new EventHandler(UpdateAdorner);
        }

        private void UpdateAdorner(object sender, EventArgs e)
        {
            UpdateAdorner();
        }


        private void UpdateAdorner()
        {
            if (!string.IsNullOrEmpty(AssociatedObject.Text) || AssociatedObject.IsFocused)
            {
                // Hide the Watermark Label if the adorner layer is visible
                AssociatedObject.TryRemoveAdorners<LabelAdorner>();
            }
            else
            {
                // Show the Watermark Label if the adorner layer is visible
                AssociatedObject.TryAddAdorner<LabelAdorner>(adorner);
            }
        }
    }
}
