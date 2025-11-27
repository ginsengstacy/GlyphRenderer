using GlyphRenderer.Configuration;
using GlyphRenderer.Output;
using GlyphRenderer.Prompting.Prompts.InputType.String.Glyph;
using GlyphRenderer.Rendering;
using ImageMagick;

namespace GlyphRenderer;

internal class GlyphProcessingOrchestrator(OutputSaver outputSaver)
{
    private readonly OutputSaver _outputSaver = outputSaver;

    internal void RenderAndSaveAllFromContext(SessionContext context)
    {
        foreach (Glyph glyph in context.Glyphs!)
        {
            using MagickImage image = RenderingHelpers.RenderGlyph(
                glyph, context.Typeface!,
                context.Color!.Value
            );

            _outputSaver.SaveImageAsEachSelectedFormat(glyph, image, context);
        }
    }
}
