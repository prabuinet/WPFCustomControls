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
using WpfCustomControlLibrary1;

namespace WPFCustomControls
{
    /// <summary>
    /// Interaction logic for AttachedPropertyWindow.xaml
    /// </summary>
    public partial class AttachedPropertyWindow : Window
    {
        public AttachedPropertyWindow()
        {
            InitializeComponent();

            // below code wont update the count, because oninitialized is run before before this code is run
            panel.SetValue(MyControlAttached.IncludeChildCountProperty, true);
            MyControlAttached.SetIncludeChildCount(panel, true);
        }
    }

    public partial class DataObj : ObservableObject
    {
        public DataObj()
        {

        }


    }
}
