namespace SunamoWpf.Helpers.ControlsWithGet;

public class ButtonHelperDesktop2
{
    public static void PerformClick(ButtonBase someButton)
    {
        someButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
    }
}