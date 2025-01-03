namespace TagsCloudVisualization.WordsHandlers;

public class WordsInLowerCaseHandler : IWordHandler
{
    public IEnumerable<string> Handle(IEnumerable<string> words) => 
        words.Select(word => word.ToLower());
}