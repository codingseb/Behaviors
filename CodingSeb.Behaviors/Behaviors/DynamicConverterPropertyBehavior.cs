using Microsoft.Xaml.Behaviors;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace CodingSeb.Behaviors
{
    public class DynamicConverterPropertyBehavior : Behavior<DependencyObject>
    {
        private readonly ExpressionEvaluator.ExpressionEvaluator expressionEvaluator = new ExpressionEvaluator.ExpressionEvaluator();

        public string BindingPropertyName
        {
            get { return (string)GetValue(BindingPropertyNameProperty); }
            set { SetValue(BindingPropertyNameProperty, value); }
        }

        public static readonly DependencyProperty BindingPropertyNameProperty =
            DependencyProperty.Register("BindingPropertyName", typeof(string), typeof(DynamicConverterPropertyBehavior)
                , new FrameworkPropertyMetadata(string.Empty, OnDependencyPropertyChanged));

        public string ConverterPropertyName
        {
            get { return (string)GetValue(ConverterPropertyNameProperty); }
            set { SetValue(ConverterPropertyNameProperty, value); }
        }

        public static readonly DependencyProperty ConverterPropertyNameProperty =
            DependencyProperty.Register("ConverterPropertyName", typeof(string), typeof(DynamicConverterPropertyBehavior),
                new FrameworkPropertyMetadata(string.Empty, OnDependencyPropertyChanged));

        public object Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(DynamicConverterPropertyBehavior),
                new FrameworkPropertyMetadata(null, OnDependencyPropertyChanged));

        private static void OnDependencyPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            (dependencyObject as DynamicConverterPropertyBehavior)?.Update();
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            Update();
        }

        private void Update()
        {
            if(!string.IsNullOrEmpty(BindingPropertyName) && Value != null && AssociatedObject != null)
            {
                DependencyProperty dependencyProperty = DependencyPropertyDescriptor.FromName(BindingPropertyName, AssociatedObject.GetType(), AssociatedObject.GetType())?.DependencyProperty;

                if (dependencyProperty != null)
                {
                    Binding binding = BindingOperations.GetBinding(AssociatedObject, dependencyProperty);

                    if (binding.Converter != null)
                    {
                        try
                        {
                            if (ConverterPropertyName.Contains(".") || ConverterPropertyName.Contains("["))
                            {
                                expressionEvaluator.Variables["converter"] = binding.Converter;
                                expressionEvaluator.Variables["value"] = Value;

                                expressionEvaluator.Evaluate($"converter.{ConverterPropertyName} = value");
                            }
                            else
                            {
                                binding.Converter.GetType().GetProperty(ConverterPropertyName)?.SetValue(binding.Converter, Value);
                            }

                            BindingOperations.GetBindingExpression(AssociatedObject, dependencyProperty)?.UpdateTarget();
                        }
                        catch { }
                    }
                }
            }
        }
    }
}
