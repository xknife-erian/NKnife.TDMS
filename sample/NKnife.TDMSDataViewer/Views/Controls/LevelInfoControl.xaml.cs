using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NKnife.TDMSDataViewer.Views.Controls
{
    /// <summary>
    /// LevelInfoControl.xaml 的交互逻辑
    /// </summary>
    public partial class LevelInfoControl : UserControl
    {
        public LevelInfoControl()
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