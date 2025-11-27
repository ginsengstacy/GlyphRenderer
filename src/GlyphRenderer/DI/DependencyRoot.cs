using Microsoft.Extensions.DependencyInjection;

namespace GlyphRenderer.DI;

internal static class DependencyRoot
{
    internal static ServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();
        services.AddGlyphRenderer();
        return services.BuildServiceProvider();
    }
}