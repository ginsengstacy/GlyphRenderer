using GlyphRenderer.Prompting;
using GlyphRenderer.Prompting.Prompts.InputType.String.Font;
using GlyphRenderer.Prompting.Prompts.InputType.String.Glyph;
using GlyphRenderer.Prompting.Prompts.InputType.String.GlyphColor;
using GlyphRenderer.Prompting.Prompts.InputType.String.ImageFormat;
using GlyphRenderer.Prompting.Prompts.InputType.String.OutputDirectory;

namespace GlyphRenderer.Configuration;

public sealed class SessionContextFactory(
    FontPrompt fontPrompt,
    GlyphPrompt glyphPrompt,
    ColorPrompt colorPrompt,
    ImageFormatPrompt imageFormatPrompt,
    OutputDirectoryPrompt outputDirectoryPrompt
)
{
    public SessionContext CreateDefault()
    {
        IPrompt[] promptOrder =
        [
            fontPrompt,
            glyphPrompt,
            colorPrompt,
            imageFormatPrompt,
            outputDirectoryPrompt
        ];

        return new SessionContext(promptOrder);
    }
}
