using Wappa.Middleware.Core.GoogleMaps;
using Wappa.Middleware.Domain.Drivers;
using Wappa.Middleware.EntityFrameworkCore.Repositories.Drivers;
using Wappa.Middleware.EntityFrameworkCore.UoW;
using System.Linq;
using System.Threading.Tasks;

namespace Wappa.Middleware.Core.Drivers
{
    public class DriverManager : WappaMiddlewareServiceBase, IDriverManager
    {
        private readonly IGoogleMapManager _googleMapManager;

        private IDriverRepository _driverRepository { get; }

        public DriverManager(IDriverRepository driverRepository, IUnitOfWork uow, IGoogleMapManager googleMapManager)
            : base(uow)
        {
            _driverRepository = driverRepository;
            _googleMapManager = googleMapManager;
        }

        public IQueryable<Driver> Drivers
        {
            get
            {
                return _driverRepository.List().AsQueryable();
            }
        }

        public async Task<int?> CreateAsync(Driver driver)
        {
            var maps = await _googleMapManager.GeocodeAdress(driver);

            driver.Longitude = maps.results[0].geometry.location.lng;
            driver.Latitude = maps.results[0].geometry.location.lat;

            await _driverRepository.AddAsync(driver);

            if (_uow.Commit())
            {
                await _uow.SaveChangesAsync();
                return (await _driverRepository.FirstOrDefaultAsync(t => t.FirtName == driver.FirtName && t.LastName == driver.LastName)).Id;
            }

            return null;
        }

        public async Task DeleteAsync(Driver driver)
        {
            await _driverRepository.DeleteAsync(driver.Id);
            if (_uow.Commit())
            {
                await _uow.SaveChangesAsync();
            }
        }

        public async Task<Driver> GetByIdAsync(int id)
        {
            return await _driverRepository.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateAsync(Driver driver)
        {
            var maps = await _googleMapManager.GeocodeAdress(driver);

            driver.Longitude = maps.results[0].geometry.location.lng;
            driver.Latitude = maps.results[0].geometry.location.lat;

            await _driverRepository.UpdateAsync(driver.Id, driver);

            if (_uow.Commit())
            {
                await _uow.SaveChangesAsync();
            }
        }
    }
}
