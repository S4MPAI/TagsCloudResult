using System.Drawing;

namespace TagsCloudVisualization.Options;

public class TagLayouterOptions(int minFontSize, int maxFontSize, FontFamily fontFamily)
{
    public int MinFontSize { get; } = minFontSize;
    public int MaxFontSize { get; } = maxFontSize;
    public FontFamily FontFamily { get; } = fontFamily;
}