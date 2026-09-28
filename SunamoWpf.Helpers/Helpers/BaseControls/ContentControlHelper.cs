namespace SunamoWpf.Helpers.BaseControls;

public partial class ContentControlHelper
{
    public static T CastTo<T>(object o) where T : class
    {
        if (o is T)
        {
            return (T)o;
        }
        var cc = (ContentControl)o;
        return cc.Content as T;

    }

    
}