using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NKnife.TDMSDataViewer.ViewModels
{
    internal class WorkbenchViewModel : BaseViewModel
    {
        public BaseDocumentViewModel? ActivePaneViewModel { get; set; }
        public ObservableCollection<BaseToolViewModel> Tools { get; set; } = new();
        public ObservableCollection<BaseDocumentViewModel> Documents { get; set; } = new();
    }

    class BaseDocumentViewModel : BaseViewModel
    {

    }

    class BaseToolViewModel : BaseViewModel
    {

    }

    class BaseViewModel : ObservableObject
    {

    }
}
