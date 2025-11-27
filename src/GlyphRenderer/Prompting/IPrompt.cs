using GlyphRenderer.Configuration;

namespace GlyphRenderer.Prompting;

public interface IPrompt
{
    PromptResult Execute();
    void UpdateContextWithParsedAndValidValue(SessionContext context);
}