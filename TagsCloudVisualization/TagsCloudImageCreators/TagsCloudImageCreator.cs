using System.Text.RegularExpressions;
using TagsCloudVisualization.ImageSavers;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.TextReaders;
using TagsCloudVisualization.Visualizers;
using TagsCloudVisualization.WordsHandlers;

namespace TagsCloudVisualization.TagsCloudImageCreators;

public class TagsCloudImageCreator(
    ITextReader[] textReaders,
    IWordHandler[] wordHandlers,
    ITagLayouter tagLayouter,
    ITagVisualizer visualizer,
    IImageSaver imageSaver) : ITagsCloudImageCreator
{
    private static readonly Regex GetWordsRegex = new(@"\b[a-zA-Zа-яА-ЯёЁ]+\b", RegexOptions.Compiled);

    public void CreateImageWithTags(string pathToText)
    {
        var text = textReaders
            .First(x => x.IsCanRead(pathToText))
            .ReadWords(pathToText);
        var words = DivideOnWords(text);

        foreach (var wordHandler in wordHandlers)
            words = wordHandler.Handle(words);

        var tags = tagLayouter.GetTags(words);
        using var bitmap = visualizer.Visualize(tags);

        imageSaver.Save(bitmap);
    }

    private static IEnumerable<string> DivideOnWords(string text) =>
        GetWordsRegex.Matches(text).Select(x => x.Value);
}