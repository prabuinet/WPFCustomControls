using System;
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

    public class MyEventControl : Selector
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
                    button.Click -= Button_Click;
                }

                button = value;

                if (button != null)
                {
                    button.Click += Button_Click;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //RaiseMyClickEvent();
            RaiseMyTripleClickEvent();
        }

        static MyEventControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyEventControl), new FrameworkPropertyMetadata(typeof(MyEventControl)));
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Button = GetTemplateChild(ButtonPart) as Button;
        }



        /* -- MyClickEvent */
        public static readonly RoutedEvent MyClickEvent = EventManager.RegisterRoutedEvent("MyClick", RoutingStrategy.Bubble, 
                typeof(RoutedEventHandler), typeof(MyEventControl));

        public event RoutedEventHandler MyClick
        {
            add { AddHandler(MyClickEvent, value); }
            remove { RemoveHandler(MyClickEvent, value); }
        }

        protected virtual void RaiseMyClickEvent()
        {
            var args = new RoutedEventArgs(MyEventControl.MyClickEvent);
            RaiseEvent(args);
        }
        /* -- MyClickEvent */


        /* -- MyTripleClickEvent */
        public static readonly RoutedEvent MyTripleClickEvent = EventManager.RegisterRoutedEvent("MyTripleClick", RoutingStrategy.Bubble,
                typeof(MyTripleClickEventHandler), typeof(MyEventControl));

        public event MyTripleClickEventHandler MyTripleClick
        {
            add { AddHandler(MyTripleClickEvent, value); }
            remove { RemoveHandler(MyTripleClickEvent, value); }
        }

        protected virtual void RaiseMyTripleClickEvent()
        {
            var args = new MyTripleClickEventArgs(MyEventControl.MyTripleClickEvent, 100);
            RaiseEvent(args);
        }
        /* -- MyClickEvent */
    }


    /* -- MyTripleClickEvent */

    public delegate void MyTripleClickEventHandler(object sender, MyTripleClickEventArgs args);

    public class MyTripleClickEventArgs : RoutedEventArgs
    {
        public int SomeValue { get; set; }

        public MyTripleClickEventArgs(RoutedEvent e, int someValue) 
            : base(e)
        {
            SomeValue = someValue;
        }
    }


}
