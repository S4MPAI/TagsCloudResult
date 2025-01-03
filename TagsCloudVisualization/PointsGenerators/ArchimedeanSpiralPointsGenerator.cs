using System.Drawing;
using TagsCloudVisualization.Base;

namespace TagsCloudVisualization.PointsGenerators;

public class ArchimedeanSpiralPointsGenerator(double radius, double angleOffset) : IPointsGenerator
{
    private readonly double _offsetPerRadian = PolarMath.GetOffsetPerRadianForArchimedeanSpiral(radius);
    private readonly double _radiansAngleOffset = PolarMath.ConvertToRadians(angleOffset);

    public IEnumerable<Point> GeneratePoints(Point startPoint)
    {
        var radiansAngle = 0d;

        while (true)
        {
            var polarRadius = _offsetPerRadian * radiansAngle;
            var pointOnSpiral = PolarMath.ConvertToCartesianCoordinateSystem(polarRadius, radiansAngle);
            pointOnSpiral.Offset(startPoint);

            yield return pointOnSpiral;

            radiansAngle += _radiansAngleOffset;
        }
        // ReSharper disable once IteratorNeverReturns
    }
}