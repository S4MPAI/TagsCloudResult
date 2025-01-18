using System.Drawing;
using System.Text.RegularExpressions;
using TagsCloudVisualization.Base;
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

    public Result<None> CreateImageWithTags(string pathToText)
    {
        return Result
            .Of(() => textReaders
                    .First(x => x.IsCanRead(pathToText))
                    .ReadWords(pathToText),
                $"Can't read tags on file with {Path.GetExtension(pathToText)} extension")
            .Then(DivideOnWords)
            .Then(ApplyHandlers)
            .Then(tagLayouter.GetTags)
            .Then(visualizer.Visualize)
            .Then(SaveImage)
            .RefineError("Image creator can't create tas cloud image");
    }

    private static IEnumerable<string> DivideOnWords(string text) =>
        GetWordsRegex.Matches(text).Select(x => x.Value);

    private IEnumerable<string> ApplyHandlers(IEnumerable<string> words)
    {
        foreach (var wordHandler in wordHandlers)
            words = wordHandler.Handle(words);

        return words;
    }

    private Result<None> SaveImage(Bitmap bitmap)
    {
        using (bitmap)
        {
            return imageSaver.Save(bitmap);
        }
    }
}