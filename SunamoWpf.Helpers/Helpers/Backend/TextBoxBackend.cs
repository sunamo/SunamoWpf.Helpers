namespace SunamoWpf.Helpers.Backend;

public class TextBoxBackend : IKeysHandler, IShowSearchResults
{
    static Type type = typeof(TextBoxBackend);
    // Menu, ToolBar and tbLineBreak = 67 lines. Should be changed in every App
    //public int addLinesInEveryScroll = 67;
    public int actualSearchedResult = -1;
    public SearchCodeElementsUCData searchCodeElementsUCData = null;
    public event VoidInt ScrollToLine;
    public event Action EndOfFilteredLines;
    public List<FoundedCodeElementWpf> actualFileSearchOccurences
    {
        get
        {
            /* zde byla anomálie kterou jsem nedokázal vysvětlit
do SearchCodeElementsUCData.founded vkládám ve value.Count 2, ale zde mi to již vrací 5
             */
            if (searchCodeElementsUCData != null && searchCodeElementsUCData.actualFileSearchOccurences != null)
            {
                return RemoveDuplicatedOccurences(searchCodeElementsUCData.actualFileSearchOccurences);
            }
            return null;
        }
    }
    private List<FoundedCodeElementWpf> RemoveDuplicatedOccurences(List<FoundedCodeElementWpf> actualFileSearchOccurences)
    {
        var distinctItems = actualFileSearchOccurences.GroupBy(x => x.Line).Select(y => y.First());
        return distinctItems.ToList();
    }
    /// <summary>
    /// Line to which was last time scrolled
    /// </summary>
    int _actualLine = 0;
    public int actualLine
    {
        set
        {
            _actualLine = value;
            TextBoxHelper.ScrollToLine(txtContent, value);
        }
        get
        {
            return _actualLine;
        }
    }
    int addRowsDuringScrolling = 0;
    /// <summary>
    /// A3 = "0"
    /// </summary>
    /// <param name="searchData"></param>
    /// <param name="tbTextBoxState"></param>
    /// <param name="txtContent"></param>
    public TextBoxBackend(TextBlock tbTextBoxState, TextEditor txtContent, int addRowsDuringScrolling)
    {
        this.txtTextBoxState = tbTextBoxState;
        this.txtContent = txtContent;
        this.addRowsDuringScrolling = addRowsDuringScrolling;
        // Is changed also when just moved cursor (mouse, arrows)
        //txtContent.SelectionChanged += TxtContent_SelectionChanged;
    }
    private void TxtContent_SelectionChanged(object sender, System.Windows.RoutedEventArgs e)
    {
        //SetActualLine( txtContent.GetLineIndexFromCharacterIndex(txtContent.SelectionStart));
    }
    public bool HandleKey(KeyEventArgs e)
    {
        return false;
    }
    TextBlock txtTextBoxState;
    //
    protected TextEditor txtContent;
    public TextBoxStateWpf state = new TextBoxStateWpf();
    public void SetActualFile(string file)
    {
        state.textActualFile = Translate.FromKey(XlfKeys.File) + ": " + file;
        SetTextBoxState();
    }
    public void SetActualLine(int line)
    {
        _actualLine = line++;
        state.textActualFile = Translate.FromKey(XlfKeys.Line) + ": " + _actualLine;
        SetTextBoxState();
    }
    public void SetTbSearchedResult(int actual, int count)
    {
        state.textSearchedResult = $"{actual}/{count}";
        SetTextBoxState();
    }
    public void SetTextBoxState(string s = null)
    {
        if (s == null)
        {
            s = (string.Join("  ", state.textActualFile, state.textSearchedResult) + " " + Translate.FromKey(XlfKeys.Line) + ": " + (actualLine + 1)).Trim();
        }
        txtTextBoxState.Text = s;
    }
    public void JumpToNextSearchedResult(int addLines)
    {
        if (actualFileSearchOccurences == null)
        {
            return;
        }
        var actualFileSearchOccurencesCount = actualFileSearchOccurences.Count;
#if DEBUG
        //if(actualFileSearchOccurencesCount == 5)
        //{
        //}
#endif
        var data = searchCodeElementsUCData;
        if (actualFileSearchOccurencesCount == 0)
        {
            SetTbSearchedResult(0, 0);
        }
        else
        {
            if (actualSearchedResult == actualFileSearchOccurencesCount)
            {
                if (EndOfFilteredLines != null)
                {
                    EndOfFilteredLines();
                }
                actualSearchedResult = 0;
            }
            int serie = actualSearchedResult + 1;
            SetTbSearchedResult(serie, actualFileSearchOccurencesCount);
            FoundedCodeElementWpf a = actualFileSearchOccurences[actualSearchedResult];
            ScrollToLineMethod(a.Line/*, addRowsDuringScrolling*/);
            actualSearchedResult++;
        }
    }
    /// <summary>
    /// A2 to full number of showing rows at one time.
    /// </summary>
    /// <param name="line"></param>
    /// <param name="addLines"></param>
    public void ScrollToLineMethod(int line)
    {
        #region 1) CodeTextBox
        //if (txtContent.LineCount > addLines && line > addLines)
        //{
        //    // -4 due to excepiton on txt.GetCharacterIndexFromLineIndex(line); - line was 244, but has only 243 lines
        //    // commented, one is sure, condition txtContent.LineCount > addLines && line > addLines is wrong due to highlight wrong line (225 instead od 160)
        //    //line += addLines - 2;
        //}
        //if (ScrollToLine != null)
        //{
        //    ScrollToLine(line);
        //}
        //if (txtContent != null)
        //{
        //    TextBoxHelper.ScrollToLine(txtContent, line);
        //}
        #endregion
        #region 2) AvalonEdit
        TextBoxHelper.ScrollToLine(txtContent, line);
        #endregion
        actualLine = line;
        WpfLogger.Info(Translate.FromKey(XlfKeys.ScrolledToLine) + " " + line);
        SetTextBoxState();
    }
    public void ScrollAboutLines(int v)
    {
        //v -= 1;
        //v = v * 2;
        //v -= 1;
        int newLine = actualLine + v;
        int countLines = SH.CountLines(txtContent.Text);
        if (newLine > countLines)
        {
            ScrollToLineMethod(countLines);
        }
        else
        {
            ScrollToLineMethod(newLine);
        }
    }
    /// <summary>
    /// Must be called in Loaded or after
    /// </summary>
    /// <param name="from"></param>
    /// <param name="length"></param>
    public void Highlight(int from, int length)
    {
        if (from != -1)
        {
            txtContent.Focus();
            txtContent.Select(from, length);
        }
    }
    /// <summary>
    /// Must be called in Loaded or after
    /// A1 -1, because highlighting can be processed only after and index was already increment
    /// </summary>
    public void Highlight(int actualSearchedResult)
    {
        if (searchCodeElementsUCData.actualFileSearchOccurences != null)
        {
            var r = searchCodeElementsUCData.actualFileSearchOccurences[actualSearchedResult];
            Highlight(r.From, r.Lenght);
        }
    }
}