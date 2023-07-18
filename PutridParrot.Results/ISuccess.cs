namespace PutridParrot.Results;

/// <summary>
/// Implemented by Success types
/// </summary>
public interface ISuccess : IResult
{
}

/// <summary>
/// Implemented by Success types which can be supplied
/// extra data
/// </summary>
public interface ISuccess<out T> : ISuccess, IResult<T>
{
}