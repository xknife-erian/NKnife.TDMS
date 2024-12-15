using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NKnife.TDMSDataViewer.Common;
using NKnife.TDMSDataViewer.Entities;

namespace NKnife.TDMSDataViewer.ViewModels.Wizards
{
    internal class DataStructuredTreeControlViewModel : ObservableObject
    {
        private readonly SampleDataCreateWizardViewModel _parentVm;

        public DataStructuredTreeControlViewModel(SampleDataCreateWizardViewModel parentVm)
        {
            _parentVm = parentVm;
#if DEBUG
            DbFileStructure = Helper.BuildSampleData();
#else
            DbFileStructure = new();
#endif
        }

        public DbFileStructure DbFileStructure
        {
            get;
            set => SetProperty(ref field, value);
        } 

        public ICommand AddGroupCmd => new RelayCommand(() =>
        {
            _parentVm.GroupPropertiesVm = new(PropertiesType.Group);
#if DEBUG
            _parentVm.GroupPropertiesVm.Name.Value = $"Group-{Guid.NewGuid().ToString().ToUpper().Substring(0, 6)}";
#endif
            _parentVm.SkipPageName      = WizardPageNames.GroupPropertiesPage;
        });

        public ICommand RemoveGroupCmd => new RelayCommand(() => { });

        public ICommand AddChannelCmd => new RelayCommand(() =>
        {
            _parentVm.ChannelPropertiesVm = new(PropertiesType.Channel);
#if DEBUG
            _parentVm.ChannelPropertiesVm.Name.Value = $"Channel-{Guid.NewGuid().ToString().ToUpper().Substring(0, 6)}";
#endif
            _parentVm.SkipPageName        = WizardPageNames.ChannelPropertiesPage;
        });

        public ICommand RemoveChannelCmd => new RelayCommand(() => { });
        public ICommand EditDataFormatSettingsCmd => new RelayCommand(() =>
        {
            _parentVm.SkipPageName = WizardPageNames.EditDataFormatSettingsPage;
            _parentVm.PreviousPageName = WizardPageNames.DataStructurePage;
        });
        public ICommand Add100RandomIntDataCmd => new RelayCommand(() => { });
        public ICommand Add100RandomIntWithTimeDataCmd => new RelayCommand(() => { });
        public ICommand Add1000RandomFloatWithTimeDataCmd => new RelayCommand(() => { });
    }
}