using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektIPS.Migrations
{
    public partial class _3rdmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Publicate",
                table: "Images",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublicationTime",
                table: "Images",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Publicate",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "PublicationTime",
                table: "Images");
        }
    }
}
