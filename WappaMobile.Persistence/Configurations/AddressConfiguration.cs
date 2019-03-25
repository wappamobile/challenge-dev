using System;
using Microsoft.EntityFrameworkCore;
using WappaMobile.Domain;

namespace WappaMobile.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Address> builder)
        {
            builder.Property<Guid>("AddressId");
            builder.HasIndex("AddressId");
        }
    }
}
