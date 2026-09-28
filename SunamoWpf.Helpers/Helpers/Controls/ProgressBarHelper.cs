namespace SunamoWpf.Helpers.Controls;

public class ProgressBarHelper
{
    ProgressBar pb = null;
    PercentCalculator percentCalculator;
    UIElement ui = null;
    public ProgressBarHelper CreateInstance(object pb, double overall)
    {
        return new ProgressBarHelper(pb, overall, ui);
    }
    public ProgressBarHelper(object pb, double overall, object ui)
    {
        var pb2 = (ProgressBar)pb;
        var ui2 = (DispatcherObject)ui;
        this.pb = pb2;
        this.ui = pb2;
        ui2.Dispatcher.Invoke(IH.delegateUpdateProgressBarWpf, pb, 0d);
        ui2.Dispatcher.Invoke(IH.delegateChangeVisibilityUIElementWpf, pb, Visibility.Visible);
        percentCalculator = new PercentCalculator(overall);
    }
    public void Done()
    {
        ui.Dispatcher.Invoke(IH.delegateUpdateProgressBarWpf, pb, 100d);
        ui.Dispatcher.Invoke(IH.delegateChangeVisibilityUIElementWpf, pb, Visibility.Collapsed);
    }
    public void DonePartially()
    {
        percentCalculator.last += percentCalculator.onePercent;
        ui.Dispatcher.Invoke(IH.delegateUpdateProgressBarWpf, pb, percentCalculator.last);
    }
}