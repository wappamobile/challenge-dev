using Microsoft.EntityFrameworkCore.Migrations;

namespace Wappa.Motoristas.API.Migrations
{
    public partial class Coordenadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Enderecos",
                type: "decimal(18,10)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Enderecos",
                type: "decimal(18,10)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Enderecos");
        }
    }
}
