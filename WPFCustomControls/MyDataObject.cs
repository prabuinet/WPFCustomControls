using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCustomControls
{
    public partial class MyDataObject : ObservableObject
    {
        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string dpName;

        public MyDataObject()
        {

        }
    }
}
