using System.Windows;
using System.Windows.Input;
using Xceed.Wpf.AvalonDock.Layout;

namespace NKnife.TDMSDataViewer.Views
{
    /// <summary>
    /// Interaction logic for Workbench.xaml
    /// </summary>
    public partial class Workbench : Window
    {
        public Workbench()
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