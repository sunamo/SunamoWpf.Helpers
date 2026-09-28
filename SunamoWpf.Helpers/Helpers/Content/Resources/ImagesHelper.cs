namespace SunamoWpf.Helpers.Content.Resources;

public static class ImagesHelper
{
    // Tato třída je zde, abych když budu konvertovat nějaký kód z WPF do WF, tak přidám pouze s do ImageHelper(vznikne ImagesHelper) a na levé straně upravím, aby se to načítalo přímo do Image místo do Content. Snad si tímto ušetřím trochu práce
    public static Image MsAppx(bool enabled, AppPics appPic)
    {
        ///Subfolder/ResourceFile.xaml
        return ImageHelper.ReturnImage(BitmapImagesHelper.MsAppx(enabled, appPic));
    }
}