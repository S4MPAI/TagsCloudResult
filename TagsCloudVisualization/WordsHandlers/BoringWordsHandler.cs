using DeepMorphy;

namespace TagsCloudVisualization.WordsHandlers;

public class BoringWordsHandler(MorphAnalyzer analyzer) : IWordHandler
{
    private static readonly HashSet<string> CorrectSpeechParts = ["сущ", "инф_гл", "прил", "деепр"];
    
    public IEnumerable<string> Handle(IEnumerable<string> words) => 
        analyzer
            .Parse(words)
            .Where(x => CorrectSpeechParts.Contains(x["чр"].BestGramKey))
            .Select(x => x.Text);
}