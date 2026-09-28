namespace SunamoWpf.Extensions;

public static class SizeExtensions
{
    public static Size RecalculateSizeWithScaleFactor(this Size s)
    {
        var scaleFactor = DisplayHelper.GetScaleFactor();
        var size = new Size(s.Width / scaleFactor, s.Height / scaleFactor);
        return size;
    }


}