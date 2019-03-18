using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wappa.Driver.Api.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Driver",
                schema: "dbo",
                columns: table => new
                {
                    DriverID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "varchar(15)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.DriverID);
                });

            migrationBuilder.CreateTable(
                name: "DriverAddress",
                schema: "dbo",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "varchar(40)", nullable: false),
                    Number = table.Column<string>(type: "varchar(10)", nullable: false),
                    Neighborhood = table.Column<string>(type: "varchar(40)", nullable: false),
                    City = table.Column<string>(type: "varchar(40)", nullable: false),
                    State = table.Column<string>(type: "varchar(20)", nullable: false),
                    Country = table.Column<string>(type: "varchar(20)", nullable: false),
                    Latitude = table.Column<string>(type: "varchar(20)", nullable: false),
                    Longitude = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverAddress", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_DriverAddress_Driver_DriverId",
                        column: x => x.DriverId,
                        principalSchema: "dbo",
                        principalTable: "Driver",
                        principalColumn: "DriverID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverCar",
                schema: "dbo",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    Make = table.Column<string>(type: "varchar(15)", nullable: false),
                    Model = table.Column<string>(type: "varchar(15)", nullable: false),
                    Plate = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverCar", x => x.CarId);
                    table.ForeignKey(
                        name: "FK_DriverCar_Driver_DriverId",
                        column: x => x.DriverId,
                        principalSchema: "dbo",
                        principalTable: "Driver",
                        principalColumn: "DriverID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverAddress_DriverId",
                schema: "dbo",
                table: "DriverAddress",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverCar_DriverId",
                schema: "dbo",
                table: "DriverCar",
                column: "DriverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverAddress",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DriverCar",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Driver",
                schema: "dbo");
        }
    }
}
