using System.Threading.Tasks;
using Wappa.Api.DataLayer.Repositories;

namespace Wappa.Api.DataLayer
{
	public class UnitOfWork : IUnitOfWork
	{
		private BackOfficeContext context;

		public UnitOfWork(BackOfficeContext context)
		{
			this.context = context;
		}

		private IAddressRepository addressRepository;
		public IAddressRepository AddressRepository
		{
			get
			{
				if (this.addressRepository == null) { this.addressRepository = new AddressRepository(this.context); }
				return this.addressRepository;
			}
		}

		private ICarRepository carRepository;
		public ICarRepository CarRepository
		{
			get
			{
				if (this.carRepository == null) { this.carRepository = new CarRepository(this.context); }
				return this.carRepository;
			}
		}

		private IDriversRepository driversRepository;
		public IDriversRepository DriversRepository
		{
			get
			{
				if (this.driversRepository == null) { this.driversRepository = new DriversRepository(this.context); }
				return this.driversRepository;
			}
		}

		public void SaveChanges() => this.context.SaveChanges();
	}
}
