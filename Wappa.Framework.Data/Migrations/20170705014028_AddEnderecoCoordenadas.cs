using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wappa.Framework.Data.Migrations
{
    public partial class AddEnderecoCoordenadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Enderecos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Enderecos",
                nullable: true);
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
