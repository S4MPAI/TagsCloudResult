using System.Drawing;
using TagsCloudVisualization.Base;

namespace TagsCloudConsole.Validators;

public class OptionsValidator : IValidator<TagsCloudVisualizationOptions>
{
    public Result<TagsCloudVisualizationOptions> Validate(TagsCloudVisualizationOptions obj)
    {
        return obj
            .AsResult()
            .Then(ValidateDictionaryPath)
            .Then(ValidateInputFilePath)
            .Then(ValidateOutputFilePath)
            .Then(ValidateTagLayouterOptions)
            .Then(ValidateCircularCloudLayouterOptions)
            .Then(ValidateImageSize)
            .RefineError("Incorrect program input parameters");
    }

        private static Result<TagsCloudVisualizationOptions> ValidateDictionaryPath(TagsCloudVisualizationOptions options) =>
        Validate(
            options,
            o => Path.Exists(o.DictionaryPath),
            $"Not found dictionary on path: {Path.GetFullPath(options.DictionaryPath)}");

    private static Result<TagsCloudVisualizationOptions> ValidateInputFilePath(TagsCloudVisualizationOptions options) =>
        Validate(
            options,
            o => Path.Exists(o.InputFilePath),
            $"Not found input file: {Path.GetFullPath(options.InputFilePath)}");

    private static Result<TagsCloudVisualizationOptions> ValidateOutputFilePath(TagsCloudVisualizationOptions options) =>
        Validate(
            options,
            o => Path.HasExtension(o.OutputFilePath) && Path.Exists(Path.GetDirectoryName(o.OutputFilePath)),
            $"Incorrect output file directory: {Path.GetFullPath(options.OutputFilePath)}");

    private static Result<TagsCloudVisualizationOptions> ValidateTagLayouterOptions(TagsCloudVisualizationOptions options) =>
        Validate(options,
                o =>
                {
                    try
                    {
                        new FontFamily(o.FontFamily);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                },
                @"Incorrect font family name. Available fonts can be found in: C:\Windows\Fonts")
            .Then(value => Validate(value,
                v => v.MinFontSize > 0,
                $"{nameof(value.MinFontSize)} must be more than zero."))
            .Then(value => Validate(value,
                v => v.MaxFontSize >= v.MinFontSize,
                $"{nameof(options.MinFontSize)} must be less or equal {nameof(options.MaxFontSize)}"))
            .RefineError("Incorrect tag layouter options");

    private static Result<TagsCloudVisualizationOptions> ValidateCircularCloudLayouterOptions(TagsCloudVisualizationOptions options) =>
        Validate(options,
                o => o.LayoutRadius > 0,
                $"{nameof(options.LayoutRadius)} must be greater than 0")
            .Then(v =>
                Validate(
                    v,
                    o => o.LayoutAngleOffset != 0,
                    $"{nameof(v.LayoutAngleOffset)} must not be 0"))
            .RefineError("Incorrect circular layouter options");

    private static Result<TagsCloudVisualizationOptions> ValidateImageSize(TagsCloudVisualizationOptions options) =>
        Validate(
            options,
            o => o.ImageWidth > 0 && o.ImageHeight > 0,
            $"Image width and height must be greater than zero. Current Size: {options.ImageWidth}x{options.ImageHeight}");

    private static Result<T> Validate<T>(T obj, Func<T, bool> predicate, string error) =>
        predicate(obj)
            ? Result.Ok(obj)
            : Result.Fail<T>(error);
}