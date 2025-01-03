using System.Drawing;

namespace TagsCloudVisualization.ImageSavers;

public interface IImageSaver
{
    public void Save(Bitmap bitmap);
}