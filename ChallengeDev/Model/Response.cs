namespace ChallengeDev.Model
{
    /// <summary>
    /// Generic Response
    /// </summary>
    public class ResponseObject<T>
    {
        /// <summary>
        /// Gets or sets the Response message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the generic response object.
        /// </summary>
        /// <value>The response.</value>
        public T Response { get; set; }
    }
}
