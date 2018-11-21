using Wappa.Domain.Interfaces.Models;
using Wappa.Domain.Models;

namespace Wappa.Test.Wappa.Domain
{
    public static class DriverFake
    {
        internal static IDriver GetDriver() =>
             new Driver
             {
                 Id = 1,
                 Document = "12312312312",
                 FirstName = "Márcio",
                 LastName = "Adão",
             };
    }
}