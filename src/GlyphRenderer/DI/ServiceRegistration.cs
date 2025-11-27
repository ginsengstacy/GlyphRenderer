using GlyphRenderer.Configuration;
using GlyphRenderer.Output;
using GlyphRenderer.Prompting;
using GlyphRenderer.Prompting.PromptAction;
using GlyphRenderer.Prompting.Prompts.InputType.Key.OverwriteFile;
using GlyphRenderer.Prompting.Prompts.InputType.Key.Restart;
using GlyphRenderer.Prompting.Prompts.InputType.String.Font;
using GlyphRenderer.Prompting.Prompts.InputType.String.Glyph;
using GlyphRenderer.Prompting.Prompts.InputType.String.GlyphColor;
using GlyphRenderer.Prompting.Prompts.InputType.String.ImageFormat;
using GlyphRenderer.Prompting.Prompts.InputType.String.OutputDirectory;
using GlyphRenderer.Terminal;
using Microsoft.Extensions.DependencyInjection;

namespace GlyphRenderer.DI;

internal static class ServiceRegistration
{
    internal static IServiceCollection AddGlyphRenderer(this IServiceCollection services) =>
        services
            .AddCoreServices()
            .AddPromptingSubsystem()
            .AddOutputSubsystem();

    private static IServiceCollection AddCoreServices(this IServiceCollection services) =>
        services
            .AddSingleton<CLI>()
            .AddSingleton<InteractiveLoop>()
            .AddSingleton<PromptWizard>()
            .AddSingleton<SessionContextFactory>()
            .AddSingleton<GlyphProcessingOrchestrator>();

    private static IServiceCollection AddPromptingSubsystem(this IServiceCollection services) =>
        services
            .AddPrompts()
            .AddParsers();

    private static IServiceCollection AddPrompts(this IServiceCollection services) =>
        services
            .AddSingleton<ColorPrompt>()
            .AddSingleton<FileOverwritePrompt>()
            .AddSingleton<FontPrompt>()
            .AddSingleton<ImageFormatPrompt>()
            .AddSingleton<OutputDirectoryPrompt>()
            .AddSingleton<RestartPrompt>()
            .AddSingleton<GlyphPrompt>();

    private static IServiceCollection AddParsers(this IServiceCollection services) =>
        services
            .AddSingleton<ColorParser>()
            .AddSingleton<CommandTypeParser>()
            .AddSingleton<FileOverwriteParser>()
            .AddSingleton<ImageFormatParser>()
            .AddSingleton<OutputDirectoryParser>()
            .AddSingleton<RestartParser>()
            .AddSingleton<TypefaceParser>()
            .AddSingleton<GlyphParser>();

    private static IServiceCollection AddOutputSubsystem(this IServiceCollection services) =>
        services
            .AddSingleton<OutputSaver>()
            .AddSingleton<OverwriteDecisionService>();
}
