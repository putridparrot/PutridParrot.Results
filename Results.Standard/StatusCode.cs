namespace Results
{
    /// <summary>
    /// A basic implementation of a status code message
    /// </summary>
    public class StatusCode
    {
        /// <summary>
        /// Creates an immutable StatusCode which contains a
        /// numeric (int) code and a string message. 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public StatusCode(int code, string message)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// Gets the code
        /// </summary>
        public int Code { get; private set; }
        /// <summary>
        /// Gets the message
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Override to return a simple key value style
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Code}:{Message}";
        }
    }
}