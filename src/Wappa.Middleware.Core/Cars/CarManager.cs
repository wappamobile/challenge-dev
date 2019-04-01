using Wappa.Middleware.Domain.Cars;
using Wappa.Middleware.EntityFrameworkCore.Repositories.Cars;
using Wappa.Middleware.EntityFrameworkCore.UoW;
using System.Linq;
using System.Threading.Tasks;

namespace Wappa.Middleware.Core.Cars
{
    public class CarManager : WappaMiddlewareServiceBase, ICarManager
    {
        private readonly ICarRepository _carRepository;

        public CarManager(ICarRepository carRepository, IUnitOfWork uow)
            : base(uow)
        {
            _carRepository = carRepository;
        }

        public IQueryable<Car> Cars
        {
            get
            {
                return _carRepository.List().AsQueryable();
            }
        }

        public async Task<int?> CreateAsync(Car car)
        {
            await _carRepository.AddAsync(car);

            if (_uow.Commit())
            {
                await _uow.SaveChangesAsync();
                return (await _carRepository.FirstOrDefaultAsync(t => t.Plate == car.Plate)).Id;
            }
            return null;
        }

        public async Task DeleteAsync(Car car)
        {
            await _carRepository.DeleteAsync(car.Id);
            if (_uow.Commit())
            {
                await _uow.SaveChangesAsync();
            }
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await _carRepository.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateAsync(Car car)
        {
            await _carRepository.UpdateAsync(car.Id, car);
            if (_uow.Commit())
            {
                await _uow.SaveChangesAsync();
            }
        }
    }
}
