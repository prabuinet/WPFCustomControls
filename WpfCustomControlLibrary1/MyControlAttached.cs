using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfCustomControlLibrary1
{
    public class MyControlAttached : ContentControl
    {
        static MyControlAttached()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyControlAttached), new FrameworkPropertyMetadata(typeof(MyControlAttached)));
        }



        public int ChildCount
        {
            get { return (int)GetValue(ChildCountProperty); }
            set { SetValue(ChildCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChildCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChildCountProperty =
            DependencyProperty.Register("ChildCount", typeof(int), typeof(MyControlAttached), new PropertyMetadata(0));

        public static bool GetIncludeChildCount(DependencyObject obj)
        {
            return (bool)obj.GetValue(IncludeChildCountProperty);
        }

        public static void SetIncludeChildCount(DependencyObject obj, bool value)
        {
            obj.SetValue(IncludeChildCountProperty, value);
        }

        // Using a DependencyProperty as the backing store for IncludeChildCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IncludeChildCountProperty =
            DependencyProperty.RegisterAttached("IncludeChildCount", typeof(bool), typeof(MyControlAttached), new PropertyMetadata(false));

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            UpdateChildCount();
        }

        private void UpdateChildCount()
        {
            ChildCount = CalculateChildCount(this.Content);
        }

        private int CalculateChildCount(object content)
        {
            int count = 0;
            if(content is DependencyObject dp && GetIncludeChildCount(dp))
                count++;

            if(content is Panel p)
            {                
                foreach(var c in p.Children)
                    count += CalculateChildCount(c);
            }

            return count;
        }
    }
}
