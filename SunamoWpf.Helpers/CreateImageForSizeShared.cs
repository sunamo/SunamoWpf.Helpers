namespace SunamoWpf._shared;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public class CreateImageForSizeShared
{

    public static void PlaceToCenter(string text, int width, int height, float fontSize, string saveToFolder)
    {
        Font font = new Font("Segoe UI", fontSize);
        Rectangle rect = new Rectangle(0, 0, width, height);

        var bmp = new Bitmap(width, height);

        var brush = System.Drawing.Brushes.Black;

        var gra = Graphics.FromImage(bmp);
        gra.SmoothingMode = SmoothingMode.AntiAlias;
        gra.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

        using (var sf = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
        })
        {
            gra.FillRectangle(ColorH.RandomLightBrush(RandomHelper.RandomEnum<ColorComponent>()).ToSystemDrawing(), rect);
            gra.DrawString(text, font, brush, new Rectangle(0, 0, bmp.Width, bmp.Height), sf);
        }

        var fn = FS.ReplaceIncorrectCharactersFile(SH.ShortForLettersCount(text, 100));
        var path = Path.Combine(saveToFolder, fn + ".jpg");
        FS.CreateUpfoldersPsysicallyUnlessThere(path);


        bmp.Save(path, ImageFormat.Jpeg);
    }

    public static void CreateSingleColorImageWithColor(int w, int h, string fn, SunamoColor c, string saveToFolder)
    {
        if (c != null && w != int.MinValue && h != int.MinValue)
        {
            Bitmap Bmp = new Bitmap(w, h);
            using (Graphics gfx = Graphics.FromImage(Bmp))
            using (SolidBrush brush = new SolidBrush(c.ToSystemDrawing()))
            {
                gfx.FillRectangle(brush, 0, 0, w, h);
            }
            Bmp.Save(Path.Combine(saveToFolder, fn + ".png"), ImageFormat.Png);
        }
    }
}