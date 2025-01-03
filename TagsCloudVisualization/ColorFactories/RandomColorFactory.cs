using System.Drawing;

namespace TagsCloudVisualization.ColorFactories;

public class RandomColorFactory : IColorFactory
{
    private readonly Random _random = new();
    
    public Color GetColor()
    {
        return Color.FromArgb(
            GetRandomArgbColorComponent(),
            GetRandomArgbColorComponent(),
            GetRandomArgbColorComponent());
    }
    
    private int GetRandomArgbColorComponent() 
        => _random.Next(0, 255);
}