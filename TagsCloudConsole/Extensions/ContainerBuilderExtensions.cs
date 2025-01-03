using System.Drawing;
using Autofac;
using DeepMorphy;
using TagsCloudVisualization.ColorFactories;
using TagsCloudVisualization.ImageSavers;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Options;
using TagsCloudVisualization.TagsCloudImageCreators;
using TagsCloudVisualization.TextReaders;
using TagsCloudVisualization.Visualizers;
using TagsCloudVisualization.WordsHandlers;
using WeCantSpell.Hunspell;

namespace TagsCloudConsole.Extensions;

public static class ContainerBuilderExtensions
{
    public static ContainerBuilder RegisterWordAnalytics(this ContainerBuilder builder, TagsCloudVisualizationOptions options)
    {
        builder.RegisterInstance(WordList.CreateFromFiles(options.DictionaryPath));
        builder.RegisterType<MorphAnalyzer>().AsSelf().SingleInstance();

        return builder;
    }

    public static ContainerBuilder RegisterTextReaders(this ContainerBuilder builder)
    {
        builder.RegisterType<TxtReader>().As<ITextReader>();

        return builder;
    }

    public static ContainerBuilder RegisterImageSavers(this ContainerBuilder builder, TagsCloudVisualizationOptions options)
    {
        builder.RegisterType<PngSaver>()
            .WithParameter(TypedParameter.From(options.OutputFilePath))
            .As<IImageSaver>();

        return builder;
    }

    public static ContainerBuilder RegisterColorFactory(this ContainerBuilder builder, TagsCloudVisualizationOptions options)
    {
        if (options.ColorName == "random")
            builder
                .RegisterType<RandomColorFactory>()
                .As<IColorFactory>();
        else
            builder
                .RegisterType<DefaultColorFactory>()
                .As<IColorFactory>()
                .WithParameter(TypedParameter.From(options.ColorName));

        return builder;
    }

    public static ContainerBuilder RegisterWordHandlers(this ContainerBuilder builder)
    {
        builder.RegisterType<WordsInLowerCaseHandler>().As<IWordHandler>();
        builder.RegisterType<BoringWordsHandler>().As<IWordHandler>();
        builder.RegisterType<StemmingWordsHandler>().As<IWordHandler>();

        return builder;
    }

    public static ContainerBuilder RegisterCloudLayouter(this ContainerBuilder builder, TagsCloudVisualizationOptions options)
    {
        builder
            .Register<CircularCloudLayouter>((_, _) =>
                new CircularCloudLayouter(
                    new Point(options.ImageWidth / 2, options.ImageHeight / 2),
                    options.LayoutRadius,
                    options.LayoutAngleOffset))
            .As<ICloudLayouter>();

        return builder;
    }

    public static ContainerBuilder RegisterTagLayouter(this ContainerBuilder builder, TagsCloudVisualizationOptions options)
    {
        builder
            .Register<TagLayouterOptions>((_, _) =>
                new TagLayouterOptions(options.MinFontSize, options.MaxFontSize, new FontFamily(options.FontFamily)))
            .AsSelf();
        builder
            .RegisterType<TagLayouter>()
            .As<ITagLayouter>();

        return builder;
    }

    public static ContainerBuilder RegisterTagVisualizer(this ContainerBuilder builder, TagsCloudVisualizationOptions options)
    {
        builder
            .RegisterType<TagVisualizer>()
            .As<ITagVisualizer>()
            .WithParameter(TypedParameter.From(new Size(options.ImageWidth, options.ImageHeight)));

        return builder;
    }

    public static ContainerBuilder RegisterTagsCloudImageCreator(this ContainerBuilder builder)
    {
        builder.RegisterType<TagsCloudImageCreator>().As<ITagsCloudImageCreator>();

        return builder;
    }
}