using ApplicationCore.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class CarroMap : IEntityTypeConfiguration<Carro>
    {
        public void Configure(EntityTypeBuilder<Carro> builder)
        {
            builder.ToTable("Carro");

            builder.Property(x => x.Marca)
                .IsRequired();

            builder.Property(x => x.Modelo)
                .IsRequired();

            builder.Property(x => x.Placa)
                .IsRequired();

        }
    }
}
