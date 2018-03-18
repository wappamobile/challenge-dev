using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Wappa.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Taxistas",
                columns: table => new
                {
                    IdTaxista = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Endereco = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false),
                    Latitude = table.Column<string>(type: "VARCHAR(25)", nullable: true),
                    Longitude = table.Column<string>(type: "VARCHAR(25)", nullable: true),
                    Marca = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Modelo = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Placa = table.Column<string>(type: "VARCHAR(8)", maxLength: 8, nullable: false),
                    PrimeiroNome = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    UltimoNome = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxistas", x => x.IdTaxista);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Taxistas");
        }
    }
}
