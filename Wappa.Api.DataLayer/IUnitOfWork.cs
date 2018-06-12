using System.Threading.Tasks;
using Wappa.Api.DataLayer.Repositories;

namespace Wappa.Api.DataLayer
{
	public interface IUnitOfWork
	{
		IAddressRepository AddressRepository { get; }

		ICarRepository CarRepository { get; }

		IDriversRepository DriversRepository { get; }

		Task<int> SaveChanges();
	}
}