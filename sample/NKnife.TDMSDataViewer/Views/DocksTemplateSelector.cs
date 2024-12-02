using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using NKnife.TDMSDataViewer.ViewModels;

namespace NKnife.TDMSDataViewer.Views;

internal class DocksTemplateSelector : DataTemplateSelector
{
    static DocksTemplateSelector()
    {
        PaneModels.Add(nameof(TDMSDataPaneViewModel), typeof(TDMSDataPane));
        PaneModels.Add(nameof(SampleDataCreateWizardViewModel), typeof(SampleDataCreateWizard));
    }

    /// <summary>
    ///     Key: ViewModel type name; Value: View type
    /// </summary>
    public static Dictionary<string, Type> PaneModels { get; set; } = new();

    public override DataTemplate? SelectTemplate(object? item, DependencyObject container)
    {
        if (item is ObservableObject viewModel)
        {
            string viewModelName = viewModel.GetType().Name;
            if (PaneModels.TryGetValue(viewModelName, out var viewType))
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