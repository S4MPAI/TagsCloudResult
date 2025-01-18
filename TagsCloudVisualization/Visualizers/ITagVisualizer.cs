using System.Drawing;
using TagsCloudVisualization.Base;
using TagsCloudVisualization.Models;

namespace TagsCloudVisualization.Visualizers;

public interface ITagVisualizer
{
    public Result<Bitmap> Visualize(IEnumerable<Tag> tags);
}