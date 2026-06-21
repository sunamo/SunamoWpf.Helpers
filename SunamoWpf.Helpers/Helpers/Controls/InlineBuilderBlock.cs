/// <summary>
/// Replacement for TextBlockHelperBase
/// Hyperlink is clickable
/// </summary>
public class InlineBuilderBlock : InlineBuilder
{
    /// <summary>
    /// Block put into FlowDocument.Blocks
    /// </summary>
    /// <param name="fd"></param>
    public InlineBuilderBlock(Paragraph fd) : base(fd.Inlines)
    {
    }
}
