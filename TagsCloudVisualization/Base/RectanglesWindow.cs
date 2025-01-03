using System.Drawing;

namespace TagsCloudVisualization.Base;

public class RectanglesWindow
{
    public int Width => _end.X - _position.X;
    public int Height => _end.Y - _position.Y;
    public Point Center => new(_position.X + Width / 2, _position.Y + Height / 2);
    private Point _end = new(int.MinValue, int.MinValue);
    private Point _position = new(int.MaxValue, int.MaxValue);
    
    public RectanglesWindow(IEnumerable<Rectangle> rectangles)
    {
        PutRectangles(rectangles);
    }

    private void PutRectangles(IEnumerable<Rectangle> rectangles)
    {
        foreach (var rectangle in rectangles)
            PutRectangle(rectangle);
    }

    private void PutRectangle(Rectangle rectangle)
    {
        _position = new Point
        {
            X = Math.Min(rectangle.Left, _position.X),
            Y = Math.Min(rectangle.Top, _position.Y)
        };
        
        _end.X = Math.Max(rectangle.Right, _end.X);
        _end.Y = Math.Max(rectangle.Bottom, _end.Y);
    }
}