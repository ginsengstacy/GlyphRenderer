using GlyphRenderer.Configuration;
using GlyphRenderer.Prompting.Prompts.InputType.String.Glyph;
using GlyphRenderer.Terminal;
using ImageMagick;
using Resources.Messages;
using System.IO;

namespace GlyphRenderer.Output;

internal sealed class OutputSaver(OverwriteDecisionService overwriteDecisionService)
{
    internal void SaveImageAsEachSelectedFormat(Glyph glyph, MagickImage image, SessionContext context)
    {
        Directory.CreateDirectory(context.OutputDirectory!);

        foreach (MagickFormat imageFormat in context.ImageFormats!)
        {
            string outputPath = BuildOutputPath(glyph, imageFormat, context.OutputDirectory!);

            if (!overwriteDecisionService.ShouldSave(outputPath, context))
            {
                Console.WriteLine(InfoMessages.SkippedRasterization_FormatString, Path.GetFileName(outputPath));
                continue;
            }

            try
            {
                using IMagickImage<byte> imageToWrite = image.Clone();

                // ICO requires multiple embedded sizes; resize each and add to collection for proper scaling
                if (imageFormat == MagickFormat.Ico)
                {
                    WriteIco(outputPath, imageToWrite);
                }
                else if (MagickFormatSupportsAlpha(imageFormat))
                {
                    WriteAlpha(outputPath, imageToWrite, imageFormat);
                }
                else
                {
                    WriteOpaque(outputPath, imageToWrite, imageFormat);
                }

                Console.WriteLine(InfoMessages.SavedFile_FormatString, Path.GetFileName(outputPath));
            }
            catch (Exception ex)
            {
                ConsoleHelpers.WriteError(ExceptionMessages.FailedToSave_FormatString, outputPath, ex.Message);
            }
        }
    }

    private static string BuildOutputPath(Glyph glyph, MagickFormat format, string directory)
    {
        string formatExtensionName = Enum.GetName(format) ?? throw new ArgumentNullException(nameof(format));
        return Path.Combine(directory, $"Glyph_{glyph.Label}.{formatExtensionName.ToLower()}");
    }

    private static bool MagickFormatSupportsAlpha(MagickFormat format) => AppConfig.ImageFormatsSupportingAlpha.Contains(format);

    private static void WriteIco(string outputPath, IMagickImage<byte> imageToWrite)
    {
        using var icoCollection = new MagickImageCollection();
        uint[] icoSizes = [16, 32, 48, 64, 128, 256];

        foreach (uint icoSize in icoSizes)
        {
            var imageToResize = imageToWrite.Clone();
            imageToResize.Resize(icoSize, icoSize);
            imageToResize.Format = MagickFormat.Png; // ICO uses PNG internally
            icoCollection.Add(imageToResize);
        }

        icoCollection.Write(outputPath, MagickFormat.Ico);
    }

    private static void WriteAlpha(string outputPath, IMagickImage<byte> imageToWrite, MagickFormat imageFormat)
    {
        imageToWrite.Format = imageFormat;
        imageToWrite.Write(outputPath);
    }

    private static void WriteOpaque(string outputPath, IMagickImage<byte> imageToWrite, MagickFormat imageFormat)
    {
        using MagickImage flattenedImage = FlattenImage(imageToWrite, MagickColors.White);
        flattenedImage.Format = imageFormat;
        flattenedImage.Write(outputPath);
    }

    private static MagickImage FlattenImage(IMagickImage<byte> image, MagickColor backgroundColor)
    {
        var background = new MagickImage(backgroundColor, image.Width, image.Height);
        background.Composite(image, CompositeOperator.Over);
        return background;
    }
}
