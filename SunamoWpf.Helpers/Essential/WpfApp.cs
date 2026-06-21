namespace SunamoWpf.Essential;



public partial class WpfApp
{
    static WpfApp()
    {
        EnableDesktopLogging(true);
    }
    public static Action<string> WriteToStartupLogRelease;
    /// <summary>
    /// Delegate set by Windows layer to show exception window. Avoids circular dependency.
    /// </summary>
    public static Action<object> ShowExceptionWindow2Delegate;
    /// <summary>
    /// Delegate set by Windows layer to show exception window with args. Returns dump string.
    /// </summary>
    public static Func<object, string, bool, string> ShowExceptionWindowDelegate;
    public static void ShowMb(string t)
    {
        reallyThrow = ThrowEx.reallyThrow2;
        ThrowEx.reallyThrow2 = false;
        if (false)
        {
            try
            {
                MessageBox.Show(t);
            }
            catch (Exception ex)
            {
                //0x800401D0 (CLIPBRD_E_CANT_OPEN))
            }
        }
        if (WriteToStartupLogRelease != null)
        {
            WriteToStartupLogRelease(t);
        }
        ThrowEx.reallyThrow2 = reallyThrow;
    }
    public static void Shutdown(object o, EventArgs eh)
    {
        WpfApp.htt.SetCancelClosing(false);
        WpfApp.window.Close();
    }
    public static void Restart()
    {
        System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
        Application.Current.Shutdown();
    }
    // Is not available in SunamoWpf.web
    //public static void Restart2()
    //{
    //    System.Windows.Forms.Application.Restart();
    //    System.Windows.Application.Current.Shutdown();
    //}
    static DependencyProperty[] props = new DependencyProperty[] { TextBlock.ForegroundProperty, TextBlock.TextProperty };
    public static string SQLExpressInstanceName()
    {
        return Environment.MachineName;
    }
#if DEBUG
    public static void WriteDebug(string v)
    {
        Debug.WriteLine(v);
    }
#endif
    /// <summary>
    /// Alternatives are:
    /// //InitApp.SetDebugLogger
    /// CmdApp.SetLogger
    /// WpfApp.SetLogger
    /// </summary>
    public static void SetLogger()
    {
        ////InitApp.Logger = SunamoLogger.Logger.LoggerBaseNS.SunamoLogger.Instance;
        ////InitApp.TemplateLogger = //SunamoTemplateLogger.Instance;
        ////InitApp.TypedLogger = TypedSunamoLogger.Instance;
    }
    public static void SaveReferenceToTextBlockStatus(bool restore, TextBlock tbTemporaryLastErrorOrWarning, TextBlock tbTemporaryLastOtherMessage)
    {
        if (restore)
        {
            tbLastErrorOrWarning = tbLastErrorOrWarningSaved;
            tbLastOtherMessage = tbLastOtherMessageSaved;
        }
        else
        {
            tbLastErrorOrWarningSaved = tbLastErrorOrWarning;
            tbLastOtherMessageSaved = tbLastOtherMessage;
        }
        if (!restore)
        {
            tbLastErrorOrWarning = tbTemporaryLastErrorOrWarning;
            tbLastOtherMessage = tbTemporaryLastOtherMessage;
        }
    }
    private static void SetForeground(TextBlock tbLastOtherMessage, Color color)
    {
        if (tbLastOtherMessage != null)
        {
            WpfApp.cd.Invoke(() =>
            {
                tbLastOtherMessage.Foreground = new SolidColorBrush(color);
            }
            );
        }
    }
    //static List<string> otherStatuses = new List<string>();
    //static List<string> errorStatuses = new List<string>();
    //public static void ClearRemembered()
    //{
    //    otherStatuses.Clear();
    //    errorStatuses.Clear();
    //}
    private static void SetStatus(TypeOfMessageWpf st, string status)
    {
        status = DateTime.Now.ToShortTimeString() + " " + status;
        Color fg = StatusHelper.GetForegroundBrushOfTypeOfMessage(st);
        if (st == TypeOfMessageWpf.Error || st == TypeOfMessageWpf.Warning)
        {
            // tbLastErrorOrWarning must be defined otherwise wont be adding to lbLogsErrors also
            //if (tbLastErrorOrWarning != null)
            //{
            SetForeground(tbLastErrorOrWarning, fg);
            TextBlockHelper.SetText(tbLastErrorOrWarning, status);
            if (lbLogsErrors != null)
            {
                TextBlock txt = DependencyObjectHelper.CreatedWithCopiedValues<TextBlock>(tbLastErrorOrWarning, props);
                cd.Invoke(() =>
                {
                    txt.ToolTip = tbLastErrorOrWarning.Text;
                    lbLogsErrors.Children.Insert(0, txt);
                });
            }
            //}
        }
        else
        {
            // tbLastOtherMessage must be defined otherwise wont be adding to lbLogsErrors also
            //if (tbLastOtherMessage != null)
            //{
            SetForeground(tbLastOtherMessage, fg);
            TextBlockHelper.SetText(tbLastOtherMessage, status);
            if (lbLogsOthers != null)
            {
                TextBlock txt = DependencyObjectHelper.CreatedWithCopiedValues<TextBlock>(tbLastOtherMessage, props);
                cd.Invoke(() =>
                {
                    txt.ToolTip = tbLastOtherMessage.Text;
                    lbLogsOthers.Children.Insert(0, txt);
                    //lbLogsOthers.InvalidateVisual();
                    //lbLogsOthers.UpdateLayout();
                    //lbLogsOthers.Children.Insert(0, new TextBlock());
                    //lbLogsOthers.Children.RemoveAt(0);
                    //lbLogsOthers.InvalidateArrange();
                    //lbLogsOthers.UpdateLayout();
                    //lbLogsOthers.
                }
                , DispatcherPriority.Render);
            }
            //}
        }
    }
    /// <summary>
    /// EN: Event triggered when status message is set
    /// CZ: Event vyvolaný když je nastavena status zpráva
    /// </summary>
    public static event Action<TypeOfMessageWpf, string> StatusSetted;

    public static void EnableDesktopLogging(bool v)
    {
        if (v)
        {
            // CZ: Povolit desktop logování - nyní se používá StatusSetted event
            // EN: Enable desktop logging - now uses StatusSetted event
            StatusSetted -= ThisApp_StatusSetted;
            StatusSetted += ThisApp_StatusSetted;
        }
        else
        {
            StatusSetted -= ThisApp_StatusSetted;
        }
    }

    private static void ThisApp_StatusSetted(TypeOfMessageWpf t, string message)
    {
        SetStatus(t, message);
    }
    // TODO: Rename to SetStatusAsync and merge with commented method SetStatus here
    public async static Task SetStatusToTextBlock(TypeOfMessageWpf st, string status)
    {
        Color fg = Colors.Black;
        if (st == TypeOfMessageWpf.Error || st == TypeOfMessageWpf.Warning)
        {
            await SetForegroundAsync(tbLastErrorOrWarning, fg);
            await SetTextAsync(tbLastErrorOrWarning, status);
        }
        else
        {
            await SetForegroundAsync(tbLastOtherMessage, fg);
            await SetTextAsync(tbLastOtherMessage, status);
        }
    }
    public async static Task SetForegroundAsync(TextBlock tbLastOtherMessage, Color color)
    {
        await cd.InvokeAsync(() =>
        {
            tbLastOtherMessage.Foreground = new SolidColorBrush(color);
        }
        , cdp);
    }
    public async static Task SetTextAsync(TextBlock lblStatusDownload, string status)
    {
        await cd.InvokeAsync(() =>
        {
            lblStatusDownload.Text = status;
        }
        , cdp);
    }
    static IEssentialMainWindowBase _mp = null;
    public static Window window = null;
    public static IHideToTray htt = null;
    public static IEssentialMainWindowBase mp
    {
        get
        {
            return _mp;
        }
        set
        {
            _mp = value;
            window = (Window)value;
            // Without it, app would be still running after close
            window.Closed += (sender, e) => window.Dispatcher.InvokeShutdown();
        }
    }
    public static TextBlock tbLastErrorOrWarning = null;
    public static TextBlock tbLastOtherMessage = null;
    static TextBlock tbLastErrorOrWarningSaved = null;
    static TextBlock tbLastOtherMessageSaved = null;
    static StackPanel lbLogsOthers = null;
    static StackPanel lbLogsErrors = null;
    public static Dispatcher cd = null;
    public static DispatcherPriority cdp = DispatcherPriority.Normal;
    public static bool rememberStatuses;
    public static void SaveReferenceToLogsStackPanel(StackPanel _lbLogsOthers, StackPanel _lbLogsErrors)
    {
        lbLogsErrors = _lbLogsErrors;
        lbLogsOthers = _lbLogsOthers;
    }
}