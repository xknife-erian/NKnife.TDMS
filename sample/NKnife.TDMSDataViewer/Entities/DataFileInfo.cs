using System.Collections.ObjectModel;

namespace NKnife.TDMSDataViewer.Entities
{
    class DataFileInfo
    {
        public LevelPropertiesSet FileProperties { get; set; } = new();
        public ObservableCollection<LevelPropertiesSet> Groups { get; set; } = new();
    }
}