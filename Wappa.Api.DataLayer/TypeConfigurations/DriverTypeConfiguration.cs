using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
			builder.HasMany(d => d.Cars).WithOne(c => c.Driver).HasForeignKey(fk => fk.DriverId);

			builder.Property(d => d.FirstName).IsRequired();
			builder.Property(d => d.LastName).IsRequired();
		}
	}
}
