namespace SunamoWpf;

public class FontHelper
{
    public static List<string> DivideStringToRows(FontFamily fontFamily, double fontSize, FontStyle fontStyle, FontStretch fontStretch, System.Windows.FontWeight fontWeight, string text, Size maxSize)
    {
        FontArgs fa = new FontArgs(fontFamily, fontSize, fontStyle, fontStretch, fontWeight);
        List<string> l = SHWithControls.DivideStringToRowsList(fontFamily, fontSize, fontStyle, fontStretch, fontWeight, text, maxSize);
        return l;
    }
}