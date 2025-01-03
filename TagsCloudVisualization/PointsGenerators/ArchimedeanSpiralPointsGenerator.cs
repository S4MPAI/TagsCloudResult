using System.Drawing;
using TagsCloudVisualization.Base;

namespace TagsCloudVisualization.PointsGenerators;

public class ArchimedeanSpiralPointsGenerator : IPointsGenerator
{
    private readonly double _offsetPerRadian;
    private readonly double _radiansAngleOffset;
    
    public ArchimedeanSpiralPointsGenerator(double radius, double angleOffset)
    {
        if (radius <= 0)
            throw new ArgumentException($"{nameof(radius)} must be greater than 0");
        if (angleOffset == 0)
            throw new ArgumentException($"{nameof(angleOffset)} must not be 0");
        
        _offsetPerRadian = PolarMath.GetOffsetPerRadianForArchimedeanSpiral(radius);
        _radiansAngleOffset = PolarMath.ConvertToRadians(angleOffset);
    }
    
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