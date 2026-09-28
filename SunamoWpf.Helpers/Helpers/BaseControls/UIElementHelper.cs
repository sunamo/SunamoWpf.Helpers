namespace SunamoWpf.Helpers.BaseControls;

public partial class UIElementHelper
{
    public static void SetIsEnabled(bool v, params UIElement[] elements)
    {
        foreach (var item in elements)
        {
            item.IsEnabled = v;
        }
    }

}