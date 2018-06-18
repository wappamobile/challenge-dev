using ApplicationCore.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class MotoristaMap : IEntityTypeConfiguration<Motorista>
    {
        public void Configure(EntityTypeBuilder<Motorista> builder)
        {
            builder.ToTable("Motorista");

            builder.Property(x => x.Nome)
                .IsRequired();

            builder.Property(x => x.Sobrenome)
                .IsRequired();

            builder.Property(x => x.CarroId)
                .IsRequired();

            builder.Property(x => x.EnderecoId)
                .IsRequired();
        }
    }
}
