namespace TagsCloudVisualization.TextReaders;

public interface ITextReader
{
    public bool IsCanRead(string filePath);
    public string ReadWords(string filePath);
}