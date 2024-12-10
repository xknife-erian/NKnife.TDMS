using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NKnife.TDMSDataViewer.Entities;

namespace NKnife.TDMSDataViewer.ViewModels.Wizards
{
    public class LevelPropertiesControlViewModel : ObservableObject
    {
        public LevelPropertiesControlViewModel(PropertiesType propertiesType)
        {
            PropertiesType = propertiesType;
            Name           = new LevelProperty() { Name = "Name" };
            Description    = new LevelProperty() { Name = "Description" };
            Title          = new LevelProperty() { Name = "Title" };
            Author         = new LevelProperty() { Name = "Author" };
            Unit           = new LevelProperty() { Name = "Unit" };
            Max            = new LevelProperty() { Name = "Max" };
            Min            = new LevelProperty() { Name = "Min" };
        }

        public PropertiesType PropertiesType { get; set; }

        public LevelProperty Name
        {
            get;
            set => SetProperty(ref field, value);
        }
        public LevelProperty Description
        {
            get;
            set => SetProperty(ref field, value);
        }
        public LevelProperty Title
        {
            get;
            set => SetProperty(ref field, value);
        }
        public LevelProperty Author
        {
            get;
            set => SetProperty(ref field, value);
        }
        public LevelProperty Unit
        {
            get;
            set => SetProperty(ref field, value);
        }
        public LevelProperty Max
        {
            get;
            set => SetProperty(ref field, value);
        }
        public LevelProperty Min
        {
            get;
            set => SetProperty(ref field, value);
        }

        public LevelPropertiesSet LevelPropertiesSet { get; set; } = new();

        public ICommand AddPropertyCommand => new RelayCommand(() =>
        {
            LevelPropertiesSet.Add(new LevelProperty() { Name = string.Empty, Value = string.Empty });
        });

        public ICommand RemovePropertyCommand => new RelayCommand(() =>
        {
            if(SelectValueIndex >= 0
               && SelectValueIndex < LevelPropertiesSet.Count)
                LevelPropertiesSet.RemoveAt(SelectValueIndex);
        });

        public int SelectValueIndex
        {
            get;
            set => SetProperty(ref field, value);
        }
    }
}