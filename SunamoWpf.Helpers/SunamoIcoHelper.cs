namespace SunamoWpf;

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

/// <summary>
/// Converts a System.Drawing.Image into a System.Drawing.Icon (.ico) in memory.
/// </summary>
public class SunamoIcoHelper
{
    /// <summary>
    /// Low quality conversion via Icon.FromHandle - currently a stub, kept for API compatibility with the original source.
    /// </summary>
    public static Icon ConvertImageToIco(System.Drawing.Image bitmap)
    {
        Icon icon = null;

        //icon = Icon.FromHandle(bitmap.GetHicon());
        return icon;
    }

    /// <summary>
    /// Attempts to save the image directly as an ICO-format stream. Kept for API compatibility;
    /// the original source notes this does not work ("Value cannot be null. (Parameter 'encoder')").
    /// </summary>
    public static Icon ConvertToIcon(System.Drawing.Image image)
    {
        MemoryStream memoryStream = new MemoryStream();
        image.Save(memoryStream, ImageFormat.Icon);
        return new Icon(memoryStream);
    }

    /// <summary>
    /// High quality conversion: builds a single-image .ico container (PNG payload) in memory from the given image.
    /// </summary>
    public static Icon IconFromImage(System.Drawing.Image image)
    {
        MemoryStream memoryStream = new MemoryStream();
        BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
        // Header
        binaryWriter.Write((short)0); // 0 : reserved
        binaryWriter.Write((short)1); // 2 : 1=ico, 2=cur
        binaryWriter.Write((short)1); // 4 : number of images
        // Image directory
        int width = image.Width;
        if (width >= 256)
        {
            width = 0;
        }

        binaryWriter.Write((byte)width); // 0 : width of image
        int height = image.Height;
        if (height >= 256)
        {
            height = 0;
        }

        binaryWriter.Write((byte)height); // 1 : height of image
        binaryWriter.Write((byte)0); // 2 : number of colors in palette
        binaryWriter.Write((byte)0); // 3 : reserved
        binaryWriter.Write((short)0); // 4 : number of color planes
        binaryWriter.Write((short)0); // 6 : bits per pixel
        long sizePosition = memoryStream.Position;
        binaryWriter.Write(0); // 8 : image size
        int dataStart = (int)memoryStream.Position + 4;
        binaryWriter.Write(dataStart); // 12: offset of image data
        // Image data
        image.Save(memoryStream, ImageFormat.Png);
        int imageSize = (int)memoryStream.Position - dataStart;
        memoryStream.Seek(sizePosition, SeekOrigin.Begin);
        binaryWriter.Write(imageSize);
        memoryStream.Seek(0, SeekOrigin.Begin);

        return new Icon(memoryStream);
    }

    /// <summary>
    /// High quality conversion: builds a single-image 32bpp .ico container (PNG payload) in memory from the given image.
    /// </summary>
    public static Icon ConvertToIco(System.Drawing.Image image)
    {
        int size = image.Width;

        Icon icon;
        using (MemoryStream imageStream = new MemoryStream())
        using (MemoryStream icoStream = new MemoryStream())
        {
            image.Save(imageStream, ImageFormat.Png);
            using (BinaryWriter binaryWriter = new BinaryWriter(icoStream))
            {
                binaryWriter.Write((short)0); //0-1 reserved
                binaryWriter.Write((short)1); //2-3 image type, 1 = icon, 2 = cursor
                binaryWriter.Write((short)1); //4-5 number of images
                binaryWriter.Write((byte)size); //6 image width
                binaryWriter.Write((byte)size); //7 image height
                binaryWriter.Write((byte)0); //8 number of colors
                binaryWriter.Write((byte)0); //9 reserved
                binaryWriter.Write((short)0); //10-11 color planes
                binaryWriter.Write((short)32); //12-13 bits per pixel
                binaryWriter.Write((int)imageStream.Length); //14-17 size of image data
                binaryWriter.Write(22); //18-21 offset of image data
                binaryWriter.Write(imageStream.ToArray()); // write image data
                binaryWriter.Flush();
                binaryWriter.Seek(0, SeekOrigin.Begin);
                icon = new Icon(icoStream);
            }
        }

        return icon;
    }
}