using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingWebsite.Migrations
{
    public partial class delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreateCourseViewModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreateCourseViewModel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HinhThucDanhGia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDKhoaHocTienQuyet = table.Column<int>(type: "int", nullable: true),
                    IDTrainer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobPosID = table.Column<int>(type: "int", nullable: false),
                    MucTieuKhoaHoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiLuongKhoaHoc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateCourseViewModel", x => x.ID);
                });
        }
    }
}
