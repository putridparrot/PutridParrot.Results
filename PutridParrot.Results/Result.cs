namespace PutridParrot.Results;

public static class Result
{
    private static readonly Success SharedSuccess = new ();

    public static Success Success() => SharedSuccess;
    public static Success<T> Success<T>(T value) => new(value);
    public static Failure Failure(string? message = null) => new (message);
    public static Failure<T> Failure<T>(T value, string? message = null) => new(value, message);
}