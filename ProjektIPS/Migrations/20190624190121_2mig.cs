using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektIPS.Migrations
{
    public partial class _2mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Emotion",
                table: "Faces",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Emotion",
                table: "Faces");
        }
    }
}
