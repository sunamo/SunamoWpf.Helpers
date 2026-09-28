namespace SunamoWpf.Helpers.ControlsWithGet.Base;

public  abstract partial class ImageHelperBase<ImageSource, ImageControl>
{
    public abstract ImageControl ReturnImage(ImageSource bs);
    public abstract ImageControl ReturnImage(ImageSource bs, double width, double height);
}