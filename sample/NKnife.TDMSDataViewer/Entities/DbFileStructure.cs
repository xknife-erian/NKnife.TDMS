using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using NKnife.TDMS.Common;

namespace NKnife.TDMSDataViewer.Entities
{
    class DbFileStructure : ObservableObject
    {
        public LevelPropertiesSet Properties
        {
            get;
            set => SetProperty(ref field, value);
        } = new();

        public ObservableCollection<Group> Groups
        {
            get;
            set => SetProperty(ref field, value);
        } = new();
    }

    class Group : ObservableObject
    {
        public LevelPropertiesSet Properties
        {
            get;
            set => SetProperty(ref field, value);
        } = new();

        public ObservableCollection<Channel> Channels { get; set; } = new();
    }

    class Channel : ObservableObject
    {
        public LevelPropertiesSet Properties
        {
            get;
            set => SetProperty(ref field, value);
        } = new();

        public ObservableCollection<Data> Datas { get; set; } = new();
    }

    class Data : ObservableObject
    {
        public TDMSDataType TDMSDataType
        {
            get;
            set => SetProperty(ref field, value);
        }

        public uint Count
        {
            get;
            set => SetProperty(ref field, value);
        }

        public uint MinValue
        {
            get;
            set => SetProperty(ref field, value);
        }

        public uint MaxValue
        {
            get;
            set => SetProperty(ref field, value);
        }
    }
}