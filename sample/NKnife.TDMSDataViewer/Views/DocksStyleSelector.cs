using System.Windows;
using System.Windows.Controls;
using NKnife.TDMSDataViewer.ViewModels;

namespace NKnife.TDMSDataViewer.Views
{
    internal class DocksStyleSelector : StyleSelector
    {
        public Style? DocumentPaneStyle { get; set; }

        public Style? ToolPaneStyle { get; set; }

        public override Style? SelectStyle(object item, DependencyObject container)
        {
            return item switch
            {
                BaseDocumentViewModel => DocumentPaneStyle,
                BaseToolViewModel     => ToolPaneStyle,
                _                     => base.SelectStyle(item, container)
            };
        }
    }
}