using System;

namespace PutridParrot.Results;

/// <summary>
/// IResult type extensions
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Checks if the result is a success
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool IsSuccess(this IResult result) =>
        result is ISuccess;

    /// <summary>
    /// Checks if the result is a failure
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool IsFailure(this IResult result) =>
        result is IFailure;

    /// <summary>
    /// If the result is a failure, returns
    /// the message
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static string FailureMessage(this IResult result) =>
        result is Failure failure ? failure.Message : string.Empty;

    /// <summary>
    /// Invokes a function within a try catch. If the
    /// function successfully executes a Success is returned
    /// otherwise a Failure with the Exception is returned
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="func"></param>
    /// <returns></returns>
    public static IResult ToResult<TResult>(this Func<TResult> func)
    {
        try
        {
            return new Success<TResult>(func());
        }
        catch (Exception ex)
        {
            return new Failure<Exception>(ex);
        }
    }

    /// <summary>
    /// If this result is Success apply the mapping
    /// function to it
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="result"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static IResult Map<TResult>(this IResult result, Func<IResult, TResult> func)
        where TResult : IResult
    {
        return result.Try(() =>
        {
            if (result.IsSuccess())
            {
                func(result);
            }
        });
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="value"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static IResult ToResult<T, TResult>(this T value, Func<T, TResult> func)
    {
        try
        {
            return new Success<TResult>(func(value));
        }
        catch (Exception ex)
        {
            return new Failure<Exception>(ex);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TException"></typeparam>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static IResult<TException> ToResult<TException>(this TException ex)
        where TException : Exception =>
            new Failure<TException>(ex);

    /// <summary>
    /// Create a Success from the supplied value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static IResult<T> ToSuccess<T>(this T value) =>
        new Success<T>(value);

    /// <summary>
    /// Create a Failure from the supplied value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static IResult<T> ToFailure<T>(this T value) =>
        new Failure<T>(value);

    /// <summary>
    /// Create a Failure from the supplied value and set the message
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static IResult<T> ToFailure<T>(this T value, string message) =>
        new Failure<T>(value, message);

    /// <summary>
    /// Chain a result with a try catch action
    /// </summary>
    /// <param name="result">The initial result</param>
    /// <param name="action">An action to invoke</param>
    /// <returns></returns>
    private static IResult Try(this IResult result, Action action)
    {
        try
        {
            action();

            return result;
        }
        catch (Exception ex)
        {
            return new Failure<Exception>(ex);
        }
    }

    /// <summary>
    /// Chain a result with a try catch function which itself
    /// returns an IResult
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="result"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    private static IResult Try<TValue>(this IResult<TValue> result, Func<TValue, IResult> func)
    {
        try
        {
            return func(result.Value);
        }
        catch (Exception ex)
        {
            return ex.ToFailure();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static IResult OnSuccess(this IResult result, Action action)
    {
        return result.Try(() =>
        {
            if (result.IsSuccess())
            {
                action();
            }
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="result"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static IResult OnSuccess<TValue>(this IResult<TValue> result, Func<TValue, IResult> func)
    {
        return result.Try(value =>
        {
            if (result.IsSuccess())
            {
                return func(value);
            }

            return result;
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static IResult OnFailure(this IResult result, Action action)
    {
        return result.Try(() =>
        {
            if (result.IsFailure())
            {
                action();
            }
        });
    }
}