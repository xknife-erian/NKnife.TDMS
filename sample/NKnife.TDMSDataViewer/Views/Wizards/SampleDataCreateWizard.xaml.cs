using System.Globalization;
using System.Windows;
using System.Windows.Data;
using NKnife.TDMSDataViewer.ViewModels.Wizards;
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
            Pages.Add($"{nameof(_FilePropertiesPage_)}", _FilePropertiesPage_);
            Pages.Add($"{nameof(_DataStructurePage_)}", _DataStructurePage_);
            Pages.Add($"{nameof(_GroupPropertiesPage_)}", _GroupPropertiesPage_);
            Pages.Add($"{nameof(_ChannelPropertiesPage_)}", _ChannelPropertiesPage_);
            Pages.Add($"{nameof(_EditDataFormatSettingsPage_)}", _EditDataFormatSettingsPage_);
            Pages.Add($"{nameof(_ResultConfirmationPage_)}", _ResultConfirmationPage_);
            Pages.Add($"{nameof(_BuildProgressPage_)}", _BuildProgressPage_);
            Pages.Add($"{nameof(_EndPage_)}", _EndPage_);
        }
    }

    internal class PageName2PageCvt : IValueConverter
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