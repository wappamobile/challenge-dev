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

		public async Task Update(Address address)
		{
			await Task.Factory.StartNew(() =>
			{
				var entity = this.context.Addresses.Find(address.Id);
				address.DriverId = entity.DriverId;
				this.context.Entry(entity).CurrentValues.SetValues(address);
			});
		}
	}
}
