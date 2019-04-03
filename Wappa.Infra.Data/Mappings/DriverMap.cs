using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wappa.Domain.Models;

namespace Wappa.Infra.Data.Mappings
{
    public class DriverMap : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(c => c.LastName)
                .HasColumnName("LastName")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(c => c.Address)
                .HasColumnName("Address")
                .HasColumnType("varchar(1000)")
                .IsRequired();

            builder.Property(c => c.Zipcode)
                .HasColumnName("ZipCode")
                .HasColumnType("varchar(9)")
                .IsRequired();

            builder.Property(c => c.CarBrand)
                .HasColumnName("CarBrand")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(c => c.CarModel)
                .HasColumnName("CarModel")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(c => c.CarPlate)
                .HasColumnName("CarPlate")
                .HasColumnType("varchar(7)")
                .IsRequired();

            builder.Property(c => c.Coordinates)
                .HasColumnName("Coordinates")
                .HasColumnType("varchar(500)")
                .IsRequired();

        }
    }
}
