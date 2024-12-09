using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace NKnife.TDMSDataViewer.ViewModels.Wizards
{
    public class LevelPropertiesControlViewModel(bool __isFileInfo) : ObservableObject
    {
        public bool IsFileInfo { get; set; } = __isFileInfo;

        public ObservableCollection<LevelProperty> ValueTuples { get; set; } = new();

        public ICommand AddPropertyCommand => new RelayCommand(() =>
        {
            ValueTuples.Add(new LevelProperty() { Name = string.Empty, Value = string.Empty });
        });

        public ICommand RemovePropertyCommand => new RelayCommand(() =>
        {
            if(SelectValueIndex >= 0
               && SelectValueIndex < ValueTuples.Count)
                ValueTuples.RemoveAt(SelectValueIndex);
        });

        public int SelectValueIndex
        {
            get;
            set => SetProperty(ref field, value);
        }
    }

    public class LevelProperty : ObservableObject
    {
        public string? Name
        {
            get;
            set => SetProperty(ref field, value);
        }

        public object? Value
        {
            get;
            set => SetProperty(ref field, value);
        }
    }
}
