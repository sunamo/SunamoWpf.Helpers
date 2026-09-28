namespace SunamoWpf.Helpers;

public class ControlFinder
{
    public static StackPanel StackPanel(FrameworkElement parent, string name)
    {
        return FindControl<StackPanel>(parent, name);
    }

    private static T FindControl<T>(FrameworkElement parent, string name) where T : UIElement
    {
        return (T)parent.FindName(name);
    }

    //public static T FindControlExclude<T>(FrameworkElement parent, params UIElement[] exclude) where T: UIElement
    //{
    //    //var d = VisualTreeHelpers.FindDescendents<UIElement>(parent);
    //    //var d3 = VisualTreeHelpers.FindDescendents3<UIElement>(parent);
    //    //var t = VisualTreeHelpers.GetLogicalChildCollection<UIElement>(parent);
    //    int i = 0;
    //    return default(T);
    //}
}