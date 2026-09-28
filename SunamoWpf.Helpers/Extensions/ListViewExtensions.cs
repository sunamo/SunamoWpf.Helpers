namespace SunamoWpf.Extensions;

public static class ListViewExtensions
{
    public static bool validated
    {
        get => ValidationHelper.validated;
        set => ValidationHelper.validated = value;
    }
}