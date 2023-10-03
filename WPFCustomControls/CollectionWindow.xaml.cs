using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WPFCustomControls
{
    /// <summary>
    /// Interaction logic for CollectionWindow.xaml
    /// </summary>
    public partial class CollectionWindow : Window
    {
        public CollectionWindow()
        {
            InitializeComponent();

            DataObject obj1 = new DataObject() { Name = "One" };
            DataObject obj2 = new DataObject() { Name = "Two" };

            list1.Items.Add(obj1);
            list2.Items.Add(obj2);
        }
    }

    public partial class DataObject : ObservableObject
    {
        [ObservableProperty]
        private string name;

        public override string ToString()
        {
            return Name;
        }
    }
}
