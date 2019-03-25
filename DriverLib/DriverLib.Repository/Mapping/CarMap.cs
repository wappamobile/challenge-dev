using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DriverLib.Domain;

namespace DriverLib.Repository.Mapping
{
    public class CarMap
    {
        public CarMap(EntityTypeBuilder<Car> entityBuilder)
        {
            entityBuilder.Property(t => t.Brand)
                   .HasColumnType("varchar(200)")
                   .HasMaxLength(200);

            entityBuilder.Property(t => t.Model)
                  .HasColumnType("varchar(200)")
                  .HasMaxLength(200);

            entityBuilder.Property(t => t.LicensePlate)
                  .HasColumnType("varchar(200)")
                  .HasMaxLength(200);

        }
    }
}
