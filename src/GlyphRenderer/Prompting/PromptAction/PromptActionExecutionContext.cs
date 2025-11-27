using GlyphRenderer.Configuration;

namespace GlyphRenderer.Prompting.PromptAction;

public readonly struct PromptActionExecutionContext
{
    public SessionContext SessionContext { get; init; }
    public object? Value { get; init; }
    public string? Message { get; init; }
}