using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace NKnife.TDMSDataViewer.Views.Wizards
{
    /// <summary>
    /// LevelPropertiesControl.xaml 的交互逻辑
    /// </summary>
    public partial class LevelPropertiesControl : UserControl
    {
        public LevelPropertiesControl()
        {
            InitializeComponent();
        }
    }

    class IsFileInfo2VisibilityCvt : IValueConverter
    {
        #region Implementation of IValueConverter
        /// <inheritdoc />
        public object? Convert(object? value,
                               Type targetType,
                               object? parameter,
                               CultureInfo culture)
        {
            return value is bool and true ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc />
        public object? ConvertBack(object? value,
                                   Type targetType,
                                   object? parameter,
                                   CultureInfo culture)
        {
            throw new NotSupportedException();
        }
        #endregion
    }

    class Collection2ListVisibilityCvt : IValueConverter
    {
        #region Implementation of IValueConverter
        /// <inheritdoc />
        public object? Convert(object? value,
                               Type targetType,
                               object? parameter,
                               CultureInfo culture)
        {
            return value is int count ? count <= 0 ? Visibility.Hidden : Visibility.Visible : Visibility.Hidden;
        }

        /// <inheritdoc />
        public object? ConvertBack(object? value,
                                   Type targetType,
                                   object? parameter,
                                   CultureInfo culture)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}