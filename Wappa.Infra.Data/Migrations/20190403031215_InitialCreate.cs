using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wappa.Infra.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(100)", nullable: false),
                    CarModel = table.Column<string>(type: "varchar(100)", nullable: false),
                    CarBrand = table.Column<string>(type: "varchar(100)", nullable: false),
                    CarPlate = table.Column<string>(type: "varchar(7)", nullable: false),
                    ZipCode = table.Column<string>(type: "varchar(9)", nullable: false),
                    Address = table.Column<string>(type: "varchar(1000)", nullable: false),
                    Coordinates = table.Column<string>(type: "varchar(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drivers");
        }
    }
}
