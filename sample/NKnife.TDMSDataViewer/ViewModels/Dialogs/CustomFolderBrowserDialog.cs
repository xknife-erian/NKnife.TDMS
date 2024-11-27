using System.Windows;
using MvvmDialogs.FrameworkDialogs;
using MvvmDialogs.FrameworkDialogs.FolderBrowser;
using Ookii.Dialogs.Wpf;

namespace NKnife.TDMSDataViewer.ViewModels.Dialogs
{
    public class CustomFolderBrowserDialog : IFrameworkDialog
    {
        private readonly FolderBrowserDialogSettings _settings;
        private readonly VistaFolderBrowserDialog _folderBrowserDialog;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFolderBrowserDialog"/> class.
        /// </summary>
        /// <param name="settings">The settings for the folder browser dialog.</param>
        public CustomFolderBrowserDialog(FolderBrowserDialogSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            _folderBrowserDialog = new VistaFolderBrowserDialog
            {
                Description            = settings.Description,
                SelectedPath           = settings.SelectedPath,
                UseDescriptionForTitle = string.IsNullOrEmpty(settings.Description),
                ShowNewFolderButton    = settings.ShowNewFolderButton
            };
        }

        /// <summary>
        /// Opens a folder browser dialog with specified owner.
        /// </summary>
        /// <param name="owner">
        /// Handle to the window that owns the dialog.
        /// </param>
        /// <returns>
        /// true if user clicks the OK button; otherwise false.
        /// </returns>
        public bool? ShowDialog(Window owner)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));

            var result = _folderBrowserDialog.ShowDialog(owner);

            // Update settings
            _settings.SelectedPath = _folderBrowserDialog.SelectedPath;

            return result;
        }
    }
}