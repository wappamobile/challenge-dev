using System.Collections.Generic;

namespace DriverRegistration.Domain.Interfaces
{
    public interface IServiceDriver
    {
        IDriver Add(IDriver driver);

        bool Update(IDriver driver);

        bool Delete(int id);

        IDriver Load(int id);

        IEnumerable<IDriver> GetOrderByFirstName(int rowindex, int rowget);

        IEnumerable<IDriver> GetOrderByLasttName(int rowindex, int rowget);
    }
}
