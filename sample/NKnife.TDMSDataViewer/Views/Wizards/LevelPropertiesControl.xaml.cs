using NKnife.TDMSDataViewer.ViewModels.Wizards;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using NKnife.TDMSDataViewer.Entities;

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

    class PropertiesType2FileVisibilityCvt : IValueConverter
    {
        #region Implementation of IValueConverter
        /// <inheritdoc />
        public object? Convert(object? value,
                               Type targetType,
                               object? parameter,
                               CultureInfo culture)
        {
            return value is PropertiesType and PropertiesType.File ? Visibility.Visible : Visibility.Collapsed;
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

    class PropertiesType2ChannelVisibilityCvt : IValueConverter
    {
        #region Implementation of IValueConverter
        /// <inheritdoc />
        public object? Convert(object? value,
                               Type targetType,
                               object? parameter,
                               CultureInfo culture)
        {
            return value is PropertiesType and PropertiesType.Channel ? Visibility.Visible : Visibility.Collapsed;
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

    class Collection2VisibilityCvt : IValueConverter
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