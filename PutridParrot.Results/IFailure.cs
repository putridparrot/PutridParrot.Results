namespace PutridParrot.Results;

/// <summary>
/// Implemented by Failure types
/// </summary>
public interface IFailure : IResult
{
}

/// <summary>
/// Implemented by Failure types which can be supplied
/// extra data
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IFailure<out T> : IFailure, IResult<T>
{
}