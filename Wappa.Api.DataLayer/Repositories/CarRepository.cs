using System.Collections.Generic;
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

		public async Task Update(List<Car> cars)
		{
			await Task.Factory.StartNew(() =>
			{
				foreach (var car in cars)
				{
					var entity = this.context.Cars.Find(car.Id);
					car.DriverId = entity.DriverId;
					this.context.Entry(entity).CurrentValues.SetValues(car);
				}
			});
		}
	}
}
