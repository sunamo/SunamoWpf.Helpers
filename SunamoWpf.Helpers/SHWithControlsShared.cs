namespace SunamoWpf;

public static partial class SHWithControls
{
    static TextBlock tb = new TextBlock();

    public static double MeasureString(FontFamily fontFamily, double fontSize, System.Windows.FontStyle fontStyle, FontStretch fontStretch, System.Windows.FontWeight fontWeight, string text, Size maxSize)
    {
        //tb.Height = Double.PositiveInfinity;
        tb.FontFamily = fontFamily;
        tb.FontSize = fontSize;
        tb.FontStyle = fontStyle;
        tb.FontStretch = fontStretch;
        tb.FontWeight = fontWeight;
        tb.Text = text;
        tb.Measure(maxSize);
        //tb.Arrange(new Rect(new Point(0, 0), maxSize));
        return tb.DesiredSize.Width;
    }

    public static object MeasureString(FormattedText f)
    {
        //tb.FontFamily = fontFamily;
        //tb.FontSize = f.;
        tb.FontStyle = FontStyles.Normal;
        //tb.FontStretch = FontStretch;
        tb.FontWeight = System.Windows.FontWeights.Normal;
        tb.Text = f.Text;
        tb.Measure(ControlHelper.SizePositiveInfinity);
        //tb.Arrange(new Rect(new Point(0, 0), maxSize));
        return tb.DesiredSize.Width;
    }
}