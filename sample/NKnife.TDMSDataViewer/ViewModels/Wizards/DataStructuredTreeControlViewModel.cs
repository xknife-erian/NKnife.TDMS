using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NKnife.TDMSDataViewer.Entities;

namespace NKnife.TDMSDataViewer.ViewModels.Wizards
{
    internal class DataStructuredTreeControlViewModel(SampleDataCreateWizardViewModel __parentVm) : ObservableObject
    {
        public ICommand AddGroupCmd => new RelayCommand(() =>
        {
            __parentVm.GroupPropertiesVm = new(PropertiesType.Group);
            __parentVm.PageName = "_GroupPropertiesPage_";
        });

        public ICommand RemoveGroupCmd => new RelayCommand(() => { });

        public ICommand AddChannelCmd => new RelayCommand(() =>
        {
            __parentVm.PageName = "_ChannelPropertiesPage_";
        });

        public ICommand RemoveChannelCmd => new RelayCommand(() => { });
    }
}