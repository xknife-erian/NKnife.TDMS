using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NKnife.TDMSDataViewer.Views;

internal class DocksTemplateSelector : DataTemplateSelector
{
    public override DataTemplate? SelectTemplate(object? item, DependencyObject container)
    {
        if (item is ObservableObject viewModel)
        {
            string viewModelName = viewModel.GetType().Name;
            if (DockTypes.PaneModels.TryGetValue(viewModelName, out var viewType))
            {
                if (container is FrameworkElement element)
                {
                    return new DataTemplate { VisualTree = new FrameworkElementFactory(viewType) };
                }
            }
        }
        return null;
    }
}