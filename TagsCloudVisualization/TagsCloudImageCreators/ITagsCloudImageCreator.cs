using TagsCloudVisualization.Base;

namespace TagsCloudVisualization.TagsCloudImageCreators;

public interface ITagsCloudImageCreator
{
    Result<None> CreateImageWithTags(string pathToText);
}