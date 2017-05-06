using System;
using System.Collections.Generic;

namespace Results
{
    /// <summary>
    /// Fluent interface for a those who prefer such an interface. This class
    /// allows the developer to build (as the name suggests) the Result object
    /// using more human readable syntax.
    /// </summary>
    /// <typeparam name="T">The Value type within the IResult</typeparam>
    /// <typeparam name="TMessage">The Message type within the IResult</typeparam>
    public class ResultBuilder<T, TMessage>
    {
        /// <summary>
        ///  Used for lazy creation of the IResult
        /// </summary>
        private readonly List<Action<Result<T, TMessage>>> _actions = new List<Action<Result<T, TMessage>>>();

        /// <summary>
        /// Create a ResultBuilder with a a ResultStatus
        /// </summary>
        /// <param name="status">The ResultStatus to initialize the IResult to</param>
        private ResultBuilder(ResultStatus status)
        {
            WithStatus(status);
        }

        /// <summary>
        /// Create a ResultBuilder with the ResultStatus Success
        /// </summary>
        /// <returns>The ResultBuilder for chaining</returns>
        public static ResultBuilder<T, TMessage> Success()
        {
            return new ResultBuilder<T, TMessage>(ResultStatus.Success);
        }

        /// <summary>
        /// Create a ResultBuilder with the ResultStatus Failure
        /// </summary>
        /// <returns>A ResultBuilder to allow chaining</returns>
        public static ResultBuilder<T, TMessage> Failure()
        {
            return new ResultBuilder<T, TMessage>(ResultStatus.Failure);
        }

        /// <summary>
        /// Create a ResultBuilder with the ResultStatus to Undefined
        /// </summary>
        /// <returns>A ResultBuilder to allow chaining</returns>
        public static ResultBuilder<T, TMessage> Undefined()
        {
            return new ResultBuilder<T, TMessage>(ResultStatus.Undefined);
        }

        /// <summary>
        /// Sets the ResultStatus
        /// </summary>
        /// <param name="status">The ResultStatus to be assigned</param>
        /// <returns>A ResultBuilder to allow chaining</returns>
        public ResultBuilder<T, TMessage> WithStatus(ResultStatus status)
        {
            _actions.Add(a => a.Status = status);
            return this;
        }

        /// <summary>
        /// Sets the Value
        /// </summary>
        /// <param name="value"></param>
        /// <returns>A ResultBuilder to allow chaining</returns>
        public ResultBuilder<T, TMessage> WithValue(T value)
        {
            _actions.Add(a => a.Value = value);
            return this;
        }

        /// <summary>
        /// Sets a message on the Result
        /// </summary>
        /// <param name="message"></param>
        /// <returns>A ResultBuilder to allow chaining</returns>
        public ResultBuilder<T, TMessage> WithMessage(TMessage message)
        {
            _actions.Add(a => a.Messages.Add(message));
            return this;
        }

        /// <summary>
        /// Sets multiple messages on a Result
        /// </summary>
        /// <param name="messages">The messages to set</param>
        /// <returns>A ResultBuilder to allow chaining</returns>
        public ResultBuilder<T, TMessage> WithMessages(TMessage[] messages)
        {
            foreach (var message in messages)
            {
                WithMessage(message);
            }
            return this;
        }

        /// <summary>
        /// Lazy creation of the Result based upon the WithXX etc, method chaining
        /// </summary>
        /// <returns>An IResult setup as per the builder</returns>
        public IResult<T, TMessage> Build()
        {
            var result = new Result<T, TMessage>();
            foreach (var action in _actions)
            {
                action(result);
            }
            return result;
        }
    }
}
