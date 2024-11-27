using MvvmDialogs.FrameworkDialogs;
using MvvmDialogs.FrameworkDialogs.FolderBrowser;
using MvvmDialogs.FrameworkDialogs.MessageBox;
using MvvmDialogs.FrameworkDialogs.OpenFile;
using MvvmDialogs.FrameworkDialogs.SaveFile;

namespace NKnife.TDMSDataViewer.ViewModels.Dialogs;

public class CustomFrameworkDialogFactory : DefaultFrameworkDialogFactory
{
    public override IFrameworkDialog CreateOpenFileDialog(OpenFileDialogSettings settings)
    {
        return new CustomOpenFileDialog(settings);
    }

    public override IFrameworkDialog CreateSaveFileDialog(SaveFileDialogSettings settings)
    {
        return new CustomSaveFileDialog(settings);
    }

    public override IMessageBox CreateMessageBox(MessageBoxSettings settings)
    {
        if (settings is CustomMessageBoxSettings customSettings)
        {
            return new CustomMessageBox(customSettings);
        }

        var customMessageBoxSettings = new CustomMessageBoxSettings()
        {
            Options = settings.Options,
            Button = settings.Button,
            Caption = settings.Caption,
            DefaultResult = settings.DefaultResult,
            Icon = settings.Icon,
            MessageBoxText = settings.MessageBoxText
        };
        return new CustomMessageBox(customMessageBoxSettings);
    }

    /// <inheritdoc />
    public override IFrameworkDialog CreateFolderBrowserDialog(FolderBrowserDialogSettings settings)
    {
        return new CustomFolderBrowserDialog(settings);
    }
}


