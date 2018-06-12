using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Api.DomainModel;

namespace Wappa.Api.DataLayer.Repositories
{
	public class AddressRepository : IAddressRepository
	{
		private BackOfficeContext context;

		public AddressRepository(BackOfficeContext context)
		{
			this.context = context;
		}

		public async Task Update(int driverId, Address address)
		{
			await Task.Factory.StartNew(() =>
			{
				var entity = this.context.Addresses.Include(d => d.Driver)
				.FirstOrDefault(d => d.Id == driverId);

				address.DriverId = entity.DriverId;
				address.Id = entity.Id;

				this.context.Entry(entity).CurrentValues.SetValues(address);
			});
		}
	}
}
