using TagsCloudVisualization.Models;

namespace TagsCloudVisualization.Layouters;

public interface ITagLayouter
{
    public IEnumerable<Tag> GetTags(IEnumerable<string> words);
}