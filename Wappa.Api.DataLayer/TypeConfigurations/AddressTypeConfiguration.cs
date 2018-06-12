using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wappa.Api.DomainModel;

namespace Wappa.Api.DataLayer.TypeConfigurations
{
	class AddressTypeConfiguration : IEntityTypeConfiguration<Address>
	{
		public void Configure(EntityTypeBuilder<Address> builder)
		{
			builder.ToTable("Addresses");

			builder.HasKey(a => a.Id);

			builder.HasOne(a => a.Driver).WithOne(d => d.Address).HasForeignKey<Address>(fk => fk.DriverId);

			builder.Property(a => a.AddressLine).IsRequired();
			builder.Property(a => a.City).IsRequired();
			builder.Property(a => a.Latitude).IsRequired();
			builder.Property(a => a.Longitude).IsRequired();
			builder.Property(a => a.PostalCode).IsRequired();
			builder.Property(a => a.State).IsRequired();
		}
	}
}
