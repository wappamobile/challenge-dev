using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DriverLib.Infra.Data.Migrations
{
    public partial class car : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    Brand = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Model = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    LicensePlate = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CarId",
                table: "Users",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cars_CarId",
                table: "Users",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cars_CarId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Users_CarId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");
        }
    }
}
