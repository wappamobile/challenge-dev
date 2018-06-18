using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Infra.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carro",
                columns: table => new
                {
                    CarroId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<bool>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    Marca = table.Column<string>(nullable: false),
                    Modelo = table.Column<string>(nullable: false),
                    Placa = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carro", x => x.CarroId);
                });

            migrationBuilder.CreateTable(
                name: "GeoLocation",
                columns: table => new
                {
                    GeoLocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoLocation", x => x.GeoLocationId);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    EnderecoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<bool>(nullable: false),
                    CEP = table.Column<string>(nullable: false),
                    Cidade = table.Column<string>(nullable: false),
                    Complemento = table.Column<string>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    GeoLocationId = table.Column<int>(nullable: true),
                    Logradouro = table.Column<string>(nullable: false),
                    Numero = table.Column<int>(nullable: false),
                    UF = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.EnderecoId);
                    table.ForeignKey(
                        name: "FK_Endereco_GeoLocation_GeoLocationId",
                        column: x => x.GeoLocationId,
                        principalTable: "GeoLocation",
                        principalColumn: "GeoLocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Motorista",
                columns: table => new
                {
                    MotoristaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<bool>(nullable: false),
                    CarroId = table.Column<int>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    EnderecoId = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Sobrenome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorista", x => x.MotoristaId);
                    table.ForeignKey(
                        name: "FK_Motorista_Carro_CarroId",
                        column: x => x.CarroId,
                        principalTable: "Carro",
                        principalColumn: "CarroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Motorista_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "EnderecoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_GeoLocationId",
                table: "Endereco",
                column: "GeoLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Motorista_CarroId",
                table: "Motorista",
                column: "CarroId");

            migrationBuilder.CreateIndex(
                name: "IX_Motorista_EnderecoId",
                table: "Motorista",
                column: "EnderecoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Motorista");

            migrationBuilder.DropTable(
                name: "Carro");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "GeoLocation");
        }
    }
}
