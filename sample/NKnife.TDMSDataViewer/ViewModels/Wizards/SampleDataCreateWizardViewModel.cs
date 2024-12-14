using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;
using NKnife.TDMSDataViewer.Common;
using NKnife.TDMSDataViewer.Entities;

namespace NKnife.TDMSDataViewer.ViewModels.Wizards
{
    internal class SampleDataCreateWizardViewModel : BaseViewModel, IModalDialogViewModel
    {
        private readonly DialogService _dialogService;

        public SampleDataCreateWizardViewModel(DialogService dialogService)
        {
            _dialogService   = dialogService;
            NextPageName         = WizardPageNames.WelcomePage;
            SkipPageName     = NextPageName;
            FilePropertiesVm = new(PropertiesType.File);
#if DEBUG
            FilePropertiesVm.Name.Value = $"SampleDataFile-{Guid.NewGuid().ToString().ToUpper().Substring(0,6)}";
#endif
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
        public string NextPageName
        {
            get;
            set => SetProperty(ref field, value);
        }

        public string SkipPageName
        {
            get;
            set
            {
                SetProperty(ref field, value);
                NextPageName = value;
            }
        }

        public LevelPropertiesControlViewModel FilePropertiesVm { get; init; }

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
            switch (NextPageName)
            {
                case WizardPageNames.WelcomePage:
                    NextPageName = WizardPageNames.FilePropertiesPage;

                    break;
                case WizardPageNames.FilePropertiesPage:
                    OnFilePropertiesPage();

                    break;
                case WizardPageNames.DataStructuredTreePage:
                    NextPageName = WizardPageNames.ResultConfirmationPage;

                    break;
                case WizardPageNames.GroupPropertiesPage:
                    OnGroupPropertiesPage();

                    break;
                case WizardPageNames.ChannelPropertiesPage:
                    OnChannelPropertiesPage();

                    break;
                case WizardPageNames.DataFormatSettingsPage:
                    NextPageName = WizardPageNames.DataStructuredTreePage;

                    break;
                case WizardPageNames.ResultConfirmationPage:
                    NextPageName = WizardPageNames.BuildProgressPage;

                    break;
                case WizardPageNames.BuildProgressPage:
                    NextPageName = WizardPageNames.EndPage;

                    break;
                case WizardPageNames.EndPage:
                    break;

            }
        });

        private void OnChannelPropertiesPage()
        {
            if(string.IsNullOrEmpty(ChannelPropertiesVm.Name.Value))
                NextPageName = WizardPageNames.ChannelPropertiesPage;
            else
                NextPageName = WizardPageNames.DataStructuredTreePage;
        }

        private void OnGroupPropertiesPage()
        {
            if(string.IsNullOrEmpty(GroupPropertiesVm.Name.Value))
                NextPageName = WizardPageNames.GroupPropertiesPage;
            else
                NextPageName = WizardPageNames.DataStructuredTreePage;
        }

        private void OnFilePropertiesPage()
        {
            if(string.IsNullOrEmpty(FilePropertiesVm.Name.Value))
            {
                _dialogService.ShowMessageBox(this,
                                              "Name是必填项，请填写后继续下一步。",
                                              "请完整填写",
                                              MessageBoxButton.OK,
                                              MessageBoxImage.Asterisk);
                NextPageName = WizardPageNames.FilePropertiesPage;
            }
            else
            {
                NextPageName = WizardPageNames.DataStructuredTreePage;
            }
        }

        public ICommand PageChangedCommand => new RelayCommand(() =>
        {
            switch (NextPageName)
            {
                case WizardPageNames.DataStructuredTreePage:
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
            }
        });

        public DataFileInfo DataFileInfo { get; set; } = new();
    }
}
