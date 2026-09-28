namespace SunamoWpf.Helpers.ControlsWithGet;

public partial class ComboBoxHelper
{
    static Type type = typeof(ComboBoxHelper);
    protected ComboBox cb = null;
    public bool raiseSelectionChanged = true;
    public event SelectionChangedEventHandler SelectionChanged;
    /// <summary>
    /// Objekt, ve kterém je vždy aktuální zda v tsddb něco je
    /// Takže se nelekni že to je promměná
    /// </summary>
    public object SelectedO = null;
    public bool Selected
    {
        get
        {
            if (SelectedO != null)
            {
                return SelectedO.ToString().Trim() != "";
            }
            return false;
        }
    }
    public string SelectedS
    {
        get
        {
            if (SelectedO == null)
            {
                if (cb.Items.Count > 0)
                {
                    cb.SelectedIndex = 0;
                    SelectedO = cb.Items[0];
                }
                else
                {
                    return string.Empty;
                }
            }
            // not need ValueFromTWithNameOrObject, TWithName has ToString
            return SelectedO.ToString();
        }
    }
    void tsddb_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SelectedO = cb.SelectedItem;
        if (SelectedO != null)
        {
            // not need ValueFromTWithNameOrObject, TWithName has ToString
            cb.ToolTip = originalToolTipText + " " + SelectedO.ToString();
        }
        if (raiseSelectionChanged)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(sender, e);
            }
        }
    }
    protected string originalToolTipText = "";
    /// <summary>
    /// A2 zda se má do SelectedO uložit tsmi.Tag nebo jen tsmi
    /// </summary>
    /// <param name="tsddb"></param>
    /// <param name="tagy"></param>
    public ComboBoxHelper(ComboBox tsddb)
    {
        this.cb = tsddb;
        tsddb.SelectionChanged += tsddb_SelectionChanged;
    }
    public ComboBox Cb
    {
        get
        {
            return cb;
        }
    }
    /// <summary>
    /// A1 is not needed, value is obtained through []
    /// Tag here is mainly for comment what data control hold 
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="list12"></param>
    public static ComboBox Get(ControlInitData d)
    {
        ComboBox cb = new ComboBox();
        ControlHelper.SetForeground(cb, d.foreground);
        foreach (var item in d.list)
        {
            cb.Items.Add(item);
        }
        if (d.OnClick != null)
        {
            ThrowEx.IsNotAllowed("d.OnClick");
        }
        cb.Tag = d.tag;
        cb.ToolTip = d.tooltip;
        cb.IsEditable = d.isEditable;
        return cb;
    }
    /// <summary>
    /// Instead of this use instance 
    /// </summary>
    /// <param name="tb"></param>
    /// <param name="control"></param>
    /// <param name="trim"></param>
    public static void Validate(ComboBox control, ref ValidateDataWpf d)
    {
        control.Validate(ref d);
    }
    public static bool validated
    {
        set
        {
            ComboBoxExtensions.validated = value;
        }
        get
        {
            return ComboBoxExtensions.validated;
        }
    }
}