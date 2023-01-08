using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingWebsite.Migrations
{
    public partial class UpdateTableTaiLieu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "filelink",
                table: "TaiLieus");

            migrationBuilder.DropColumn(
                name: "videoLink",
                table: "TaiLieus");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TaiLieus",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "TaiLieus",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "TaiLieus");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "TaiLieus");

            migrationBuilder.AddColumn<string>(
                name: "filelink",
                table: "TaiLieus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "videoLink",
                table: "TaiLieus",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
