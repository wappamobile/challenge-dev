using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Wappa.Infraestrutura.Migrations
{
    public partial class WappaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_MOTORISTA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(unicode: false, maxLength: 55, nullable: true),
                    SOBRENOME = table.Column<string>(unicode: false, maxLength: 55, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_MOTORISTA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_CARRO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    COR = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    ID_MOTORISTA = table.Column<int>(nullable: false),
                    LUGAR = table.Column<int>(unicode: false, maxLength: 2, nullable: true),
                    MALA = table.Column<int>(unicode: false, maxLength: 2, nullable: true),
                    MARCA = table.Column<string>(unicode: false, maxLength: 55, nullable: true),
                    MODELO = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    PLACA = table.Column<string>(unicode: false, maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CARRO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TB_CARRO_TB_MOTORISTA_ID_MOTORISTA",
                        column: x => x.ID_MOTORISTA,
                        principalTable: "TB_MOTORISTA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_ENDERECO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CEP = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    COMPLEMENTO = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    ID_MOTORISTA = table.Column<int>(nullable: false),
                    LATITUDE = table.Column<double>(unicode: false, maxLength: 25, nullable: true),
                    LOGRADOURO = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    LONGITUDE = table.Column<double>(unicode: false, maxLength: 25, nullable: true),
                    NUMERO = table.Column<int>(unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ENDERECO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TB_ENDERECO_TB_MOTORISTA_ID_MOTORISTA",
                        column: x => x.ID_MOTORISTA,
                        principalTable: "TB_MOTORISTA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "FK_CARRO_MOTORISTA",
                table: "TB_CARRO",
                column: "ID_MOTORISTA");

            migrationBuilder.CreateIndex(
                name: "FK_ENDERECO_MOTORISTA",
                table: "TB_ENDERECO",
                column: "ID_MOTORISTA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_CARRO");

            migrationBuilder.DropTable(
                name: "TB_ENDERECO");

            migrationBuilder.DropTable(
                name: "TB_MOTORISTA");
        }
    }
}
