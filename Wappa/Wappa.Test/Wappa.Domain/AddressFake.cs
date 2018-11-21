using Wappa.Domain.Interfaces.Models;
using Wappa.Domain.Models;

namespace Wappa.Test.Wappa.Domain
{
    internal static class AddressFake
    {
        internal static IAddress GetAddress() =>
         new Address
         {
             Id = 1,
             DriverId = 1,
             PostalCode = "03937090",
             StreetName = "Avenida Ouro Verde de Minas",
             Number = "800",
             Neighborhood = "Jardim Imperador",
             City = "São Paulo",
             StateCode = "SP",
             Country = "Brasil",
             Longitude = 0,
             Latitude = 0
         };
    }
}