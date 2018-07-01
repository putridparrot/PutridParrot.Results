using System;
using System.Collections.Generic;
using System.Linq;

namespace PutridParrot.Results
{
    /// <summary>
    /// An implementation of a ICompositeResult. The basic implementation
    /// is suitable for most requirements of aggregating and querying 
    /// against the results
    /// </summary>
    /// <typeparam name="T">The result value type</typeparam>
    /// <typeparam name="TMessage">The result message type</typeparam>
    public class CompositeResult<T, TMessage> : ICompositeResult<T, TMessage>
    {
        private IList<IResult<T, TMessage>> _results;

        /// <summary>
        /// Creates a CompositeResult
        /// </summary>
        public CompositeResult() :
            base()
        {
        }

        public IList<IResult<T, TMessage>> Results => _results ?? (_results = new List<IResult<T, TMessage>>());

        /// <summary>
        /// Determine's the composite status of the results. If
        /// all are Success then the result is success, if 
        /// a single failure exists then the result is a Failure
        /// otherwise the composite result is Undefined and you
        /// need to iterate the Results for yourself.
        /// </summary>
        public ResultStatus Status
        {
            get
            {
                var groups = Results.GroupBy(r => r.Status).ToArray();

                if (groups.Any(g => g.Key == ResultStatus.Failure))
                    return ResultStatus.Failure;
                if (groups.All(g => g.Key == ResultStatus.Success))
                    return ResultStatus.Success;

                return ResultStatus.Undefined;
            }
        }

        /// <summary>
        /// Gets all messages across the aggregated results
        /// </summary>
        public IList<TMessage> Messages
        {
            get
            {
                var list = new List<TMessage>();
                foreach (var r in Results)
                {
                    list.AddRange(r.Messages);
                }
                return list;
            }
        }

        /// <summary>
        /// Gets a result value only if all results return the same value. 
        /// </summary>
        public T Value
        {
            get
            {
                if (Results.Count == 0)
                    throw new Exception("No results exist");

                var firstValue = Results[0].Value;
                if (Results.Count == 1)
                    return firstValue;

                if (Results.All(r => EqualityComparer<T>.Default.Equals(r.Value, firstValue)))
                    return firstValue;

                throw new Exception("Multiple result values exist");
            }
        }

        /// <summary>
        /// Checks if any results have the supplied ResultStatus
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool Any(ResultStatus status)
        {
            return Results.Any(r => r.Status == status);
        }

        /// <summary>
        /// Gets an enumerator for all result values
        /// </summary>
        public IEnumerable<T> Values
        {
            get { return Results.Select(r => r.Value); }
        }
    }
}
