using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Wappa.Api.DataLayer.TypeConfigurations;
using Wappa.Api.DomainModel;

namespace Wappa.Api.DataLayer
{
	public class BackOfficeContext : DbContext
	{
		public BackOfficeContext(DbContextOptions options) : base(options) { }

		public virtual DbSet<Address> Addresses { get; set; }

		public virtual DbSet<Car> Cars { get; set; }

		public virtual DbSet<Driver> Drivers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new AddressTypeConfiguration());
			modelBuilder.ApplyConfiguration(new CarTypeConfiguration());
			modelBuilder.ApplyConfiguration(new DriverTypeConfiguration());
		}
	}
}
