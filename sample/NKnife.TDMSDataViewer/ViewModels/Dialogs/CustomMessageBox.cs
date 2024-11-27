using System.Windows;
using MvvmDialogs.FrameworkDialogs.MessageBox;
using Ookii.Dialogs.Wpf;

namespace NKnife.TDMSDataViewer.ViewModels.Dialogs;
public class CustomMessageBox : IMessageBox
{
    private readonly CustomMessageBoxSettings _settings;
    private readonly TaskDialog _messageBox;
    private readonly int _countDownNum = 3;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomMessageBox"/> class.
    /// </summary>
    /// <param name="settings">The settings for the folder browser dialog.</param>
    public CustomMessageBox(CustomMessageBoxSettings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        _messageBox = new TaskDialog
        {
            Content = settings.MessageBoxText,
        };

        SetUpTitle();
        SetUpButtons();
        SetUpIcon();
    }

    /// <summary>
    /// Opens a message box with specified owner.
    /// </summary>
    /// <param name="owner">
    /// Handle to the window that owns the dialog.
    /// </param>
    /// <returns>
    /// A <see cref="MessageBoxResult"/> value that specifies which message box button is
    /// clicked by the user.
    /// </returns>
    public MessageBoxResult Show(Window owner)
    {
        if (owner == null) throw new ArgumentNullException(nameof(owner));

        var result = _messageBox.ShowDialog(owner);
        return ToMessageBoxResult(result);
    }

    private void SetUpTitle()
    {
        _messageBox.WindowTitle = string.IsNullOrEmpty(_settings.Caption) ?
            " " :
            _settings.Caption;
    }

    private void SetUpButtons()
    {
        switch (_settings.Button)
        {
            case MessageBoxButton.OKCancel:
                _messageBox.Buttons.Add(new TaskDialogButton(ButtonType.Ok));
                _messageBox.Buttons.Add(new TaskDialogButton(ButtonType.Cancel));
                break;

            case MessageBoxButton.YesNo:
                _messageBox.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
                _messageBox.Buttons.Add(new TaskDialogButton(ButtonType.No));
                break;

            case MessageBoxButton.YesNoCancel:
                _messageBox.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
                _messageBox.Buttons.Add(new TaskDialogButton(ButtonType.No));
                _messageBox.Buttons.Add(new TaskDialogButton(ButtonType.Cancel));
                break;

            default:
                _messageBox.Buttons.Add(new TaskDialogButton(ButtonType.Ok));
                break;
        }

        if (_settings.ShowCountDown)
        {
            SetButtonCountDown(_settings.Button);
        }
    }

    private void SetButtonCountDown(MessageBoxButton messageBoxButton)
    {
        string            content;
        TaskDialogButton? yesButton;
        TaskDialogButton?  noButton = null;

        switch (messageBoxButton)
        {
            case MessageBoxButton.YesNo:
                content = "是(Y)";
                yesButton = _messageBox.Buttons.FirstOrDefault(c => c.ButtonType == ButtonType.Yes);
                noButton = _messageBox.Buttons.FirstOrDefault(c => c.ButtonType == ButtonType.No);
                break;
            case MessageBoxButton.YesNoCancel:
                content = "是(Y)";
                yesButton = _messageBox.Buttons.FirstOrDefault(c => c.ButtonType == ButtonType.Yes);
                noButton = _messageBox.Buttons.FirstOrDefault(c => c.ButtonType == ButtonType.Cancel);
                break;

            case MessageBoxButton.OKCancel:
                content = "确定";
                yesButton = _messageBox.Buttons.FirstOrDefault(c => c.ButtonType == ButtonType.Ok);
                noButton = _messageBox.Buttons.FirstOrDefault(c => c.ButtonType == ButtonType.Cancel);
                break;

            case MessageBoxButton.OK:
                content = "确定";
                yesButton = _messageBox.Buttons.FirstOrDefault(c => c.ButtonType == ButtonType.Ok);
                break;

            default:
                return;
        }

        if (yesButton == null)
            return;

        var originalButtonType = yesButton.ButtonType;
        yesButton.Enabled = false;
        yesButton.Default = false;
        yesButton.ButtonType = ButtonType.Custom;
        yesButton.Text = $"{content}({_countDownNum})";

        var timer = new System.Timers.Timer(1000);
        var secondsLeft = _countDownNum;

        timer.Elapsed += (_, e) =>
        {
            if (secondsLeft > 1)
            {
                secondsLeft--;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    yesButton.Text = $"{content}({secondsLeft})";
                });
            }
            else
            {
                timer.Stop();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    yesButton.Enabled = true;
                    yesButton.ButtonType = originalButtonType;
                    yesButton.Text = content;


                    if (noButton != null)
                    {
                        noButton.Default = true;
                    }
                });
            }
        };

        timer.Start();
    }

    private void SetUpIcon()
    {
        switch (_settings.Icon)
        {
            case MessageBoxImage.Error:
                _messageBox.MainIcon = TaskDialogIcon.Error;
                break;

            case MessageBoxImage.Information:
                _messageBox.MainIcon = TaskDialogIcon.Information;
                break;

            case MessageBoxImage.Warning:
                _messageBox.MainIcon = TaskDialogIcon.Warning;
                break;
            case MessageBoxImage.Question:
                _messageBox.MainIcon = TaskDialogIcon.Information;
                break;

            default:
                _messageBox.MainIcon = TaskDialogIcon.Custom;
                break;
        }
    }

    private static MessageBoxResult ToMessageBoxResult(TaskDialogButton button)
    {
        switch (button.ButtonType)
        {
            case ButtonType.Cancel:
                return MessageBoxResult.Cancel;

            case ButtonType.No:
                return MessageBoxResult.No;

            case ButtonType.Ok:
                return MessageBoxResult.OK;

            case ButtonType.Yes:
                return MessageBoxResult.Yes;

            default:
                return MessageBoxResult.None;
        }
    }
}
