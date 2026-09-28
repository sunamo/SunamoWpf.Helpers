namespace SunamoWpf.Helpers.ControlsWithGet;

public class LabelHelper
{
    static Type type = typeof(LabelHelper);

    public static Label Get(ControlInitData d)
    {
        Label vr = new Label();
        ControlHelper.SetForeground(vr, d.foreground);
        vr.Content = ContentControlHelper.GetContent(d);
        vr.Foreground = d.foreground;
        if (d.OnClick != null)
        {
            ThrowEx.IsNotNull("d.OnClick", d.OnClick);
            //vr.MouseDown += d.OnClick;
        }
        vr.Tag = d.tag;
        vr.ToolTip = d.tooltip;
        return vr;
    }
}