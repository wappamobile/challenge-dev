using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wappa.Motoristas.API.Models;

namespace Wappa.Motoristas.API.Data.Mappings
{
    public class CarroMapping : IEntityTypeConfiguration<Carro>
    {
        public void Configure(EntityTypeBuilder<Carro> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Marca)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Modelo)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Placa)
                .IsRequired()
                .HasColumnType("varchar(20)");

            
            builder.ToTable("Carros");
        }
    }
}