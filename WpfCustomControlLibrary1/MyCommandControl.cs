using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfCustomControlLibrary1
{
    public static class ControlCommands
    {
        private static RoutedCommand _MyUpdateCommand = new RoutedCommand();

        public static RoutedCommand MyUpdateCommand
        {
            get => _MyUpdateCommand;
            set => _MyUpdateCommand = value;
        }
        
    }
    

    public class MyCommandControl : Control
    {
        private Button button;

        static MyCommandControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyCommandControl), new FrameworkPropertyMetadata(typeof(MyCommandControl)));
        }

        public MyCommandControl()
        {
            CommandBindings.Add(new CommandBinding(ControlCommands.MyUpdateCommand, ExecuteUpdate, CanExecuteUpdate));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, ExecuteCut, CanExecuteCut));
            
        }

        private void CanExecuteCut(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExecuteCut(object sender, ExecutedRoutedEventArgs e)
        {
            button.Content = "Cut Executed";
        }

        private void ExecuteUpdate(object sender, ExecutedRoutedEventArgs e)
        {
            if (button != null)
                button.Content = e.Parameter;
        }

        private void CanExecuteUpdate(object sender, CanExecuteRoutedEventArgs e)
        {
            var p = e.Parameter as string;
            if(p != null)
                e.CanExecute = true;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            button = GetTemplateChild("PART_Button") as Button;
        }
    }
}
