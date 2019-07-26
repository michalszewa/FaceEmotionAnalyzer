using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektIPS.Migrations
{
    public partial class updatedscheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faces_Images_ImageId",
                table: "Faces");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Faces",
                newName: "PhotoId");

            migrationBuilder.RenameIndex(
                name: "IX_Faces_ImageId",
                table: "Faces",
                newName: "IX_Faces_PhotoId");

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(nullable: true),
                    Anger = table.Column<int>(nullable: false),
                    Contempt = table.Column<int>(nullable: false),
                    Disgust = table.Column<int>(nullable: false),
                    Fear = table.Column<int>(nullable: false),
                    Happiness = table.Column<int>(nullable: false),
                    Neutral = table.Column<int>(nullable: false),
                    Sadness = table.Column<int>(nullable: false),
                    Surprise = table.Column<int>(nullable: false),
                    PublicationTime = table.Column<DateTime>(nullable: false),
                    Publicate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Faces_Photos_PhotoId",
                table: "Faces",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faces_Photos_PhotoId",
                table: "Faces");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.RenameColumn(
                name: "PhotoId",
                table: "Faces",
                newName: "ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Faces_PhotoId",
                table: "Faces",
                newName: "IX_Faces_ImageId");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Anger = table.Column<int>(nullable: false),
                    Contempt = table.Column<int>(nullable: false),
                    Disgust = table.Column<int>(nullable: false),
                    Fear = table.Column<int>(nullable: false),
                    Happiness = table.Column<int>(nullable: false),
                    Neutral = table.Column<int>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    Publicate = table.Column<bool>(nullable: false),
                    PublicationTime = table.Column<DateTime>(nullable: false),
                    Sadness = table.Column<int>(nullable: false),
                    Surprise = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Faces_Images_ImageId",
                table: "Faces",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
