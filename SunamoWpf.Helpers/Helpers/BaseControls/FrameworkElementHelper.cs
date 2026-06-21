namespace SunamoWpf.Helpers.BaseControls;

public partial class FrameworkElementHelper
{
    static Type type = typeof(FrameworkElementHelper);
    public static object GetTagCheckBoxListUC(object o)
    {
        var fw = (FrameworkElement)o;
        var t = fw.Tag;
        var s = (FrameworkElementTag)t;
        return s.tagCheckBoxListUC;
    }
    public static int CountOfAncestor(FrameworkElement item)
    {
        var mi = item as MenuItem;
        int result = 0;
        while (true)
        {
            if (item.Parent == null)
            {
                break;
            }
            item = item.Parent as FrameworkElement;
            if (item == null)
            {
                break;
            }
            result++;
        }
        return result;
    }
    private static string HeaderOrName(FrameworkElement item)
    {
        var mi = item as MenuItem;
        if (mi != null)
        {
            if (mi.Name != null)
            {
                return mi.Name;
            }
            if (mi.Header != null)
            {
                return mi.Header.ToString();
            }
            return "(null)";
        }
        return "Not MI";
    }
    public static T CastTo<T>(FrameworkElement o) where T : class
    {
        T casted = default(T);
        //var casted2 = o as T;
        ScrollViewer sw;
        while (EqualityComparer<T>.Default.Equals(casted, default(T)))
        {
            if (o.Parent == null)
            {
                break;
            }
            o = (FrameworkElement)o.Parent;
            casted = o as T;
        }
        return casted;
    }
    public static Size GetMaxContentSize(FrameworkElement fe)
    {
        return new Size(fe.ActualWidth, fe.ActualHeight);
    }
    public static Size GetContentSize(FrameworkElement fe)
    {
        return new Size(fe.Width, fe.Height);
    }
    public static void SetMaxContentSize(FrameworkElement fe, Size s)
    {
        fe.MaxWidth = s.Width;
        fe.MaxHeight = s.Height;
        fe.Width = s.Width;
        fe.Height = s.Height;
    }
    public static void SetWidthAndHeight(FrameworkElement fe, Size s)
    {
        fe.Width = s.Width;
        fe.Height = s.Height;
        fe.UpdateLayout();
    }
    public static T FindName<T>(FrameworkElement element, string controlNamePrefix, int serie)
    {
        return FindName<T>(element, controlNamePrefix + serie);
    }
    static bool IsContentControl(object customControl)
    {
        if (RH.IsOrIsDeriveFromBaseClass(customControl.GetType(), typeof(ContentControl)))
        {
            var contentControl = (ContentControl)customControl;
            if (RH.IsOrIsDeriveFromBaseClass(contentControl.Content.GetType(), typeof(FrameworkElement)))
            {
                return true;
            }
        }
        return false;
    }
    static bool IsPanel(object customControl)
    {
        return RH.IsOrIsDeriveFromBaseClass(customControl.GetType(), typeof(Panel));
    }
    public static T FindByTag<T>(object customControl, object v)
        where T : FrameworkElement
    {
        if (IsContentControl(customControl))
        {
            ContentControl c = (ContentControl)customControl;
            return FindByTag<T>(c.Content, v);
        }
        else if (IsPanel(customControl))
        {
            Panel c = (Panel)customControl;
            foreach (var item in c.Children)
            {
                if (IsPanel(item) || IsContentControl(item))
                {
                    return FindByTag<T>(item, v);
                }
                if (RH.IsOrIsDeriveFromBaseClass(item.GetType(), typeof(FrameworkElement)))
                {
                    FrameworkElement fw = (FrameworkElement)item;
                    if (BTS.CompareAsObjectAndString(fw.Tag, v))
                    {
                        return (T)fw;
                    }
                }
            }
        }
        else
        {
            ThrowEx.Custom(Translate.FromKey(XlfKeys.customControlIsNotContentControlOrPanel));
        }
        return default(T);
    }
    /// <summary>
    /// Dont use Aligment for stretch / fill all available size.
    /// Width / Height = double.NaN work like a charm!
    /// </summary>
    /// <param name="g"></param>
    public static void AligmentStretch(Grid g)
    {
        g.HorizontalAlignment = HorizontalAlignment.Stretch;
        g.VerticalAlignment = VerticalAlignment.Stretch;
    }
    /// <summary>
    /// Dont use Aligment for stretch / fill all available size.
    /// Width / Height = double.NaN work like a charm!
    /// </summary>
    /// <param name="fw"></param>
    public static void HorizontalAligmentStretch(FrameworkElement fw)
    {
        fw.HorizontalAlignment = HorizontalAlignment.Stretch;
        if (fw is Control)
        {
            var c = (Control)fw;
            c.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        }
    }
    /// <summary>
    /// Dont use Aligment for stretch / fill all available size.
    /// Width / Height = double.NaN work like a charm!
    /// </summary>
    /// <param name="fw"></param>
    public static void VerticalAligmentStretch(FrameworkElement fw)
    {
        fw.VerticalAlignment = VerticalAlignment.Stretch;
        if (fw is Control)
        {
            var c = (Control)fw;
            c.VerticalContentAlignment = VerticalAlignment.Stretch;
        }
    }
    public static T FindName<T>(FrameworkElement element, string controlName)
    {
        return (T)element.FindName(controlName);
    }
}
