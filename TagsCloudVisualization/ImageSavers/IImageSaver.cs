using System.Drawing;
using TagsCloudVisualization.Base;

namespace TagsCloudVisualization.ImageSavers;

public interface IImageSaver
{
    public Result<None> Save(Bitmap bitmap);
}