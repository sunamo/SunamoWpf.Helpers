namespace SunamoWpf.Helpers.BaseControls;

public static class DependencyObjectHelper
{
    public static object MarkupWriter { get; private set; }

    public static List<DependencyProperty> GetDependencyProperties(object element)
    {
        List<DependencyProperty> properties = new List<DependencyProperty>();
        MarkupObject markupObject = System.Windows.Markup.Primitives.MarkupWriter.GetMarkupObjectFor(element);
        if (markupObject != null)
        {
            foreach (MarkupProperty mp in markupObject.Properties)
            {
                if (mp.DependencyProperty != null)
                {
                    properties.Add(mp.DependencyProperty);
                }
            }
        }

        return properties;
    }

    /// <summary>
    /// Return only real atteched in App
    /// Subtype of dependency property
    /// It's property as Grid.Row etc.
    /// </summary>
    /// <param name="element"></param>
    public static List<DependencyProperty> GetAttachedProperties(object element)
    {
        List<DependencyProperty> attachedProperties = new List<DependencyProperty>();
        MarkupObject markupObject = System.Windows.Markup.Primitives.MarkupWriter.GetMarkupObjectFor(element);
        if (markupObject != null)
        {
            foreach (MarkupProperty mp in markupObject.Properties)
            {
                if (mp.IsAttached)
                {
                    attachedProperties.Add(mp.DependencyProperty);
                }
            }
        }

        return attachedProperties;
    }

    public static T CreatedWithCopiedValues<T>(T t, params DependencyProperty[] p) where T : DependencyObject
    {
        dynamic d = new System.Dynamic.ExpandoObject();
        d.t = t;
        d.p = p;

        CreatedWithCopiedValuesWorker(d);

        //Thread thread = new Thread(new ParameterizedThreadStart( ));
        //thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
        //thread.Start( d);
        //thread.Join(); //Wait for the thread to end <== here will be froze

        return (T)d.r;
    }

    static void CreatedWithCopiedValuesWorker(object o)
    {
        dynamic d = o;
        object instance = null;
        WpfApp.cd.Invoke(() =>
        {

            instance = Activator.CreateInstance(d.t.GetType());
            foreach (var item in d.p)
            {
                object value = null;
                value = d.t.GetValue(item); ;
                (instance as DependencyObject).SetValue(item, value);
            }
        });
        d.r = instance;
    }
}