using System.Collections.Generic;
using System.Linq;

namespace Wappa.Domain.Messages
{
    public class Response
    {
        public Response() =>
            Messages = new List<string>();

        public Response(object obj) : this()
        {
            AddValue(obj);
        }

        public bool HasMessages => Messages.Any();
        public IList<string> Messages { get; private set; }

        public object Value { get; private set; }

        public Response AddNotification(string notification)
        {
            Messages.Add(notification);
            return this;
        }

        public Response AddValue(object @object)
        {
            Value = @object;
            return this;
        }

        public override string ToString() =>
            string.Join(" - ", Messages);
    }
}