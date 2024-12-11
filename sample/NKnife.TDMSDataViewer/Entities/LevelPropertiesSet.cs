using System.Collections.ObjectModel;

namespace NKnife.TDMSDataViewer.Entities
{
    public class LevelPropertiesSet : ObservableCollection<LevelProperty>
    {
        public ObservableCollection<LevelPropertiesSet> Children { get; set; } = new();
    }
}