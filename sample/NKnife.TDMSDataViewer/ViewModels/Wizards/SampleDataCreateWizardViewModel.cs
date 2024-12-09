using MvvmDialogs;
using NKnife.TDMSDataViewer.Common;

namespace NKnife.TDMSDataViewer.ViewModels.Wizards
{
    internal class SampleDataCreateWizardViewModel : BaseViewModel, IModalDialogViewModel
    {
        public SampleDataCreateWizardViewModel()
        {
            DataStructuredTree = new(this);
        }

        public string? PageName
        {
            get;
            set => SetProperty(ref field, value);
        } = "_WelcomePage_";

        public LevelPropertiesControlViewModel FileProperties { get; init; } = new(true);
        public LevelPropertiesControlViewModel GroupProperties { get; init; } = new(true);
        public LevelPropertiesControlViewModel ChannelProperties { get; init; } = new(true);
        public DataStructuredTreeControlViewModel DataStructuredTree { get; init; }

        /// <inheritdoc />
        public bool? DialogResult { get; } = null;
    }
}
