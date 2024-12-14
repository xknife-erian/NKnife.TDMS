using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;
using MvvmDialogs.FrameworkDialogs.FolderBrowser;
using MvvmDialogs.FrameworkDialogs.OpenFile;
using NKnife.TDMSDataViewer.Common;
using NKnife.TDMSDataViewer.ViewModels.Wizards;
using NKnife.TDMSDataViewer.Views;

namespace NKnife.TDMSDataViewer.ViewModels
{
    internal class WorkbenchViewModel(DialogService __dialogService) : BaseViewModel
    {
        public BaseDocumentViewModel? ActivePaneViewModel { get; set; }
        public ObservableCollection<BaseToolViewModel> Tools { get; set; } = new();
        public ObservableCollection<BaseDocumentViewModel> Documents { get; set; } = new();

        public ICommand CreateSampleDataFileCmd => new RelayCommand(() =>
        {
            __dialogService.ShowDialog(this, new SampleDataCreateWizardViewModel(__dialogService));
        });

        public ICommand CreateDataFileCmd => new RelayCommand(() =>
        {
            __dialogService.ShowFolderBrowserDialog(this, new FolderBrowserDialogSettings());
        });

        public ICommand OpenDataFileCmd => new RelayCommand(() =>
        {
            __dialogService.ShowOpenFileDialog(this, new OpenFileDialogSettings());
        });
    }
}