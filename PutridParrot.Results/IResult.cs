namespace PutridParrot.Results;

/// <summary>
/// Implemented by Result types, Success
/// and Failure being examples
/// </summary>
public interface IResult
{
}

/// <summary>
/// Implemented by Result types, Success
/// and Failure being examples, where data
/// is associated with the result
/// </summary>
public interface IResult<out T> : IResult
{
    T Value { get; }
}