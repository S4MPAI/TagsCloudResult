namespace TagsCloudVisualization.WordsHandlers;

public interface IWordHandler
{
    public IEnumerable<string> Handle(IEnumerable<string> words);
}