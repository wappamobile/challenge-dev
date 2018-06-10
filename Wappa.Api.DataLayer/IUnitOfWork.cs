using System.Threading.Tasks;
using Wappa.Api.DataLayer.Repositories;

namespace Wappa.Api.DataLayer
{
	public interface IUnitOfWork
	{
		IDriversRepository DriversRepository { get; }

		Task<int> SaveChanges();
	}
}