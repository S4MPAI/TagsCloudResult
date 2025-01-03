using System.Drawing;

namespace TagsCloudVisualization.ColorFactories;

public class DefaultColorFactory(string colorName) : IColorFactory
{
    public Color GetColor() => 
        Color.FromName(colorName);
}