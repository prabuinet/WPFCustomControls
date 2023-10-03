using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfCustomControlLibrary1
{
    public class MyCollectionControl : Control
    {
        static MyCollectionControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyCollectionControl), new FrameworkPropertyMetadata(typeof(MyCollectionControl)));
        }

        public MyCollectionControl()
        {
            Items = new ObservableCollection<object>();
        }



        public ObservableCollection<object> Items
        {
            get { return (ObservableCollection<object>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<object>), typeof(MyCollectionControl), 
                // setting default value for collection property will create a singleton, all instance of the control will point or have the save object referenced
                //new PropertyMetadata(new ObservableCollection<object>()));

                new PropertyMetadata(null));


    }
}
