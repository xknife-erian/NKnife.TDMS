using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace NKnife.TDMSDataViewer.ViewModels.Wizards
{
    class DataStructuredTreeControlViewModel : ObservableObject
    {
        public ICommand AddGroupCmd => new RelayCommand(() =>
        {
        });
        public ICommand RemoveGroupCmd => new RelayCommand(() =>
        {
        });
        public ICommand AddChannelCmd => new RelayCommand(() =>
        {
        });
        public ICommand RemoveChannelCmd => new RelayCommand(() =>
        {
        });
    }
}
