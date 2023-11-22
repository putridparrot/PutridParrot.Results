using System;

namespace PutridParrot.Results;

/// <summary>
/// A basic failure result, it may be assigned a message
/// explaining the failure
/// </summary>
public class Failure : IFailure
{
    internal Failure(string? message = null)
    {
        Message = message ?? String.Empty;
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
    internal Failure(T value, string? message = null) :
        base(message)
    {
        Value = value;
    }

    public T Value { get; }
}