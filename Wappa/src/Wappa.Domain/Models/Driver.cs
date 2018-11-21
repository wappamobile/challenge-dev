using Wappa.Domain.Interfaces.Models;

namespace Wappa.Domain.Models
{
    public class Driver : IDriver
    {
        public Driver()
        {
            Id = 0;
            Document = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
        }

        public int? Id { get; set; }
        public string Document { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}