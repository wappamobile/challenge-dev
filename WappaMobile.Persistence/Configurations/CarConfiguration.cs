using System;
using Microsoft.EntityFrameworkCore;
using WappaMobile.Domain;

namespace WappaMobile.Persistence.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Car> builder)
        {
            builder.Property<Guid>("CarId");
            builder.HasIndex("CarId");
        }
    }
}
