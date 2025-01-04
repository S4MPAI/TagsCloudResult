using Autofac;
using TagsCloudConsole.Extensions;

namespace TagsCloudConsole;

public class TagsCloudContainerBuilder(TagsCloudVisualizationOptions options)
{
    public IContainer Build()
    {
        return new ContainerBuilder()
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
    }
}