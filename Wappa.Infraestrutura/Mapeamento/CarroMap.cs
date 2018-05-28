using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wappa.Dominio.Entidade;

namespace Wappa.Infraestrutura.Mapeamento
{
    public class CarroMap : IEntityTypeConfiguration<Carro>
    {
        public void Configure(EntityTypeBuilder<Carro> builder)
        {
            builder.ToTable("TB_CARRO");

            builder.HasKey(c => c.Id);

            builder.HasIndex(e => e.IdMotorista)
                   .HasName("FK_CARRO_MOTORISTA");

            builder.Property(c => c.IdMotorista)
                   .HasColumnName("ID_MOTORISTA");

            builder.Property(c => c.Id)
                   .HasColumnName("ID");

            builder.Property(c => c.Modelo)
                .HasColumnName("MODELO")
                .HasMaxLength(25)
                .IsUnicode(false);

            builder.Property(c => c.Cor)
                .HasColumnName("COR")
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(c => c.Placa)
                .HasColumnName("PLACA")
                .HasMaxLength(8)
                .IsUnicode(false);

            builder.Property(c => c.Lugar)
                .HasColumnName("LUGAR")
                .HasMaxLength(2)
                .IsUnicode(false);

            builder.Property(c => c.Mala)
                .HasColumnName("MALA")
                .HasMaxLength(2)
                .IsUnicode(false);

            builder.Property(c => c.Marca)
                .HasColumnName("MARCA")
                .HasMaxLength(55)
                .IsUnicode(false);

            builder.HasOne(d => d.Motorista)
                .WithMany(p => p.ListaCarro)
                .HasForeignKey(d => d.IdMotorista)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}