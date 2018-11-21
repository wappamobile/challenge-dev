using Wappa.Domain.Interfaces.Models;
using Wappa.Domain.Models;

namespace Wappa.Test.Wappa.Domain
{
    public static class VehicleFake
    {
        internal static IVehicle GetVehicle() =>
             new Vehicle
             {
                 Id = 1,
                 DriverId = 1,
                 Plate = "XXX1234",
                 Model = "Clio",
                 Fabricator = "Renault",
             };
    }
}