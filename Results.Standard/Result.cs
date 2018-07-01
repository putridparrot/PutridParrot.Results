using System;
using System.Collections.Generic;
using System.Linq;

namespace PutridParrot.Results
{
    /// <summary>
    /// A simple and general implementation of the IResult. This is 
    /// probably suitable for most requirements
    /// </summary>
    /// <typeparam name="T">The return type</typeparam>
    /// <typeparam name="TMessage">The type of messages that the method creates</typeparam>
    public class Result<T, TMessage> : IResult<T, TMessage>
    {
        private IList<TMessage> _messages;

        /// <summary>
        /// Creates a Result in a default, Undefined status state
        /// </summary>
        public Result()
        {
            Status = ResultStatus.Undefined;
        }

        /// <summary>
        /// Creates a Result with the supplied status
        /// </summary>
        /// <param name="status"></param>
        public Result(ResultStatus status)
            : this()
        {
            Status = status;
        }

        /// <summary>
        /// Creates a Result with the supplied status an return value
        /// </summary>
        /// <param name="status"></param>
        /// <param name="value"></param>
        public Result(ResultStatus status, T value)
            : this(status)
        {
            Value = value;
        }

        /// <summary>
        /// Creates a Result with the supplie value, the status will
        /// be Undefined, hence require setting
        /// </summary>
        /// <param name="value"></param>
        public Result(T value)
            : this(ResultStatus.Undefined, value)
        {
        }

        /// <summary>
        /// Gets the status of the result/return
        /// </summary>
        public ResultStatus Status { get; set; }
        /// <summary>
        /// Gets the value of the result/return
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Gets any messages supplied with the Result
        /// </summary>
        public IList<TMessage> Messages => _messages ?? (_messages = new List<TMessage>());

        /// <summary>
        /// Turns the messages into a string seperated by newlines
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Join("\r\n", Messages.Select(m => m.ToString()));
        }
    }

    /// <summary>
    /// For scenarios where no return value is required/expected. Equivalient
    /// to a return of void, but still allows us to supply a return status 
    /// and messages
    /// </summary>
    /// <typeparam name="TMessage">The message type returned by the method</typeparam>
    public class Result<TMessage> : Result<object, TMessage>
    {
        /// <summary>
        /// Creates a Result with a given status
        /// </summary>
        /// <param name="status">The ResultStatus to assign</param>
        public Result(ResultStatus status)
            : base(status)
        {
        }
    }

    /// <summary>
    /// Result similar to a void Result which still return
    /// a status and messages. The messages are simply 
    /// expected to by of type string
    /// </summary>
    public class Result : Result<object, string>
    {
        /// <summary>
        /// Creates a Result with Undefined status
        /// </summary>
        public Result() :
            base()
        {
        }

        /// <summary>
        /// Creates a Result with a supplied status
        /// </summary>
        /// <param name="status">The ResultStatus to assign</param>
        public Result(ResultStatus status)
            : base(status)
        {
        }
    }
}


