using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Xceed.Wpf.Toolkit;

namespace NKnife.TDMSDataViewer.Views.Wizards
{
    /// <summary>
    ///     SampleDataCreateWizard.xaml 的交互逻辑
    /// </summary>
    public partial class SampleDataCreateWizard : Window
    {
        public static Dictionary<string, WizardPage> Pages { get; } = new();
        public SampleDataCreateWizard()
        {
            InitializeComponent();
            Pages.Clear();
            Pages.Add($"{nameof(_WelcomePage_)}", _WelcomePage_);
            Pages.Add($"{nameof(_EndPage_)}", _EndPage_);
            Pages.Add($"{nameof(_FilePropertyPage_)}", _FilePropertyPage_);
            Pages.Add($"{nameof(_DataStructuredTreePage_)}", _DataStructuredTreePage_);
            Pages.Add($"{nameof(_GroupPropertyPage_)}", _GroupPropertyPage_);
            Pages.Add($"{nameof(_ChannelPropertyPage_)}", _ChannelPropertyPage_);
        }
    }

    internal class CurrentPageName2PageCvt : IValueConverter
    {
        #region Implementation of IValueConverter
        /// <inheritdoc />
        public object? Convert(object? value,
                               Type targetType,
                               object? parameter,
                               CultureInfo culture)
        {
            if(value is string valueStr
               && !string.IsNullOrEmpty(valueStr))
            {
                if(SampleDataCreateWizard.Pages.TryGetValue(valueStr, out var wizardPage))
                    return wizardPage;
            }

            return null;
        }

        /// <inheritdoc />
        public object? ConvertBack(object? value,
                                   Type targetType,
                                   object? parameter,
                                   CultureInfo culture)
        {
            if(value is WizardPage wizardPage)
                return wizardPage.Name;

            return value?.ToString();
        }
        #endregion
    }
}