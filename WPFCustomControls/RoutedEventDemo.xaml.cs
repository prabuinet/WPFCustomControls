using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace WPFCustomControls
{
    /// <summary>
    /// Interaction logic for RoutedEventDemo.xaml
    /// </summary>
    public partial class RoutedEventDemo : Window
    {
        public RoutedEventDemo()
        {
            InitializeComponent();
            this.DataContext = new RoutedWindowModel();
            this.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(MyHandler), true);
        }

        private void MyHandler(object sender, RoutedEventArgs routedEventArgs)
        {
            Debug.WriteLine("My handler");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button Click");
        }

        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("StackPanel_Click");            
        }

        private void Window_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Window_Click");
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Grid_Click");
            //e.Handled = true; - This will stop bubbling
        }

        private void Grid_MyClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Grid MyClick");            
        }
                
        private void MyEventControl_MyClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("MyEventControl MyClick");
        }

        private void MyEventControl_MyTripleClick(object sender, WpfCustomControlLibrary1.MyTripleClickEventArgs args)
        {
            Debug.WriteLine("MyEventControl MyTripleClick");
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(TextBox1.SelectedText))
                e.CanExecute = true;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Clipboard.SetText(TextBox1.SelectedText);
        }

        private void Paste2Button_Click(object sender, RoutedEventArgs e)
        {
            ApplicationCommands.Paste.Execute(null, TextBox2);
        }
    }
}
