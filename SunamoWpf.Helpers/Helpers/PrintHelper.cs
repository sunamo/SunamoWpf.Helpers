namespace SunamoWpf.Helpers;

public enum FormatOfPaper
{
    A,
    B,
    C
}
public enum LengthUnit
{
    Mm,
    In
}
public class PrintHelper
{
    public static Size GetPixelSizeForPaper(int dpiXPrinter, int dpiYPrinter, FormatOfPaper fp, int size, LandscapePortraitWpf lp)
    {
        Size sizeInOfPaper = SizeOfPaper.GetPaperSize(fp.ToString() + size, LengthUnit.In, lp);
        sizeInOfPaper = SizeH.Multiply(sizeInOfPaper, dpiXPrinter, dpiYPrinter);
        return SizeH.Divide(sizeInOfPaper, 2);
    }
}
public static class SizeOfPaper
{
    const double mmInInch = 25.4d;
    /// <summary>
    /// V režimu Portrait pouze
    /// </summary>
    static Dictionary<string, Size> papersInMm = new Dictionary<string, Size>();
    static SizeOfPaper()
    {
        papersInMm.Add("A4", new Size(210, 297));
    }
    static Type type = typeof(PrintHelper);
    public static Size GetPaperSize(string a4, LengthUnit lu, LandscapePortraitWpf lp)
    {
        if (papersInMm.ContainsKey(a4))
        {
            Size vr = papersInMm[a4];
            if (lp == LandscapePortraitWpf.Landscape)
            {
                vr = new Size(vr.Height, vr.Width);
            }
            if (lu == LengthUnit.Mm)
            {
                return vr;
            }
            else if (lu == LengthUnit.In)
            {
                return SizeH.Divide(vr, mmInInch);
            }
        }
        else
        {
        }
        ThrowEx.Custom(Translate.FromKey(XlfKeys.NISizeOfPaperGetPaperSize) + "()");
        return Size.Empty;
    }
}