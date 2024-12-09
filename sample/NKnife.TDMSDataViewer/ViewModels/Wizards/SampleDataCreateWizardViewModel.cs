using MvvmDialogs;
using NKnife.TDMSDataViewer.Common;

namespace NKnife.TDMSDataViewer.ViewModels.Wizards
{
    internal class SampleDataCreateWizardViewModel : BaseViewModel, IModalDialogViewModel
    {
        public LevelInfoControlViewModel FileInfo { get; init; } = new(true);
        public DataStructuredTreeControlViewModel DataStructuredTree { get; init; } = new();

        /// <inheritdoc />
        public bool? DialogResult { get; } = null;
    }
}
