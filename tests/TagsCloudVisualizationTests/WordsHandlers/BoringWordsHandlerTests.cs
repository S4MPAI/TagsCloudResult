using DeepMorphy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordsHandlers;

namespace TagsCloudVisualizationTests.WordsHandlers;

public class BoringWordsHandlerTests
{
    [Test]
    public void Handle_ShouldReturnExpectedWords()
    {
        var words = new[] {"В", "тарелка", "лежать", "красивая", "апельсинка", "и", "я", "ее", "скушать" };
        var handler = new BoringWordsHandler(new MorphAnalyzer());
        
        var actualWords = handler.Handle(words);
        var expectedWords = new[] { "тарелка", "лежать", "красивая", "апельсинка", "скушать" };
        
        actualWords.Should().BeEquivalentTo(expectedWords);
    }
}