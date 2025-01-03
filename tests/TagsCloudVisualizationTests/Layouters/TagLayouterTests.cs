using System.Drawing;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Models;
using TagsCloudVisualization.Options;

namespace TagsCloudVisualizationTests.Layouters;

public class TagLayouterTests
{
    private ICloudLayouter _cloudLayouterMock;
    private Rectangle[] _wordPlaces;

    [SetUp]
    public void SetUp()
    {
        _cloudLayouterMock = A.Fake<ICloudLayouter>();
        _wordPlaces = new Rectangle[]
        {
            new(5, 5, 20, 20),
            new(10, 10, 50, 50),
            new(15, 15, 30, 30),
        };
        A.CallTo(() => _cloudLayouterMock
            .PutNextRectangle(A<Size>.Ignored))
            .ReturnsNextFromSequence(_wordPlaces);
    }

    [TestCase(-1, 5, Description = "WhenMinFontSizeLessThanZero")]
    [TestCase(10, 5, Description = "WhenMinFontMoreThanMaxFontSize")]
    public void Constructor_ShouldThrowArgumentException(int minFonSize, int maxFonSize)
    {
        var options = new TagLayouterOptions(minFonSize, maxFonSize, "Arial");
        var constructor = () => new TagLayouter(_cloudLayouterMock, options);

        constructor.Should().Throw<ArgumentException>();
    }


    [Test]
    public void GetTags_ShouldReturnTags()
    {
        var a = new TagLayouter(_cloudLayouterMock, new TagLayouterOptions(10, 20, "Arial"));
        var words = new List<string> { "слово", "одежда", "ежевика", "слово", "слово", "одежда" };
        var fontFamily = new FontFamily("Arial");

        var actualTags = a.GetTags(words).ToList();
        var expectedTags = new Tag[]
        {
            new("слово", 20, fontFamily, _wordPlaces[0]),
            new("одежда", 15, fontFamily, _wordPlaces[1]),
            new("ежевика", 10, fontFamily, _wordPlaces[2])
        };

        actualTags.Count.Should().Be(3);
        for (var i = 0; i < actualTags.Count; i++)
            actualTags[i].Should().BeEquivalentTo(expectedTags[i]);
    }
}