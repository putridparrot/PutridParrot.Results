
using System.Collections.Generic;

namespace PutridParrot.Results
{
    /// <summary>
    /// Interface which allows us to implement result composition/aggregation.
    /// All IResults are expected to be of the same value type and same message
    /// type. The ICompositeResult is itself an IResult
    /// </summary>
    /// <typeparam name="T">The value type for all aggregated results</typeparam>
    /// <typeparam name="TMessage">The message type for all aggregated results</typeparam>
    public interface ICompositeResult<out T, TMessage> : IResult<T, TMessage>
    {
        /// <summary>
        /// Determines if any of the results match the 
        /// supplied result status 
        /// </summary>
        /// <param name="status"></param>
        /// <returns>True if the status exists, false otherwise</returns>
        bool Any(ResultStatus status);
        /// <summary>
        /// Returns all values, across all results
        /// </summary>
        IEnumerable<T> Values { get; }
    }
}