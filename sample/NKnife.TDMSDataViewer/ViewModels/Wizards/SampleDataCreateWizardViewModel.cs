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
            NextPageName     = WizardPageNames.WelcomePage;
            SkipPageName     = NextPageName;
            PreviousPageName = NextPageName;
            FilePropertiesVm = new LevelPropertiesControlViewModel(PropertiesType.File);
#if DEBUG
            FilePropertiesVm.Name.Value = $"SampleDataFile-{Guid.NewGuid().ToString().ToUpper().Substring(0, 6)}";
#endif
            DataStructuredTree = new DataStructuredTreeControlViewModel(this);

            GroupPropertiesVm   = new LevelPropertiesControlViewModel(PropertiesType.Group);
            ChannelPropertiesVm = new LevelPropertiesControlViewModel(PropertiesType.Channel);
        }

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

        public string PreviousPageName
        {
            get;
            set => SetProperty(ref field, value);
        }

        public string SkipPageName
        {
            get;
            set
            {
                if(value == WizardPageNames.GroupPropertiesPage
                   || value == WizardPageNames.ChannelPropertiesPage)
                    PreviousPageName = WizardPageNames.DataStructuredTreePage;
                NextPageName = value;
                SetProperty(ref field, value);
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

        public DataFileInfo DataFileInfo { get; set; } = new();

        public ICommand NextCommand => new RelayCommand(() =>
        {
            switch (NextPageName)
            {
                case WizardPageNames.WelcomePage:
                    NextPageName = WizardPageNames.FilePropertiesPage;
                    PreviousPageName = WizardPageNames.WelcomePage;

                    break;
                case WizardPageNames.FilePropertiesPage:
                    OnFilePropertiesPage();

                    break;
                case WizardPageNames.DataStructuredTreePage:
                    NextPageName = WizardPageNames.ResultConfirmationPage;
                    PreviousPageName = WizardPageNames.DataStructuredTreePage;

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
                    PreviousPageName = WizardPageNames.DataStructuredTreePage;

                    break;
                case WizardPageNames.BuildProgressPage:
                    NextPageName = WizardPageNames.EndPage;
                    PreviousPageName = WizardPageNames.BuildProgressPage;

                    break;
                case WizardPageNames.EndPage:
                    break;
            }
        });

        public ICommand PreviousCommand => new RelayCommand(() => { });
        public ICommand CancelCommand => new RelayCommand(() => { });
        public ICommand FinishCommand => new RelayCommand(() => { });

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

        /// <inheritdoc />
        public bool? DialogResult { get; } = null;

        private void OnChannelPropertiesPage()
        {
            if(string.IsNullOrEmpty(ChannelPropertiesVm.Name.Value))
            {
                PromptNamePropertyNotFilled();
                NextPageName     = WizardPageNames.ChannelPropertiesPage;
                PreviousPageName = WizardPageNames.DataStructuredTreePage;
            }
            else
            {
                NextPageName     = WizardPageNames.DataStructuredTreePage;
                PreviousPageName = WizardPageNames.FilePropertiesPage;
            }
        }

        private void OnGroupPropertiesPage()
        {
            if(string.IsNullOrEmpty(GroupPropertiesVm.Name.Value))
            {
                PromptNamePropertyNotFilled();
                NextPageName     = WizardPageNames.GroupPropertiesPage;
                PreviousPageName = WizardPageNames.DataStructuredTreePage;
            }
            else
            {
                NextPageName     = WizardPageNames.DataStructuredTreePage;
                PreviousPageName = WizardPageNames.FilePropertiesPage;
            }
        }

        private void OnFilePropertiesPage()
        {
            if(string.IsNullOrEmpty(FilePropertiesVm.Name.Value))
            {
                PromptNamePropertyNotFilled();
                NextPageName     = WizardPageNames.FilePropertiesPage;
                PreviousPageName = WizardPageNames.WelcomePage;
            }
            else
            {
                NextPageName     = WizardPageNames.DataStructuredTreePage;
                PreviousPageName = WizardPageNames.FilePropertiesPage;
            }
        }

        private void PromptNamePropertyNotFilled()
        {
            _dialogService.ShowMessageBox(this, "[Name] 是必填项，请填写后继续下一步。", "请完整填写", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}