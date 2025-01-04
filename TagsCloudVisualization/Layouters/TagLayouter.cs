using System.Drawing;
using TagsCloudVisualization.Models;
using TagsCloudVisualization.Options;

namespace TagsCloudVisualization.Layouters;

public class TagLayouter(ICloudLayouter cloudLayouter, TagLayouterOptions options) : ITagLayouter
{
    private readonly int _minFontSize = options.MinFontSize;
    private readonly int _maxFontSize = options.MaxFontSize;
    private readonly FontFamily _fontFamily = options.FontFamily;
    private int FontSizeOffset => _maxFontSize - _minFontSize;
    private readonly Graphics _graphics = Graphics.FromHwnd(IntPtr.Zero);

    public IEnumerable<Tag> GetTags(IEnumerable<string> words)
    {
        var wordsCounts = words
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.Count());
        var maxCount = wordsCounts.Max(x => x.Value);
        var minCount = wordsCounts.Min(x => x.Value);

        foreach (var (word, count) in wordsCounts)
        {
            var fontSize = GetFontSize(count, minCount, maxCount);
            yield return new Tag(
                word,
                fontSize,
                _fontFamily,
                cloudLayouter.PutNextRectangle(GetWordSize(word, fontSize)));
        }
    }

    private int GetFontSize(int count, int minWordCount, int maxWordCount) =>
        _minFontSize + (count - minWordCount) * FontSizeOffset / (maxWordCount - minWordCount);

    private Size GetWordSize(string word, int fontSize) =>
        Size.Ceiling(_graphics.MeasureString(word, new Font(_fontFamily, fontSize)));
}