using System.Drawing;
using TagsCloudVisualization.PointsGenerators;

namespace TagsCloudVisualization.Layouters;

public class CircularCloudLayouter : ICloudLayouter
{
    private readonly IEnumerator<Point> _pointsIterator;
    private readonly List<Rectangle> _rectangles = [];

    public CircularCloudLayouter(Point center, double radius, double angleOffset)
    {
        var pointsGenerator = new ArchimedeanSpiralPointsGenerator(radius, angleOffset);
        _pointsIterator = pointsGenerator.GeneratePoints(center).GetEnumerator();
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        Rectangle rectangle;
        do
        {
            _pointsIterator.MoveNext();
            var rectanglePos = _pointsIterator.Current;
            rectangle = CreateRectangleWithCenter(rectanglePos, rectangleSize);
        } while (_rectangles.Any(rectangle.IntersectsWith));
        
        _rectangles.Add(rectangle);
        
        return rectangle;
    }

    private static Rectangle CreateRectangleWithCenter(Point centerPos, Size rectangleSize)
    {
        var xPos = centerPos.X - rectangleSize.Width / 2;
        var yPos = centerPos.Y - rectangleSize.Height / 2;
            
        return new Rectangle(xPos, yPos, rectangleSize.Width, rectangleSize.Height);
    }
}