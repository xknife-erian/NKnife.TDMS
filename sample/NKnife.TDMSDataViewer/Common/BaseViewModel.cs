using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NKnife.TDMSDataViewer.Common
{
    abstract class BaseViewModel : ObservableObject, IViewModel
    {
    }

    public interface IViewModel : INotifyPropertyChanged, INotifyPropertyChanging
    {

    }
}