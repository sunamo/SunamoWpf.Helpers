namespace SunamoWpf.Helpers.ControlsWithGet;

public partial class TextBlockHelper{

    static Type type = typeof(TextBlockHelper);

    /// <summary>
    /// tag is not needed, value is obtained through []
    /// Tag here is mainly for comment what data control hold 
    /// </summary>
    /// <param name="text"></param>
    public static TextBlock Get(ControlInitData d)
    {
        TextBlock tb = new TextBlock();

        // TextBlock is not derived from Control, so have own property Foreground
        TextBlockHelper.SetForeground(tb, d.foreground);

        if (d.imagePath != null)
        {
            ThrowEx.IsNotNull("d.imagePath", d.imagePath);
        }

        tb.Tag = d.tag;
        tb.ToolTip = d.tooltip;
        tb.Text = d.text;

        return tb;
    }

    public static void SetText(TextBlock lblStatusDownload, string status)
    {
        if (lblStatusDownload != null)
        {
            // Must be invoke because after that I immediately load it on ListBox
            lblStatusDownload.Dispatcher.Invoke(() =>
            {
                lblStatusDownload.Text = status;
            }

            );
        }
    }

    /// <summary>
    /// A1 can be TextBlock or any object
    /// </summary>
    /// <param name = "tb"></param>
    public static string TextOrToString(object tb)
    {
        if (tb is TextBlock)
        {
            var tb2 = (TextBlock)tb;
            return tb2.Text;
        }

        return tb.ToString();
    }

    public static void SetForeground(TextBlock c, Brush fg)
    {
        if (fg != null)
        {
            c.Foreground = fg;
        }
    }
}