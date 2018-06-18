using ApplicationCore.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class GeoLocationMap : IEntityTypeConfiguration<GeoLocation>
    {
        public void Configure(EntityTypeBuilder<GeoLocation> builder)
        {
            builder.ToTable("GeoLocation");

            builder.Property(x => x.Latitude)
                .IsRequired();

            builder.Property(x => x.Longitude)
                .IsRequired();

        }
    }
}
