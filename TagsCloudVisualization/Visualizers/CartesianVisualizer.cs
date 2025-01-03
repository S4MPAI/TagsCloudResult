using System.Drawing;

namespace TagsCloudVisualization.Visualizers;

public class CartesianVisualizer(Size bitmapSize) : IVisualizer
{
    private const int MinColorComponentValue = 0;
    private const int MaxColorComponentValue = 255;
    private readonly Random _random = new();
    private readonly Point _centerOffset = new(bitmapSize.Width / 2, bitmapSize.Height / 2);

    public Bitmap CreateBitmap(IEnumerable<Rectangle> rectangles)
    {
        var bitmap = new Bitmap(bitmapSize.Width, bitmapSize.Height);
        using var graphics = Graphics.FromImage(bitmap);

        foreach (var rectangle in rectangles)
        {
            rectangle.Offset(_centerOffset);
            using var pen = GetRandomPen();

            graphics.DrawRectangle(pen, rectangle);
        }

        return bitmap;
    }

    private Pen GetRandomPen() =>
        new
        (Color.FromArgb(
            GetRandomArgbColorComponent(),
            GetRandomArgbColorComponent(),
            GetRandomArgbColorComponent())
        );

    private int GetRandomArgbColorComponent() =>
        _random.Next(MinColorComponentValue, MaxColorComponentValue);
}