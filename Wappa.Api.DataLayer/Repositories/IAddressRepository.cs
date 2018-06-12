using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wappa.Api.DomainModel;

namespace Wappa.Api.DataLayer.Repositories
{
	public interface IAddressRepository
	{
		Task Update(Address address);
	}
}
