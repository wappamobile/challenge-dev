using MediatR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Wappa.Domain.Messages;

namespace Wappa.Application.CommandRequests
{
    public class QueryDriverRequest : IRequest<Response>
    {
        public QueryDriverRequest()
        {
            Single = false;
        }

        public QueryDriverRequest(int id) : this()
        {
            Id = id;
            Single = true;
        }

        public QueryDriverRequest(string document) : this()
        {
            Single = true;
            Document = document;
            Id = null;
        }

        [JsonIgnore]
        public bool Single { get; private set; }

        [StringLength(11, MinimumLength = 11)]
        [Display(Name = "Document")]
        public string Document { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Ìd")]
        public int? Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}