using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WPFCustomControls
{
    public partial class RoutedWindowModel : ObservableObject
    {
        [ObservableProperty]
        string name = "My Name is?";

        [RelayCommand]
        public void SetName(string _name)
        {
            this.Name = _name;
        }
    }
}
