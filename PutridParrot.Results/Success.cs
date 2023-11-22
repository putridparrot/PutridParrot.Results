namespace PutridParrot.Results;

public class Success : ISuccess
{
    internal Success()
    {
    }
}

public class Success<T> : Success, ISuccess<T>
{
    internal Success(T value)
    {
        Value = value;
    }

    public T Value { get; }
}
