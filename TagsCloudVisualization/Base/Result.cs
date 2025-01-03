namespace TagsCloudVisualization.Base;

public class None
{
    private None()
    {
    }
}

public readonly struct Result<TValue>(string error, TValue value = default!)
{
    public TValue Value { get; } = value;
    public string Error { get; } = error;
    public bool IsSuccess => Error == null;

    public static implicit operator Result<TValue>(TValue value) =>
        Result.Ok(value);
}

public static class Result
{
    public static Result<T> AsResult<T>(this T value) =>
        Ok(value);

    public static Result<T> Ok<T>(T value) =>
        new(null!, value);

    public static Result<None> Ok() =>
        Ok<None>(null!);

    public static Result<T> Fail<T>(string error) =>
        new(error);

    public static Result<T> Of<T>(Func<T> func, string? error = null)
    {
        try
        {
            return func();
        }
        catch (Exception e)
        {
            return Fail<T>(error ?? e.Message);
        }
    }

    public static Result<T> Then<T>(this Result<T> result, Func<T, Result<T>> func) =>
        result.IsSuccess ? func(result.Value) : Fail<T>(result.Error);

    public static Result<T> RefineError<T>(this Result<T> result, string error)
        => result.IsSuccess ? result : Fail<T>($"{error}. {result.Error}");
}