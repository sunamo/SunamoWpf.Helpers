namespace SunamoWpf.StartupHelper;

public class StartupHelper
{
    static StopwatchHelper swOverall = new StopwatchHelper();
    public static FileTextLogger ftl = null;
    public static Type type = typeof(StartupHelper);

    public static String[] args = null;

    const string bp = @"C:\_._\";

    static bool saveLoadedAssemblies = false;

    /// <summary>
    /// In MW_Loaded on the end before set init flag
    /// </summary>
    public static void Dispose()
    {
        if (saveLoadedAssemblies)
        {
            //var fLoadedAssemblies = AppData.ci.GetFile(AppFolders.Logs, "loadedAssemblies.txt");
            //TF.WriteAllLines(fLoadedAssemblies, loadedAssemblies);
            //PHWin.Code(fLoadedAssemblies);
        }

        if (ftl != null)
        {
            if (ftl.fn != null)
            {
                // Tóto by nemělo být potřeba protože ukládám to postupně po každém řádku
                //TF.WriteAllText(ftl.fn, ftl.sb.ToString());

            }
            else
            {
                ThrowEx.IsNull("ftl.fn", ftl.fn);
            }
            ftl.sb.Clear();
        }

    }

    static Action<string> sl => PD.WriteToStartupLogRelease;

    /// <summary>
    /// Optional
    /// </summary>
    /// <param name="e"></param>
    public static void Startup(StartupEventArgs e)
    {
        args = e.Args;


        if (WriterEventLog.IsAdmin())
        {
#if MB
                WpfApp.ShowMb("A");
#endif
            sl("A");
        }
        else
        {
#if MB
            WpfApp.ShowMb("Non admin");
#endif
            sl("Non admin");
        }

    }

    static Application app = null;

    static List<string> loadedAssemblies = new List<string>();

#if MB
    static void ShowMb(string s)
    {
        MessageBox.Show(s);
    }
#endif

    /// <summary>
    /// In OnStartup
    /// </summary>
    /// <param name="dispatcher"></param>
    /// <param name="app"></param>
    public static void Ctor(Dispatcher dispatcher, Application app)
    {
        ftl = new FileTextLogger(bp + @"StartupLogRelease.txt");
        PD.WriteToStartupLogRelease = ftl.WriteNewLine;

        PD.WriteToStartupLogRelease("Ctor_Start");
        if (FS.ExistsDirectory(bp))
        {
            //swOverall.Start();
            // cant use app data here coz wasnt call AppData.ci.GetFolderWithAppsFiles


            PD.WriteToStartupLogRelease("Dir Exists");
        }
        else
        {
            PD.WriteToStartupLogRelease("Dir Not Exists");
        }
        //PD.WriteToStartupLogRelease("MB2");

        app.LoadCompleted += App_LoadCompleted;

#if MB
        // used as PD.delShowMb
        TranslateDictionary.ShowMb = ShowMb; //WpfApp.ShowMb;
#endif

        WpfApp.cd = dispatcher;

        //var d = HttpRequestHelper.GetResponseText("https://app.sunamo.net/hasvalidlicence.ashx?idUser=1&module=olqwgptsxghthafarznbgpnoljhtgxjrhbpqwkpehsaimuwwgucmoowxgrymvynagvfgxxelgpus&pw=pr6LGMmcoajbUA4c1C0tYw%3D%3D&", HttpMethod.Get, new HttpRequestData { });

        ////var d = WinSecHelper.IsMyComputer(null, null);
        //int i = 0;

        //AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

        //AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
        //{


        //    var always = CAG.ToList<string>("Aps.Projs", "Aps.Slns", "Aps.Sis", "Aps.resources");
        //    var n = args.Name;
        //    if (saveLoadedAssemblies)
        //    {
        //        loadedAssemblies.Add(n);
        //    }

        //    if (n.Contains("Aps") && CA.StartWithAnyInElement(n, always, false).Count == 0)
        //    {
        //        ////DebugLogger.Instance.WriteLine(n);

        //        if(n.StartsWith("Aps.Xlf"))
        //        {
        //            var la = Assembly.Load("Aps.Xlf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
        //            return la;
        //        }
        //    }
        //    return null;
        //    //String resourceName = "AssemblyLoadingAndReflection." + new AssemblyName(args.Name).Name + ".dll";

        //    //using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
        //    //{

        //    //    Byte[] assemblyData = new Byte[stream.Length];

        //    //    stream.Read(assemblyData, 0, assemblyData.Length);

        //    //    return Assembly.Load(assemblyData);

        //    //}

        //};
    }

    private static void App_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
    {
        // CanT be used for my purposes
        StartupHelper.Dispose();


    }

}