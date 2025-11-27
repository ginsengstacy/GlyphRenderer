using GlyphRenderer.Configuration;
using GlyphRenderer.Prompting.PromptAction;
using GlyphRenderer.Terminal;
using Resources.Messages;

namespace GlyphRenderer.Prompting.Prompts.InputType.String.Glyph;

public sealed class GlyphPrompt(GlyphParser glyphParser, CommandTypeParser commandTypeParser) : PromptBase<string, Glyph[]?>(commandTypeParser)
{
    protected override string Message => PromptMessages.Glyph;

    protected override Func<string> GetInput => () => ConsoleHelpers.ReadLineSafe();
    protected override IPromptInputParser<string, Glyph[]?> Parser { get; } = glyphParser;
    protected override Action<SessionContext, Glyph[]?> ValueUpdater { get; } = (context, value) => context.Glyphs = value;
}