using Autofac;
using TagsCloudConsole.Extensions;
using TagsCloudConsole.Validators;
using TagsCloudVisualization.TagsCloudImageCreators;

namespace TagsCloudConsole;

public static class Program
{
    public static void Main(string[] args)
    {
        var options = CommandLine.Parser.Default
            .ParseArguments<TagsCloudVisualizationOptions>(args)
            .Value;
        var validator = new OptionsValidator();
        var validatedParams = validator.Validate(options);

        if (!validatedParams.IsSuccess)
        {
            Console.WriteLine(validatedParams.Error);
            return;
        }

        var container = new ContainerBuilder()
            .RegisterWordAnalytics(options)
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