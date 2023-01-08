using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingWebsite.Migrations
{
    public partial class UpdateTableBaiTap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaiTaps_AspNetUsers_trainerID",
                table: "BaiTaps");

            migrationBuilder.DropIndex(
                name: "IX_BaiTaps_trainerID",
                table: "BaiTaps");

            migrationBuilder.DropColumn(
                name: "trainerID",
                table: "BaiTaps");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "trainerID",
                table: "BaiTaps",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaiTaps_trainerID",
                table: "BaiTaps",
                column: "trainerID");

            migrationBuilder.AddForeignKey(
                name: "FK_BaiTaps_AspNetUsers_trainerID",
                table: "BaiTaps",
                column: "trainerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
