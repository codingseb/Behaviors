using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace CodingSeb.Behaviors
{
    /// <summary>
    /// Allow to generate the specified ICommand On the specified Keyboard event
    /// </summary>
    public class KeyboardToCommandBehavior : Behavior<Control>
    {
        /// <summary>
        /// The Keyboard event on which to send the command.
        /// By Default : KeyDown
        /// </summary>
        public KeyboardEventType KeyboardEvent
        {
            get { return (KeyboardEventType)GetValue(KeyBoardEventProperty); }
            set { SetValue(KeyBoardEventProperty, value); }
        }

        // Using a DependencyProperty as the backing store for KeyBoardEventType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyBoardEventProperty =
            DependencyProperty.Register("KeyBoardEvent", typeof(KeyboardEventType?), typeof(KeyboardToCommandBehavior), new PropertyMetadata(KeyboardEventType.KeyDown));


        /// <summary>
        /// The command to execute
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(KeyboardToCommandBehavior), new PropertyMetadata(null));

        /// <summary>
        /// The Key Stroke to watch to execute the command (Examples : "Ctrl+E", "F5", "Alt+Shift+Escape").
        /// ByDefault : "Enter"
        /// </summary>
        public string KeyStroke
        {
            get { return (string)GetValue(KeyStrokeProperty); }
            set { SetValue(KeyStrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for KeyStroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyStrokeProperty =
            DependencyProperty.Register("KeyStroke", typeof(string), typeof(KeyboardToCommandBehavior), new PropertyMetadata("Enter"));

        protected override void OnAttached()
        {
            base.OnAttached();
            TryToSubScribe();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            TryToUnSubscribe();
        }

        private void TryToSubScribe()
        {
            if (AssociatedObject != null)
            {
                switch(KeyboardEvent)
                {
                    case KeyboardEventType.KeyUp:
                        AssociatedObject.KeyUp += AssociatedObject_KeyEvent;
                        break;
                    case KeyboardEventType.PreviewKeyDown:
                        AssociatedObject.PreviewKeyDown += AssociatedObject_KeyEvent;
                        break;
                    case KeyboardEventType.PreviewKeyUp:
                        AssociatedObject.PreviewKeyUp += AssociatedObject_KeyEvent;
                        break;
                    default:
                        AssociatedObject.KeyDown += AssociatedObject_KeyEvent;
                        break;
                }
            }
        }

        private void TryToUnSubscribe()
        {
            if (AssociatedObject != null)
            {
                switch (KeyboardEvent)
                {
                    case KeyboardEventType.KeyUp:
                        AssociatedObject.KeyUp -= AssociatedObject_KeyEvent;
                        break;
                    case KeyboardEventType.PreviewKeyDown:
                        AssociatedObject.PreviewKeyDown -= AssociatedObject_KeyEvent;
                        break;
                    case KeyboardEventType.PreviewKeyUp:
                        AssociatedObject.PreviewKeyUp -= AssociatedObject_KeyEvent;
                        break;
                    default:
                        AssociatedObject.KeyDown -= AssociatedObject_KeyEvent;
                        break;
                }
            }
        }

        private void AssociatedObject_KeyEvent(object sender, KeyEventArgs e)
        {
            if (Command?.CanExecute(AssociatedObject) == true && KeyStroke != null)
            {
                bool execute = KeyStroke.Split('+').Last().Trim().Equals(e.Key.ToString(), StringComparison.OrdinalIgnoreCase);

                if (KeyStroke.IndexOf("Ctrl", StringComparison.OrdinalIgnoreCase) > -1 && (Keyboard.Modifiers & ModifierKeys.Control) == 0)
                    execute = false;
                if (KeyStroke.IndexOf("Alt", StringComparison.OrdinalIgnoreCase) > -1 && (Keyboard.Modifiers & ModifierKeys.Alt) == 0)
                    execute = false;
                if (KeyStroke.IndexOf("Shift", StringComparison.OrdinalIgnoreCase) > -1 && (Keyboard.Modifiers & ModifierKeys.Shift) == 0)
                    execute = false;
                if (KeyStroke.IndexOf("Win", StringComparison.OrdinalIgnoreCase) > -1 && (Keyboard.Modifiers & ModifierKeys.Windows) == 0)
                    execute = false;

                if (execute)
                {
                    Command?.Execute(AssociatedObject);
                }
            }
        }
    }
}
