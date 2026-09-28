namespace SunamoWpf;

public partial class PicturesSunamo
{
    public static System.Drawing.Image ImageResize(System.Drawing.Image image, int width, int height)
    {
        var bmp = new System.Drawing.Bitmap(width, height);
        using (var g = System.Drawing.Graphics.FromImage(bmp))
        {
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(image, 0, 0, width, height);
        }
        return bmp;
    }

    public static List<string> GetPicturesFiles(string path)
    {
        var masc = string.Join(";", AllLists.BasicImageExtensions);
        return Directory.GetFiles(path, masc, SearchOption.TopDirectoryOnly).ToList();
    }


}