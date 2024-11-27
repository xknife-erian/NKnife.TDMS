using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;
using NKnife.TDMSDataViewer.ViewModels.Common;
using NKnife.TDMSDataViewer.ViewModels.Dialogs;

namespace NKnife.TDMSDataViewer.ViewModels
{
    internal class WorkbenchViewModel : BaseViewModel
    {
        public WorkbenchViewModel()
        {
            var _dialogService = new DialogService(frameworkDialogFactory: new CustomFrameworkDialogFactory());

        }
        public BaseDocumentViewModel? ActivePaneViewModel { get; set; }
        public ObservableCollection<BaseToolViewModel> Tools { get; set; } = new();
        public ObservableCollection<BaseDocumentViewModel> Documents { get; set; } = new();
        public ICommand CreateSampleDataFileCmd => new AsyncRelayCommand(() => { return Task.CompletedTask;});
        public ICommand CreateDataFileCmd => new AsyncRelayCommand(() => { return Task.CompletedTask; });
        public ICommand OpenDataFileCmd => new AsyncRelayCommand(() => { return Task.CompletedTask; });
    }

}
