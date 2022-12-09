using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingWebsite.Migrations
{
    public partial class CreateTableCouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhoaHoc = table.Column<string>(nullable: true),
                    TenKhoaHoc = table.Column<string>(nullable: true),
                    ThoiLuongKhoaHoc = table.Column<int>(nullable: false),
                    MucTieuKhoaHoc = table.Column<string>(nullable: true),
                    HinhThucDanhGia = table.Column<string>(nullable: true),
                    IDKhoaHocTienQuyet = table.Column<int>(nullable: true),
                    IDTrainer = table.Column<string>(nullable: true),
                    IDJobPos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Courses_Occuptions_IDJobPos",
                        column: x => x.IDJobPos,
                        principalTable: "Occuptions",
                        principalColumn: "OccuptionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_Courses_IDKhoaHocTienQuyet",
                        column: x => x.IDKhoaHocTienQuyet,
                        principalTable: "Courses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_AspNetUsers_IDTrainer",
                        column: x => x.IDTrainer,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_IDJobPos",
                table: "Courses",
                column: "IDJobPos");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_IDKhoaHocTienQuyet",
                table: "Courses",
                column: "IDKhoaHocTienQuyet");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_IDTrainer",
                table: "Courses",
                column: "IDTrainer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
