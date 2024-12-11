using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;
using NKnife.TDMSDataViewer.Common;
using NKnife.TDMSDataViewer.Entities;

namespace NKnife.TDMSDataViewer.ViewModels.Wizards
{
    internal class SampleDataCreateWizardViewModel : BaseViewModel, IModalDialogViewModel
    {
        private string _lastPageName = "_WelcomePage_";

        public SampleDataCreateWizardViewModel()
        {
            PageName           = _lastPageName;
            DataStructuredTree = new(this);

            GroupPropertiesVm = new(PropertiesType.Group);
            ChannelPropertiesVm = new(PropertiesType.Channel);
        }

        /// <inheritdoc />
        public bool? DialogResult { get; } = null;

        public string? FileFullPath
        {
            get;
            set => SetProperty(ref field, value);
        }

        public LevelPropertiesControlViewModel FilePropertiesVm { get; init; } = new(PropertiesType.File);

        public DataStructuredTreeControlViewModel DataStructuredTree { get; init; }

        public LevelPropertiesControlViewModel GroupPropertiesVm
        {
            get;
            set => SetProperty(ref field, value);
        }

        public LevelPropertiesControlViewModel ChannelPropertiesVm
        {
            get;
            set => SetProperty(ref field, value);
        }

        public ICommand NextCommand => new RelayCommand(() =>
        {
            switch (PageName)
            {
                case "_FilePropertyPage_":
                    if(string.IsNullOrEmpty(FilePropertiesVm.Name.Value)
                       || string.IsNullOrEmpty(FilePropertiesVm.Description.Value))
                        PageName = "_FilePropertyPage_";

                    break;
            }
        });

        public ICommand PageChangedCommand => new RelayCommand(() =>
        {
            switch (_lastPageName)
            {
                case "_FilePropertyPage_":
                    DataFileInfo.FileProperties.Clear();

                    DataFileInfo.FileProperties.Add(FilePropertiesVm.Name);
                    DataFileInfo.FileProperties.Add(FilePropertiesVm.Description);

                    if(!string.IsNullOrEmpty(FilePropertiesVm.Title.Value))
                        DataFileInfo.FileProperties.Add(FilePropertiesVm.Title);
                    if(!string.IsNullOrEmpty(FilePropertiesVm.Author.Value))
                        DataFileInfo.FileProperties.Add(FilePropertiesVm.Author);

                    foreach (var lp in FilePropertiesVm.LevelPropertiesSet)
                    {
                        if(!string.IsNullOrEmpty(lp.Name)
                           && !string.IsNullOrEmpty(lp.Value))
                            DataFileInfo.FileProperties.Add(lp);
                    }

                    break;
                case "_GroupPropertiesPage_":
                case "_ChannelPropertiesPage_":
                    break;
            }

            _lastPageName = PageName;
        });

        public DataFileInfo DataFileInfo { get; set; } = new();

        public string PageName
        {
            get;
            set => SetProperty(ref field, value);
        }

    }
}
