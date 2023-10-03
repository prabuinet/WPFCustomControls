using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCustomControlLibrary1
{
    public class MyCustomReadOnlyControl : ContentControl
    {
        private const string ButtonPart = "PART_Button";

        Button button = null;

        protected Button Button
        {
            get { return button; }

            set
            {
                if (button != null)
                {
                    button.Click -= button_Click;
                }

                button = value;

                if (button != null)
                {
                    button.Click += button_Click;
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.Button = GetTemplateChild(ButtonPart) as Button;
        }

        static MyCustomReadOnlyControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyCustomReadOnlyControl), new FrameworkPropertyMetadata(typeof(MyCustomReadOnlyControl)));
        }

        
        private static readonly DependencyPropertyKey HasBeenClickedPropertyKey =
            DependencyProperty.RegisterReadOnly("HasBeenClicked", typeof(bool), 
                typeof(MyCustomReadOnlyControl), new PropertyMetadata(false));

        public static readonly DependencyProperty HasBeenClickedProperty = HasBeenClickedPropertyKey.DependencyProperty;

        public bool HasBeenClicked { get {
                return (bool)GetValue(HasBeenClickedProperty);
            } 
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SetValue(HasBeenClickedPropertyKey, true);
        }

    }
}
