using System.Windows;
using System.Windows.Input;
using Xceed.Wpf.AvalonDock.Layout;

namespace NKnife.TDMSDataViewer.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnPreviewMouseRightDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is not FrameworkElement frameworkElement)
                return;

            switch (frameworkElement.DataContext)
            {
                case LayoutDocument layoutDocument:
                    layoutDocument.IsSelected = true;
                    layoutDocument.IsActive   = true;

                    break;
                case LayoutAnchorable layoutAnchorable:
                    layoutAnchorable.IsSelected = true;
                    layoutAnchorable.IsActive   = true;

                    break;
            }
        }
    }
}