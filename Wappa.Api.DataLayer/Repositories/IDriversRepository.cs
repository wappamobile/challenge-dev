using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wappa.Api.DomainModel;

namespace Wappa.Api.DataLayer.Repositories
{
	public interface IDriversRepository
	{
		void Add(Driver driver);

		Task<IList<Driver>> GetAll(String sortBy, int limit, int offset);
	}
}