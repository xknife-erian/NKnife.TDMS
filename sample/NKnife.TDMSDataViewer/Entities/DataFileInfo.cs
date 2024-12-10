namespace NKnife.TDMSDataViewer.Entities
{
    class DataFileInfo
    {
        public LevelPropertiesSet FileProperties { get; set; } = new();
        public Dictionary<string, LevelPropertiesSet> GroupPropertiesList { get; set; } = new();
        public Dictionary<string, LevelPropertiesSet> ChannelPropertiesList { get; set; } = new();
    }
}