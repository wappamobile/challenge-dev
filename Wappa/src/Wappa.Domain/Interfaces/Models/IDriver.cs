namespace Wappa.Domain.Interfaces.Models
{
    public interface IDriver
    {
        int? Id { get; set; }
        string Document { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}