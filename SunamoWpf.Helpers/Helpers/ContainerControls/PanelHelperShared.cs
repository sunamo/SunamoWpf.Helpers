namespace SunamoWpf.Helpers.ContainerControls;

public partial class PanelHelper
{
    public static UIElementCollection Children(StackPanel key, Dispatcher d)
    {
        //WpfApp.cd

        var r = d.Invoke(() => key.Children, DispatcherPriority.ContextIdle);
        return r;

    }
}