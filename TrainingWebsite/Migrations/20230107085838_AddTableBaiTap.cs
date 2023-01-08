using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingWebsite.Migrations
{
    public partial class AddTableBaiTap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaiTaps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoiDung = table.Column<string>(nullable: true),
                    CreateDate = table.Column<string>(nullable: true),
                    trainerID = table.Column<string>(nullable: true),
                    courseID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaiTaps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BaiTaps_Courses_courseID",
                        column: x => x.courseID,
                        principalTable: "Courses",
                        principalColumn: "courseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaiTaps_AspNetUsers_trainerID",
                        column: x => x.trainerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaiTaps_courseID",
                table: "BaiTaps",
                column: "courseID");

            migrationBuilder.CreateIndex(
                name: "IX_BaiTaps_trainerID",
                table: "BaiTaps",
                column: "trainerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaiTaps");
        }
    }
}
