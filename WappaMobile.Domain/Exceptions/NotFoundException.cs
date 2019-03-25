using System;
namespace WappaMobile.Domain
{
    [System.Serializable]
    public class NotFoundException : WappaMobileException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:WappaMobile.Domain.NotFoundException"/> class.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="id">Identifier.</param>
        public NotFoundException(string entity, string id) : base($"{entity} with id {id} not found.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NotFoundException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        public NotFoundException(string message) : base(message)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NotFoundException"/> class
        /// </summary>
        /// <param name="context">The contextual information about the source or destination.</param>
        /// <param name="info">The object that holds the serialized object data.</param>
        protected NotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
