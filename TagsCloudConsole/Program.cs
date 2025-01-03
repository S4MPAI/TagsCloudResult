using Autofac;
using TagsCloudConsole.Extensions;
using TagsCloudVisualization.TagsCloudImageCreators;

namespace TagsCloudConsole;

public static class Program
{
    public static void Main(string[] args)
    {
        var options = CommandLine.Parser.Default
            .ParseArguments<TagsCloudVisualizationOptions>(args)
            .Value;

        var container = new ContainerBuilder()
            .RegisterWordAnalytics()
            .RegisterWordHandlers()
            .RegisterTextReaders()
            .RegisterImageSavers(options)
            .RegisterColorFactory(options)
            .RegisterCloudLayouter(options)
            .RegisterTagLayouter(options)
            .RegisterTagVisualizer(options)
            .RegisterTagsCloudImageCreator()
            .Build();

        var creator = container.Resolve<ITagsCloudImageCreator>();
        creator.CreateImageWithTags(options.InputFilePath);
    }
}