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

    [TemplatePart(Name = TextBlockPart, Type = typeof(TextBlock))]
    public class MyControl : Selector
    {

        private const string TextBlockPart = "PART_TextBlock";

        //TextBlock textBlock = null;

        TextBlock _textBlock = null;
        protected TextBlock TextBlock
        {
            get { return _textBlock; }
           
            set
            {
                if (_textBlock != null)
                {
                    _textBlock.TextInput -= TextBlock_TextInput;
                }

                _textBlock = value;

                if (_textBlock != null)
                {
                    _textBlock.TextInput += TextBlock_TextInput;
                }
            }
        }


        static MyControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyControl), new FrameworkPropertyMetadata(typeof(MyControl)));
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            /*
            
            OnApplyTemplate method will be called multiple times, and the below event handling will lead to memory leak
             
            var textBlock = GetTemplateChild(TextBlockPart) as TextBlock;
            if(textBlock != null)
            {
                textBlock.Text = "Set From Code";
                textBlock.TextInput += TextBlock_TextInput;
            }
            */

            /* 
             
            the below code is converted into protected Property to make it clean             

            if(textBlock != null)
            {
                textBlock.TextInput -= TextBlock_TextInput;
            }

            textBlock = GetTemplateChild(TextBlockPart) as TextBlock;

            if (textBlock != null)
            {
                textBlock.Text = "Set From Code";
                textBlock.TextInput += TextBlock_TextInput;
            }

            */

            TextBlock = GetTemplateChild(TextBlockPart) as TextBlock;
        }


        private void TextBlock_TextInput(object sender, TextCompositionEventArgs e)
        {
            //throw new NotImplementedException();
        }


    }
}
