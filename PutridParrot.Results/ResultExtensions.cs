namespace PutridParrot.Results;

/// <summary>
/// IResult type extensions
/// </summary>
public static class ResultExtensions
{
    public static bool IsSuccess(this IResult result) =>
        result is ISuccess;

    public static bool IsFailure(this IResult result) =>
        result is IFailure;

    public static string FailureMessage(this IResult result) =>
        result is Failure failure ? failure.Message : string.Empty;
}