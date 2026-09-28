namespace SunamoWpf.StartupHelper;

public class FileTextLogger
{
    /// <summary>
    /// It was be totally nonsense, just do it in memory. Even if I call sw.Close and sw.Dispose app still hold the file
    /// </summary>
    public StreamWriter sw = null;
    public string fn = null;
    public StringBuilder sb = new StringBuilder();

    /// <summary>
    /// Buffer 1MB
    /// </summary>
    /// <param name="fn"></param>
    public FileTextLogger(string fn)
    {
        this.fn = fn;
        FS.CreateUpfoldersPsysicallyUnlessThere(fn);
        //FileStream fs = new FileStream(fn, FileMode.OpenOrCreate);

        // 1024 * 1024 *
        // cant use, could terminate itself
        //PH.ShutdownProcessWhichOccupyFileHandleExe(fn);

        // It was be totally nonsense, just do it in memory. Even if I call sw.Close and sw.Dispose app still hold the file
        //sw = File.CreateText(fn);//, Encoding.UTF8,  1024 * bufferInMb);
        //sw.AutoFlush = true;
        WriteNewLine(DateTime.Now.ToLongTimeString());
    }

    public void WriteNewLine(string l)
    {
        // Is written StartupHelper.Dispose => just sb here
        //TF.AppendAllText(l + Environment.NewLine, fn);
        sb.AppendLine(l);

        // Umí se to zapsat aji ve StartupHelper.Dispose ale budu to zapisovat aji zde průběřně protože StartupHelper.Dispose to nedosáhne
        File.WriteAllText(fn, sb.ToString());
    }
}