namespace SunamoWpf;

public partial class PicturesSunamo
{
    private static List<string> s_supportedExtensionForResize = new List<string> {
        "png", "jpg", "jpeg", "gif"
    };
    static Type type = typeof(PicturesSunamo);
    /// <summary>
    /// Vypočte optimální šířku v případě že obrázek je postaven na výšku.
    /// </summary>
    /// <param name="p"></param>
    /// <param name="p_2"></param>
    /// <param name="p_3"></param>
    public static System.Windows.Size CalculateOptimalSizeHeight(int width, int height, int maxHeight)
    {
        System.Windows.Size vr = new System.Windows.Size(width, height);
        int vyskaSloupce = maxHeight;
        if (height > vyskaSloupce)
        {
            vr.Height = vyskaSloupce;
            // mohl by ses ještě rozhodovat jestli round, nebo floor, nebo ceil
            vr.Width = vyskaSloupce * width / height;
        }
        return vr;
    }
    public static bool GetImageFormatFromExtension1(string filePath, out string ext)
    {
        ext = Path.GetExtension(filePath).TrimStart('.').ToLower();
        if (PicturesSunamo.IsSupportedResizeForExtension(ext))
        {
            return true;
        }
        return false;
    }
    public static ImageFormats GetImageFormatsFromExtension(string filePath)
    {
        string ext = Path.GetExtension(filePath).TrimStart('.').ToLower();
        return GetImageFormatsFromExtension2(ext);
    }
    private static bool IsSupportedResizeForExtension(string ext)
    {
        if (ext == "jpg")
        {
            ext = "jpeg";
        }
        for (int i = 0; i < s_supportedExtensionForResize.Count; i++)
        {
            if (s_supportedExtensionForResize[i] == ext)
            {
                return true;
            }
        }
        return false;
    }
    public static ImageFormats GetImageFormatsFromExtension2(string ext)
    {
        if (ext == "")
        {
            return ImageFormats.None;
        }
        if (!IsSupportedResizeForExtension(ext))
        {
            return ImageFormats.None;
        }
        return (ImageFormats)Enum.Parse(typeof(ImageFormats), ext, true);
    }
    public static System.Windows.Size CalculateOptimalSize(int width, int height, int maxWidth)
    {
        return CalculateOptimalSizeWpf(width, height, maxWidth);
    }
    public static System.Windows.Size CalculateOptimalSizeWpf(double width, double height, int maxWidth)
    {
        System.Windows.Size vr = new System.Windows.Size(width, height);
        int sirkaSloupce = maxWidth;
        if (width > sirkaSloupce)
        {
            vr.Width = sirkaSloupce;
            // mohl by ses ještě rozhodovat jestli round, nebo floor, nebo ceil
            vr.Height = sirkaSloupce * height / width;
        }
        return vr;
    }
}