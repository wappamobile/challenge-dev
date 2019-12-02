
namespace Wappa.Teste.Project.Core.Models
{
    public class DriverModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Surname { get; set; }

        public AddressModel Address { get; set; }
        public VehicleModel Vehicle { get; set; }
    }
}
