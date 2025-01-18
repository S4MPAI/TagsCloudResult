using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.Base;

namespace TagsCloudVisualization.ImageSavers;

public class PngSaver(string path) : IImageSaver
{
    public Result<None> Save(Bitmap bitmap) =>
        Result
            .OfAction(() => bitmap.Save(path, ImageFormat.Png))
            .RefineError("Image saver can't save image");
}