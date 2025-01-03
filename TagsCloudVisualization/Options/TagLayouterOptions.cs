using System.Drawing;

namespace TagsCloudVisualization.Options;

public class TagLayouterOptions(int minFontSize, int maxFontSize, string fontName)
{
    public int MinFontSize { get; } = minFontSize;
    public int MaxFontSize { get; } = maxFontSize;
    public FontFamily FontFamily { get; } = ConvertFontNameToFontFamily(fontName);

    private static FontFamily ConvertFontNameToFontFamily(string fontName)
    {
        try
        {
            return new FontFamily(fontName);
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException($"Font name '{fontName}' is invalid", e);
        }
    }
}