using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wappa.Framework.Data.Migrations
{
    public partial class AddEnderecoPais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PessoaId",
                table: "Enderecos");

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "Enderecos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "Enderecos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Enderecos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Enderecos");

            migrationBuilder.AlterColumn<string>(
                name: "Longitude",
                table: "Enderecos",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "Latitude",
                table: "Enderecos",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<long>(
                name: "PessoaId",
                table: "Enderecos",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
