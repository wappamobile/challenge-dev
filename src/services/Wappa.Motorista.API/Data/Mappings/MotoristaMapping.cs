using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wappa.Motoristas.API.Models;

namespace Wappa.Motoristas.API.Data.Mappings
{
    public class MotoristaMapping : IEntityTypeConfiguration<Motorista>
    {
        public void Configure(EntityTypeBuilder<Motorista> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.SobreNome)
                .IsRequired()
                .HasColumnType("varchar(200)");
            
            // 1 : 1 => Motorista : Endereco
            builder.HasOne(c => c.Endereco)
                .WithOne(c => c.Motorista);

            // 1 : 1 => Motorista : Carro
            builder.HasOne(c => c.Carro)
                .WithOne(c => c.Motorista);

            builder.ToTable("Motoristas");
        }
    }
}