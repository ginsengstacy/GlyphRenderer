using ImageMagick;
using System.Windows.Media;

namespace GlyphRenderer.Configuration;

public static class Defaults
{
    public static readonly Color Color = Colors.Black;
    public static readonly MagickFormat[] ImageFormats = [MagickFormat.Png];
}