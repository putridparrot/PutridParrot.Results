namespace PutridParrot.Results;

/// <summary>
/// A basic failure result, it may be assigned a message
/// explaining the failure
/// </summary>
public class Failure : IFailure
{
    public Failure() :
        this(string.Empty)
    {
    }

    public Failure(string message)
    {
        Message = message;
    }

    public string Message { get; }
}

/// <summary>
/// A Failure which can be supplied with a value
/// and message. The value may be something simple
/// like a result code or a more complex type.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Failure<T> : Failure, IFailure<T>
{
    public Failure(T result) :
        this(result, string.Empty)
    {
    }

    public Failure(T value, string message) :
        base(message)
    {
        Value = value;
    }

    public T Value { get; }
}