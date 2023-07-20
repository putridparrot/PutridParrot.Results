//using System;

//namespace PutridParrot.Results;

///// <summary>
///// Wraps results and non-IResult code in a sort of
///// fluent style interface
///// </summary>
//public static class FluentResultExtensions
//{
//    /// <summary>
//    /// Try/Catch a function call and wrap the result as an IResult
//    /// </summary>
//    /// <typeparam name="TValue"></typeparam>
//    /// <param name="value"></param>
//    /// <param name="map"></param>
//    /// <returns></returns>
//    public static IResult Try<TValue>(this TValue value, Func<TValue, TValue> map)
//    {
//        try
//        {
//            return new Success<TValue>(map(value));
//        }
//        catch (Exception ex)
//        {
//            return new Failure<Exception>(ex);
//        }
//    }

//    public static IResult Try<TValue>(this TValue value, Func<TValue, IResult> map)
//    {
//        try
//        {
//            return map(value);
//        }
//        catch (Exception ex)
//        {
//            return new Failure<Exception>(ex);
//        }
//    }

//    /// <summary>
//    /// Try/Catch a action call and wrap the result as an IResult
//    /// </summary>
//    /// <param name="action"></param>
//    /// <returns></returns>
//    public static IResult Try(Action action)
//    {
//        try
//        {
//            action();
//            return new Success();
//        }
//        catch (Exception ex)
//        {
//            return new Failure<Exception>(ex);
//        }
//    }

//    /// <summary>
//    /// Try/Catch a function call and wrap the result as an IResult
//    /// </summary>
//    /// <typeparam name="TReturnValue"></typeparam>
//    /// <param name="func"></param>
//    /// <returns></returns>
//    public static IResult Try<TReturnValue>(Func<TReturnValue> func)
//    {
//        try
//        {
//            return new Success<TReturnValue>(func());
//        }
//        catch (Exception ex)
//        {
//            return new Failure<Exception>(ex);
//        }
//    }

//    public static IResult Try(Func<IResult> func)
//    {
//        try
//        {
//            return func();
//        }
//        catch (Exception ex)
//        {
//            return new Failure<Exception>(ex);
//        }
//    }

//    /// <summary>
//    /// Chain a result so that if it's a success then
//    /// invoke a function which may be wrapped in an exception
//    /// </summary>
//    /// <typeparam name="TValue"></typeparam>
//    /// <typeparam name="TReturnValue"></typeparam>
//    /// <param name="result">If the result is a success returns a new Success otherwise returns the Failure</param>
//    /// <param name="func">The function to call with the result value, the result is wrapped in a
//    /// Success unless and exception occurs</param>
//    /// <returns></returns>
//    public static IResult OnSuccess<TValue, TReturnValue>(this IResult<TValue> result, Func<TValue, TReturnValue> func)
//    {
//        try
//        {
//            if (result.IsSuccess())
//            {
//                return new Success<TReturnValue>(func(result.Value));
//            }

//            return result;
//        }
//        catch (Exception ex)
//        {
//            return new Failure<Exception>(ex);
//        }
//    }

//    public static IResult OnSuccess<TValue>(this IResult<TValue> result, Func<TValue, IResult> func)
//    {
//        try
//        {
//            if (result.IsSuccess())
//            {
//                return func(result.Value);
//            }

//            return result;
//        }
//        catch (Exception ex)
//        {
//            return new Failure<Exception>(ex);
//        }
//    }

//    /// <summary>
//    /// Chain a result so that if it's a failure then
//    /// invoke an action which may be wrapped in an exception
//    /// </summary>
//    /// <typeparam name="TValue"></typeparam>
//    /// <typeparam name="TReturnValue"></typeparam>
//    /// <param name="result">The current result is always returned</param>
//    /// <param name="action">Action to carry out if the result is a failure</param>
//    /// <returns></returns>
//    public static IResult OnFailure<TValue, TReturnValue>(this IResult<TValue> result, Action<TValue> action)
//    {
//        try
//        {
//            if (result.IsFailure())
//            {
//                action(result.Value);
//            }

//            return result;
//        }
//        catch (Exception ex)
//        {
//            return new Failure<Exception>(ex);
//        }
//    }

//}