using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wappa.Dominio.Entidade;

namespace Wappa.Infraestrutura.Mapeamento
{
    public class MotoristaMap : IEntityTypeConfiguration<Motorista>
    {
        public void Configure(EntityTypeBuilder<Motorista> builder)
        {
            builder.ToTable("TB_MOTORISTA");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasColumnName("ID");

            builder.Property(c => c.Nome)
                   .HasColumnName("NOME")
                   .HasMaxLength(55)
                   .IsUnicode(false);

            builder.Property(c => c.Sobrenome)
                   .HasColumnName("SOBRENOME")
                   .HasMaxLength(55)
                   .IsUnicode(false);
        }
    }
}