namespace SunamoWpf.Helpers.StaticControls;

public class TextBlockDataHelper
{
    public static TextBlockDataCompare GetForCompare(decimal value1, decimal value2)
    {
        TextBlockDataCompare vr = new TextBlockDataCompare();

        if (value1 > value2)
        {

            vr.fg = Brushes.Green;

            vr.fg2 = Brushes.Red;


            vr.text = " > ";
        }
        else if (value2 > value1)
        {

            vr.fg = Brushes.Red;

            vr.fg2 = Brushes.Green;


            vr.text = " < ";
        }
        else
        {
            vr.text = " = ";
        }

        return vr;
    }
}