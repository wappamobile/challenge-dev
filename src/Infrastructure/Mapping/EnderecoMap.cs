using ApplicationCore.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Endereco");

            builder.Property(x => x.Logradouro)
                .IsRequired();

            builder.Property(x => x.Numero)
                .IsRequired();

            builder.Property(x => x.Complemento)
                .IsRequired();

            builder.Property(x => x.CEP)
               .IsRequired();

            builder.Property(x => x.Cidade)
               .IsRequired();

            builder.Property(x => x.UF)
               .HasMaxLength(2)
               .IsRequired();

        }
    }
}
