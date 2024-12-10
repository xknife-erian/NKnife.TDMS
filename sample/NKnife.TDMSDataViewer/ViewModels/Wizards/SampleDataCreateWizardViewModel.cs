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
        }

        /// <inheritdoc />
        public bool? DialogResult { get; } = null;

        public string? FileFullPath
        {
            get;
            set => SetProperty(ref field, value);
        }

        public LevelPropertiesControlViewModel FileProperties { get; init; } = new(PropertiesType.File);
        public LevelPropertiesControlViewModel GroupProperties { get; init; } = new(PropertiesType.Group);
        public LevelPropertiesControlViewModel ChannelProperties { get; init; } = new(PropertiesType.Channel);
        public DataStructuredTreeControlViewModel DataStructuredTree { get; init; }

        public ICommand PageChangedCommand => new RelayCommand(() =>
        {
            switch (_lastPageName)
            {
                case "_FilePropertyPage_":
                    DataFileInfo.FileProperties.Clear();
                    // DataFileInfo.FileProperties.AddRange(FileProperties.LevelPropertiesSet);

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
