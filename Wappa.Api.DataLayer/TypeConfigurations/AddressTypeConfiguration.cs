using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Wappa.Api.DomainModel;

namespace Wappa.Api.DataLayer.TypeConfigurations
{
	class AddressTypeConfiguration : IEntityTypeConfiguration<Address>
	{
		public void Configure(EntityTypeBuilder<Address> builder)
		{
			builder.ToTable("Addresses");

			builder.HasKey(a => a.Id);

			builder.Property(a => a.AddressLine);
			builder.Property(a => a.City);
			builder.Property(a => a.Latitude);
			builder.Property(a => a.Longitude);
			builder.Property(a => a.State);
		}
	}
}
