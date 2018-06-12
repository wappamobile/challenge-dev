using System.Threading.Tasks;
using Wappa.Api.DomainModel;

namespace Wappa.Api.DataLayer.Repositories
{
	public interface IAddressRepository
	{
		Task Update(int driverId, Address address);
	}
}
