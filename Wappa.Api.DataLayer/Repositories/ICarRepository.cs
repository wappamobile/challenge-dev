using System.Collections.Generic;
using System.Threading.Tasks;
using Wappa.Api.DomainModel;

namespace Wappa.Api.DataLayer.Repositories
{
	public interface ICarRepository
	{
		Task Update(Driver driver, List<Car> address);
	}
}