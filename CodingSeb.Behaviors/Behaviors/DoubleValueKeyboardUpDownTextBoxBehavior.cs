using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CodingSeb.Behaviors
{
    public class DoubleValueKeyboardUpDownTextBoxBehavior : Behavior<TextBox>
    {
        /// <summary>
        /// Predefined mode of use of this Behavior to set others properties
        /// </summary>
        public DoubleValueMode Mode
        {
            get { return (DoubleValueMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Mode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(DoubleValueMode), typeof(DoubleValueKeyboardUpDownTextBoxBehavior), new PropertyMetadata(DoubleValueMode.None, new PropertyChangedCallback(ModeChanged)));

        private static void ModeChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            if(sender is DoubleValueKeyboardUpDownTextBoxBehavior behavior)
            {
                behavior.UpdateFromMode();
            }
        }

        private void UpdateFromMode()
        {
            switch(Mode)
            {
                case DoubleValueMode.Percentage:
                    Maximum = 100;
                    Minimum = 0;
                    CanEqualMaximum = true;
                    CanEqualMinimum = true;
                    Loop = false;
                    break;
                case DoubleValueMode.PositiveOnly:
                    Maximum = double.MaxValue;
                    Minimum = 0;
                    CanEqualMaximum = true;
                    CanEqualMinimum = true;
                    Loop = false;
                    break;
                case DoubleValueMode.NegativeOnly:
                    Maximum = 0;
                    Minimum = double.MinValue;
                    CanEqualMaximum = true;
                    CanEqualMinimum = true;
                    Loop = false;
                    break;
                case DoubleValueMode.DegreesAngle:
                    Maximum = 360;
                    Minimum = 0;
                    CanEqualMaximum = false;
                    CanEqualMinimum = true;
                    Loop = true;
                    break;
                case DoubleValueMode.RadianAngle:
                    Maximum = 2 * Math.PI;
                    Minimum = 0;
                    CanEqualMaximum = false;
                    CanEqualMinimum = true;
                    Loop = true;
                    break;
                default:
                    Maximum = double.MaxValue;
                    Minimum = double.MinValue;
                    CanEqualMaximum = true;
                    CanEqualMinimum = true;
                    Loop = true;
                    break;
            }
        }

        /// <summary>
        /// The Maximum value.
        /// By default double.MaxValue
        /// </summary>
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Maximum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(DoubleValueKeyboardUpDownTextBoxBehavior), new PropertyMetadata(double.MaxValue));

        /// <summary>
        /// Specifiy if the value of the textBox can equal Maximum or not
        /// By default : true
        /// </summary>
        public bool CanEqualMaximum
        {
            get { return (bool)GetValue(CanEqualMaximumProperty); }
            set { SetValue(CanEqualMaximumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanEqualMaximum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanEqualMaximumProperty =
            DependencyProperty.Register("CanEqualMaximum", typeof(bool), typeof(DoubleValueKeyboardUpDownTextBoxBehavior), new PropertyMetadata(true));

        /// <summary>
        /// The Minimum value.
        /// By default double.MinValue
        /// </summary>
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Minimum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(DoubleValueKeyboardUpDownTextBoxBehavior), new PropertyMetadata(double.MinValue));

        /// <summary>
        /// Specifiy if the value of the textBox can equal Minimum or not
        /// By default : true
        /// </summary>
        public bool CanEqualMinimum
        {
            get { return (bool)GetValue(CanEqualMinimumProperty); }
            set { SetValue(CanEqualMinimumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanEqualMinimum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanEqualMinimumProperty =
            DependencyProperty.Register("CanEqualMinimum", typeof(bool), typeof(DoubleValueKeyboardUpDownTextBoxBehavior), new PropertyMetadata(true));

        /// <summary>
        /// If true when value reach Maximum it go automatically to Minimum and when value reachMaximum it go automatically to Maximum.
        /// If false When Maximum or minimum are reach it can'nt go further.
        /// By Default : false
        /// </summary>
        public bool Loop
        {
            get { return (bool)GetValue(LoopProperty); }
            set { SetValue(LoopProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Loop.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoopProperty =
            DependencyProperty.Register("Loop", typeof(bool), typeof(DoubleValueKeyboardUpDownTextBoxBehavior), new PropertyMetadata(false));

        /// <summary>
        /// Increment when Up or Down used without modifier keys
        /// </summary>
        public double Increment
        {
            get { return (double)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Increment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IncrementProperty =
            DependencyProperty.Register("Increment", typeof(double), typeof(DoubleValueKeyboardUpDownTextBoxBehavior), new PropertyMetadata(1d));

        /// <summary>
        /// Increment when Up or Down used with Ctrl modifier key
        /// </summary>
        public double SmallIncrement
        {
            get { return (double)GetValue(SmallIncrementProperty); }
            set { SetValue(SmallIncrementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SmallIncrement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SmallIncrementProperty =
            DependencyProperty.Register("SmallIncrement", typeof(double), typeof(DoubleValueKeyboardUpDownTextBoxBehavior), new PropertyMetadata(0.1d));

        /// <summary>
        /// Increment when Up or Down used with Shift modifier key
        /// </summary>
        public double BigIncrement
        {
            get { return (double)GetValue(BigIncrementProperty); }
            set { SetValue(BigIncrementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BigIncrement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BigIncrementProperty =
            DependencyProperty.Register("BigIncrement", typeof(double), typeof(DoubleValueKeyboardUpDownTextBoxBehavior), new PropertyMetadata(5.0d));

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewKeyDown += PreviewKeyDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewKeyDown -= PreviewKeyDown;
        }

        private void PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (sender is TextBox txtBox)
            {
                if (double.TryParse(txtBox.Text, out double value))
                {
                    double newValue = value;
                    double increment = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift) ? BigIncrement : (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl) ? SmallIncrement : Increment);

                    if (e.Key == Key.Up)
                    {
                        newValue = value + increment;
                        
                        if(newValue > Maximum || (newValue.Equals(Maximum) && !CanEqualMaximum))
                        {
                            if (Loop)
                                newValue = Minimum + newValue - Maximum;
                            else if (CanEqualMaximum)
                                newValue = Maximum;
                            else
                                newValue = value;
                        }

                        txtBox.Text = newValue.ToString();
                        e.Handled = true;
                    }
                    else if (e.Key == Key.Down)
                    {
                        newValue = value - increment;

                        if (newValue < Minimum || (newValue.Equals(Minimum) && !CanEqualMinimum))
                        {
                            if (Loop)
                                newValue = Maximum - newValue + Minimum;
                            else if (CanEqualMinimum)
                                newValue = Minimum;
                            else
                                newValue = value;
                        }

                        txtBox.Text = newValue.ToString();
                        e.Handled = true;
                    }
                }
            }
        }
    }
}
