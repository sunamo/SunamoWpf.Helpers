namespace SunamoWpf;

public interface IXamlDisplay
{

}

public class XamlDisplay
{
    

    public static ItemsPanelTemplate GetItemsPanelTemplate()
    {
        string xaml = @"<ItemsPanelTemplate   xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'>
                            <Grid>
                                <Grid.ColumnDefinitions>
                                    <ColumnDefinition />
                                </Grid.ColumnDefinitions>
                                <Grid.RowDefinitions>
                                    <RowDefinition />
                                </Grid.RowDefinitions>
                            </Grid>
                    </ItemsPanelTemplate>";
        return XamlReader.Parse( xaml) as ItemsPanelTemplate;
    }
}