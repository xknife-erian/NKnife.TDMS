using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;
using NKnife.TDMSDataViewer.Common;

namespace NKnife.TDMSDataViewer.ViewModels.Wizards
{
    internal class SampleDataCreateWizardViewModel : BaseViewModel, IModalDialogViewModel
    {
        private string _lastPageName = "_WelcomePage_";

        public SampleDataCreateWizardViewModel()
        {
            PageName           = _lastPageName;
            DataStructuredTree = new(this);
        }

        public string PageName
        {
            get;
            set => SetProperty(ref field, value);
        }

        public LevelPropertiesControlViewModel FileProperties { get; init; } = new(PropertiesType.File);
        public LevelPropertiesControlViewModel GroupProperties { get; init; } = new(PropertiesType.Group);
        public LevelPropertiesControlViewModel ChannelProperties { get; init; } = new(PropertiesType.Channel);
        public DataStructuredTreeControlViewModel DataStructuredTree { get; init; }

        /// <inheritdoc />
        public bool? DialogResult { get; } = null;

        public void PageChanged()
        {
            switch (PageName)
            {
                case "_FilePropertyPage_":
                    DataFile.FileProperties.Clear();
                    DataFile.FileProperties.AddRange(FileProperties.ValueTuples);
                    break;
                case "_GroupPropertiesPage_":
                case "_ChannelPropertiesPage_":
                    break;
            }
            _lastPageName = PageName;
        }

        public DataFile DataFile { get; set; } = new();

    }


    class DataFile
    {
        public List<LevelProperty> FileProperties { get; set; } = new();
        public List<LevelProperty> GroupProperties { get; set; } = new();
        public List<LevelProperty> ChannelProperties { get; set; } = new();
    }
}
