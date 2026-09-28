namespace SunamoWpf;

public class XamlGeneratorDesktop //: XamlGenerator
{
    static Type type = typeof(XamlGeneratorDesktop);



    public T GetControl<T>(string xml)
    {

        xml = SHReplace.ReplaceFirstOccurences(xml, ">", " xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">");
        var vrR = (T)XamlReader.Parse(xml);
        return vrR;
    }
}