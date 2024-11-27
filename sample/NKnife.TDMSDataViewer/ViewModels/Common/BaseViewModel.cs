using System.Collections.Specialized;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NKnife.TDMSDataViewer.ViewModels.Common
{
    abstract class BaseViewModel : ObservableObject, IViewModel
    {
    }

    public interface IViewModel : INotifyPropertyChanged, INotifyPropertyChanging
    {

    }
}