namespace TagsCloudVisualization.TextReaders;

public class TxtReader : ITextReader
{
    public bool IsCanRead(string filePath) =>
        filePath.EndsWith(".txt");

    public string ReadWords(string filePath) =>
        File.ReadAllText(filePath);
}