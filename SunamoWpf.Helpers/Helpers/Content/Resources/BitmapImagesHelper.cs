namespace SunamoWpf.Helpers.Content.Resources;

/// <summary>
/// Obsahuje pouze jednu jedinou metodu public static BitmapImage MsAppx(bool enabled, AppPics appPic), která se bude používat když budu mít obrázky na světlém pozadí - původně to bylo vymyšleno pro tmavé pozadí a tak to teď vypadá že neaktivní jsou aktivní a vice-versa.
/// Ö vše ostatní se stará BitmapImageHelper(bez s)
/// Posloupnost je BitmapImage (sealed) -> BitmapSource (abstract) -> ImageSource (abstract)
/// </summary>
public static class BitmapImagesHelper
{
    public static BitmapImage MsAppx(bool enabled, AppPics appPic)
    {
        string cesta = "";
        if (enabled)
        {
            cesta = "//d/";
        }
        else
        {
            cesta = "//e/";
        }
        cesta += appPic.ToString() + ".png";
        return BitmapImageHelper.MsAppx(cesta);
    }
}