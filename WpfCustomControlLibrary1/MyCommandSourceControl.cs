using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfCustomControlLibrary1
{

    public class MyCommandSourceControl : Control, ICommandSource
    {
        private TextBlock button;

        static MyCommandSourceControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyCommandSourceControl), new FrameworkPropertyMetadata(typeof(MyCommandSourceControl)));
        }

        public MyCommandSourceControl()
        {
            
        }

        private EventHandler canExecuteChangedHandler;

        [TypeConverter(typeof(CommandConverter))]
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(MyCommandSourceControl), new PropertyMetadata(null, new PropertyChangedCallback(OnCommandChanged)));


        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as MyCommandSourceControl;
            if(control != null)
            {
                control.OnCommandChanged((ICommand) e.OldValue, (ICommand) e.NewValue);
            }
        }

        protected virtual void OnCommandChanged(ICommand oldValue, ICommand newValue)
        {
            if(oldValue != null)
            {
                UnhookCommand(oldValue, newValue);
            }

            HookupCommand(oldValue, newValue);

            CanExecuteChanged(null, null);
        }

        private void UnhookCommand(ICommand oldCommand, ICommand newCommand)
        {
            EventHandler hander = CanExecuteChanged;
            oldCommand.CanExecuteChanged -= CanExecuteChanged;
        }

        private void HookupCommand(ICommand oldCommand, ICommand newCommand)
        {
            EventHandler handler = new EventHandler(CanExecuteChanged);
            canExecuteChangedHandler = handler;
            if(newCommand != null)
            {
                newCommand.CanExecuteChanged += canExecuteChangedHandler;
            }
        }

        private void CanExecuteChanged(object sender, EventArgs e)
        {
            if(Command != null)
            {
                RoutedCommand rc = Command as RoutedCommand;
                if(rc != null)
                {
                    IsEnabled = rc.CanExecute(CommandParameter, CommandTarget);
                }
                else 
                    IsEnabled = Command.CanExecute(CommandParameter);
            }
        }

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(MyCustomReadOnlyControl), new PropertyMetadata(null));




        public IInputElement CommandTarget
        {
            get { return (IInputElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandTarget.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(MyCommandSourceControl), new PropertyMetadata(null));




        //public object CommandParameter => throw new NotImplementedException();

        //public IInputElement CommandTarget => throw new NotImplementedException();

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if(button != null)
                button.MouseLeftButtonUp -= Button_MouseLeftButtonUp;

            button = GetTemplateChild("PART_TextBlock") as TextBlock;

            if(button != null)
                button.MouseLeftButtonUp += Button_MouseLeftButtonUp;
        }

        private void Button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RaiseCommand();
        }

        private void RaiseCommand()
        {
            if (Command != null)
            {
                RoutedCommand rc = Command as RoutedCommand;
                if (rc != null)
                {
                    rc.Execute(CommandParameter, CommandTarget);
                }
                else
                    Command.Execute(CommandParameter);
            }
        }
    }
}
