using TagsCloudVisualization.Base;

namespace TagsCloudConsole.Validators;

public interface IValidator<T>
{
    public Result<T> Validate(T obj);
}