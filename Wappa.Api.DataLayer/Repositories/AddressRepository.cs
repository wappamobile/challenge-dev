using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			await Task.FromResult(this.context.Addresses.Update(address));
		}
	}
}
