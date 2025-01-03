using System.Drawing;

namespace TagsCloudVisualization.ImageSavers;

public class PngSaver(string path) : IImageSaver
{
    public void Save(Bitmap bitmap) =>
        bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Png);
}