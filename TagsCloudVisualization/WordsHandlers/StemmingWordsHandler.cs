using WeCantSpell.Hunspell;

namespace TagsCloudVisualization.WordsHandlers;

public class StemmingWordsHandler(WordList dictionary) : IWordHandler
{
    public IEnumerable<string> Handle(IEnumerable<string> words)
    {
        foreach (var word in words)
        {
            var lemma = dictionary.CheckDetails(word).Root;
            
            yield return string.IsNullOrWhiteSpace(lemma) ? word : lemma;
        }
    }       
}