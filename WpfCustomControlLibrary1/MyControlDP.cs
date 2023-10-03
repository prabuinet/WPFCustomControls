using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCustomControlLibrary1
{

    public class MyControlDP : Control
    {

        static MyControlDP()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyControlDP), new FrameworkPropertyMetadata(typeof(MyControlDP)));            
        }


        // these are not mandatory to create a dependency property, just there for convenience
        public string Text
        {
            //IMPORTANT: Never write any logic inside this getter and setters
            // Reason: because the setter will never hit when databinding, actual TextProperty is called, not the below setter
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MyControlDP), 
                new PropertyMetadata("Default Value Text", 
                    OnPropertyChangeCallback, OnCoerceValueCallback));



        private static void OnPropertyChangeCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private static object OnCoerceValueCallback(DependencyObject d, object baseValue)
        {
            //throw new NotImplementedException();
            MyControlDP control = d as MyControlDP;
            if (control != null)
            {
                if ((string)baseValue == "hello")
                {
                    return "hello world";
                }
            }

            return baseValue;
        }

        public static readonly DependencyProperty BorderProperty =
            DependencyProperty.Register("Border", typeof(string), typeof(MyControlDP), 
                new PropertyMetadata(null, 
                    new PropertyChangedCallback(OnBorderPropertyChangedCallback), 
                    new CoerceValueCallback(OnBorderPropertyCoerce)));

        public static readonly DependencyProperty BoldProperty =
            DependencyProperty.Register("Bold", typeof(string), typeof(MyControlDP),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnBoldPropertyChangedCallback),
                    new CoerceValueCallback(OnBoldPropertyCoerce)));

        private static object OnBoldPropertyCoerce(DependencyObject d, object baseValue)
        {
            //throw new NotImplementedException();
            return baseValue;
        }

        private static void OnBoldPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private static void OnBorderPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MyControlDP control = d as MyControlDP;
            if (control != null)
            {
                // always use a virtual method to make any changes (so that the inheriting control can override the behaviour)
                control.OnBorderPropertyChangedCallback(e.OldValue as string, e.NewValue as string);
            }
        }

        public virtual void OnBorderPropertyChangedCallback(string oldValue, string newValue)
        {
            // perform the changes
        }

        // CoerceValue Callback - this allow us to change the value of the incoming value 
        private static object OnBorderPropertyCoerce(DependencyObject d, object baseValue)
        {
            MyControlDP control = d as MyControlDP;
            if (control != null)
            {
                if((string)baseValue == "hello")
                {
                    return "hello world";
                }
            }

            return baseValue;   
        }
    }
}
