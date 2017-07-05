using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Wappa.Framework.Data;

namespace Wappa.Framework.Data.Migrations
{
    [DbContext(typeof(DriverContext))]
    partial class DriverContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Wappa.Framework.Model.Comum.Endereco", b =>
                {
                    b.Property<long>("EnderecoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CEP");

                    b.Property<string>("Cidade");

                    b.Property<string>("Complemento");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<short>("Numero");

                    b.Property<string>("Pais");

                    b.Property<string>("Rua");

                    b.Property<string>("UF");

                    b.HasKey("EnderecoId");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("Wappa.Framework.Model.Pessoa.Motorista", b =>
                {
                    b.Property<long>("PessoaId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("EnderecoId");

                    b.Property<string>("Nome");

                    b.Property<string>("Sobrenome");

                    b.Property<long>("VeiculoId");

                    b.HasKey("PessoaId");

                    b.HasIndex("EnderecoId");

                    b.HasIndex("VeiculoId");

                    b.ToTable("Motoristas");
                });

            modelBuilder.Entity("Wappa.Framework.Model.Veiculo.Carro", b =>
                {
                    b.Property<long>("VeiculoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Marca");

                    b.Property<string>("Modelo");

                    b.Property<string>("Placa");

                    b.HasKey("VeiculoId");

                    b.ToTable("Carros");
                });

            modelBuilder.Entity("Wappa.Framework.Model.Pessoa.Motorista", b =>
                {
                    b.HasOne("Wappa.Framework.Model.Comum.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId");

                    b.HasOne("Wappa.Framework.Model.Veiculo.Carro", "Carro")
                        .WithMany()
                        .HasForeignKey("VeiculoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
