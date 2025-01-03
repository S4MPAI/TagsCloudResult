using System.Drawing;
using TagsCloudVisualization.Models;

namespace TagsCloudVisualization.Visualizers;

public interface ITagVisualizer
{
    public Bitmap Visualize(IEnumerable<Tag> tags);
}