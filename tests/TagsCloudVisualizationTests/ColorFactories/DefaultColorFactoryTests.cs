using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.ColorFactories;

namespace TagsCloudVisualizationTests.ColorFactories;

public class DefaultColorFactoryTests
{
    [TestCaseSource(nameof(_getColorTestCases))]
    public Color GetColor_ShouldReturnExpectedColor(string colorName)
    {
        var factory = new DefaultColorFactory(colorName);

        var actualColor = factory.GetColor();

        return actualColor;
    }

    private static TestCaseData[] _getColorTestCases = [
        new TestCaseData("black").Returns(Color.Black),
        new TestCaseData("white").Returns(Color.White),
        new TestCaseData("crimson").Returns(Color.Crimson),
        new TestCaseData("blue").Returns(Color.Blue),
    ];

    [Test]
    public void GetColor_ShouldReturnDefaultColor_WhenColorNameIncorrect()
    {
        var colorName = "notColor";
        var factory = new DefaultColorFactory(colorName);

        var actualColor = factory.GetColor();

        actualColor.IsKnownColor.Should().Be(false);
        actualColor.ToArgb().Should().Be(0);
    }
}