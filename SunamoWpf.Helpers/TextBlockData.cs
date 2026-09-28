namespace SunamoWpf.Data;

public class TextBlockData
{
    public SolidColorBrush fg = Brushes.Black;
    public string text = "";
    public System.Windows.FontWeight fontWeight = FontWeightHelper.FromEnum(SunamoWpf.Enums.FontWeights.normal);

    public SunamoWpf.Enums.FontWeights fontWeight2
    {
        set
        {
            FontWeightHelper.FromEnum(value);
        }
    }
}

public class TextBlockDataCompare : TextBlockData
{
    public SolidColorBrush fg2 = Brushes.Black;
}