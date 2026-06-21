namespace SunamoWpf.Helpers;

public class FontWeightHelper
{
    public static FontWeight FromEnum(SunamoWpf.Enums.FontWeights fw)
    {
        return FontWeight.FromOpenTypeWeight((int)fw);
    }
}