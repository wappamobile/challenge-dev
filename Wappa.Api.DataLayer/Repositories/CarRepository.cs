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

		public async Task Update(int driverId, List<Car> cars)
		{
			await Task.Factory.StartNew(() =>
			{
				foreach (var car in cars)
				{
					var entity = this.context.Cars.Include(d => d.Driver)
									.FirstOrDefault(d => d.Id == driverId);

					car.DriverId = entity.DriverId;
					car.Id = entity.Id;

					this.context.Entry(entity).CurrentValues.SetValues(car);
				}
			});
		}
	}
}
