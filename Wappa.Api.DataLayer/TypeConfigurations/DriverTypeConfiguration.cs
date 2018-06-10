using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Wappa.Api.DomainModel;

namespace Wappa.Api.DataLayer.TypeConfigurations
{
	class DriverTypeConfiguration : IEntityTypeConfiguration<Driver>
	{
		public void Configure(EntityTypeBuilder<Driver> builder)
		{
			builder.ToTable("Drivers");

			builder.HasKey(d => d.Id);

			builder.HasOne(d => d.Address).WithOne(a => a.Driver).HasForeignKey<Address>(fk => fk.DriverId);
			builder.HasMany(d => d.Car).WithOne(c => c.Driver);

			builder.Property(d => d.FirstName).IsRequired();
			builder.Property(d => d.LastName).IsRequired();
		}
	}
}
