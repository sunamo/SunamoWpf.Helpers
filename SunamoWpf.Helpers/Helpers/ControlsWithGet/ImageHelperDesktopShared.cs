namespace SunamoWpf.Helpers.ControlsWithGet;

public partial class ImageHelperDesktop : ImageHelperBase<ImageSource, Image>
{
    static Type type = typeof(ImageHelperDesktop);

public static Image Get(object imagePathOrBitmapImage)
    {
        var t = imagePathOrBitmapImage.GetType();
        BitmapImage bi = null;
        if (t == TypesDesktop.tBitmapImage)
        {
            bi = (BitmapImage)imagePathOrBitmapImage;
        }
        else if (t == Types.tString)
        {
            bi = BitmapImageHelper.PathToBitmapImage(imagePathOrBitmapImage.ToString());
        }
        else
        {
            ThrowEx.NotImplementedCase(t);
        }

        Image img = ImageHelper.ReturnImage(bi);
        return img;
    }

public override Image ReturnImage(ImageSource bs)
    {
        Image image = new Image();
        image.Stretch = Stretch.Uniform;
        image.Source = bs;
        return image;
    }
public override Image ReturnImage(ImageSource bs, double width, double height)
    {
        Image image = new Image();
        image.Stretch = Stretch.Uniform;
        image.Source = bs;
        image.Width = width;
        image.Height = height;
        return image;
    }
}