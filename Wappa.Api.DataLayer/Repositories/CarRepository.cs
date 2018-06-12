using System;
using System.Collections.Generic;
using System.Text;
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
			await Task.Factory.StartNew(() => this.context.Cars.UpdateRange(cars));
		}
	}
}
