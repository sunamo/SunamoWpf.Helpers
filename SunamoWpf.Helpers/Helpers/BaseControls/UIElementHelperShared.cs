namespace SunamoWpf.Helpers.BaseControls;

public partial class UIElementHelper
{
    public static void SetVisibility(bool v, params UIElement[] elements)
    {
        foreach (var item in elements)
        {
            item.Visibility = v ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
