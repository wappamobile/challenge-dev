using System;
using System.Collections.Generic;
using System.Text;
using Wappa.Api.DomainModel;

namespace Wappa.Api.DataLayer.Repositories
{
    public class DriversRepository : IDriversRepository
	{
		private BackOfficeContext context;

		public DriversRepository(BackOfficeContext context)
		{
			this.context = context;
		}

		public void Add(Driver driver)
		{
			this.context.Drivers.Add(driver);
		}
    }
}
