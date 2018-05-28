using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wappa.Dominio.Entidade;

namespace Wappa.Infraestrutura.Mapeamento
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("TB_ENDERECO");

            builder.HasIndex(e => e.IdMotorista)
                   .HasName("FK_ENDERECO_MOTORISTA");

            builder.Property(c => c.IdMotorista)
                   .HasColumnName("ID_MOTORISTA");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasColumnName("ID");

            builder.Property(c => c.Cep)
                .HasColumnName("CEP")
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(c => c.Complemento)
                .HasColumnName("COMPLEMENTO")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(c => c.Logradouro)
                .HasColumnName("LOGRADOURO")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(c => c.Numero)
                .HasColumnName("NUMERO")
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(c => c.Longitude)
                .HasColumnName("LONGITUDE")
                .HasMaxLength(25)
                .IsUnicode(false);

            builder.Property(c => c.Latitude)
                .HasColumnName("LATITUDE")
                .HasMaxLength(25)
                .IsUnicode(false);

            builder.HasOne(d => d.Motorista)
                .WithMany(p => p.ListaEndereco)
                .HasForeignKey(d => d.IdMotorista)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}