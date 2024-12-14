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
#if DEBUG
            __parentVm.GroupPropertiesVm.Name.Value = $"Group-{Guid.NewGuid().ToString().ToUpper().Substring(0, 6)}";
#endif
            __parentVm.SkipPageName      = WizardPageNames.GroupPropertiesPage;
        });

        public ICommand RemoveGroupCmd => new RelayCommand(() => { });

        public ICommand AddChannelCmd => new RelayCommand(() =>
        {
            __parentVm.ChannelPropertiesVm = new(PropertiesType.Channel);
#if DEBUG
            __parentVm.ChannelPropertiesVm.Name.Value = $"Channel-{Guid.NewGuid().ToString().ToUpper().Substring(0, 6)}";
#endif
            __parentVm.SkipPageName        = WizardPageNames.ChannelPropertiesPage;
        });

        public ICommand RemoveChannelCmd => new RelayCommand(() => { });
    }
}