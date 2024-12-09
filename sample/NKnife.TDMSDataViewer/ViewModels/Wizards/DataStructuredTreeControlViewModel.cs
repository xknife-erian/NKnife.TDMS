using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace NKnife.TDMSDataViewer.ViewModels.Wizards
{
    class DataStructuredTreeControlViewModel(SampleDataCreateWizardViewModel __parentVm) : ObservableObject
    {
        public ICommand AddGroupCmd => new RelayCommand(() =>
        {
            __parentVm.PageName = "_GroupPropertyPage_";
        });
        public ICommand RemoveGroupCmd => new RelayCommand(() =>
        {
        });
        public ICommand AddChannelCmd => new RelayCommand(() =>
        {
            __parentVm.PageName = "_ChannelPropertyPage_";
        });
        public ICommand RemoveChannelCmd => new RelayCommand(() =>
        {
        });
    }
}
