using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Api.DomainModel;

namespace Wappa.Api.DataLayer.Repositories
{
	public class CarRepository : ICarRepository
	{
		private BackOfficeContext context;

		public CarRepository(BackOfficeContext context)
		{
			this.context = context;
		}

		public async Task Update(Driver driver, List<Car> cars)
		{
			await Task.Factory.StartNew(() =>
			{
				this.DeleteOldCars(driver.Cars);

				foreach (var car in cars)
				{
					car.DriverId = driver.Id;
					this.context.Cars.Add(car);
				}
			});
		}

		private void DeleteOldCars(ICollection<Car> cars)
		{
			foreach (var car in cars)
			{
				this.context.Cars.Remove(car);
			}
		}
	}
}
