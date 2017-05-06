
using System.Collections.Generic;

namespace Results
{
    /// <summary>
    /// A result may take different forms, they just need
    /// to implement the IResult interface. A Result 
    /// implementation exists for most requirements
    /// </summary>
    /// <typeparam name="T">The type of the Value returned</typeparam>
    /// <typeparam name="TMessage">The type of any messages returned</typeparam>
    public interface IResult<out T, TMessage>
    {
        /// <summary>
        /// Gets the status of the return value
        /// </summary>
        ResultStatus Status { get; }
        /// <summary>
        /// Gets messages associated with the return
        /// (if any exist)
        /// </summary>
        IList<TMessage> Messages { get; }
        /// <summary>
        /// Gets the return value of the given type
        /// </summary>
        T Value { get; }
    }
}
