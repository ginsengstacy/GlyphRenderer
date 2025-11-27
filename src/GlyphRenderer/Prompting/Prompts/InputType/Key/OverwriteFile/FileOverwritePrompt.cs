using GlyphRenderer.Configuration;
using GlyphRenderer.Prompting.PromptAction;
using GlyphRenderer.Terminal;
using Resources.Messages;

namespace GlyphRenderer.Prompting.Prompts.InputType.Key.OverwriteFile;

public sealed class FileOverwritePrompt(FileOverwriteParser fileOverwriteParser, CommandTypeParser commandTypeParser)
    : PromptBase<ConsoleKeyInfo, FileOverwriteResult?>(commandTypeParser)
{
    protected override string Message => PromptMessages.OverwriteFile_FormatString;
    protected override Func<ConsoleKeyInfo> GetInput => () => ConsoleHelpers.ReadKeyLine(intercept: true);
    protected override IPromptInputParser<ConsoleKeyInfo, FileOverwriteResult?> Parser { get; } = fileOverwriteParser;
    protected override Action<SessionContext, FileOverwriteResult?> ValueUpdater { get; } =
        (context, value) => context.OverwriteMode = value!.Value.OverwriteMode;
}
