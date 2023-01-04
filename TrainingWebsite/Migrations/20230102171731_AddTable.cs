using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingWebsite.Migrations
{
    public partial class AddTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Level_LevelID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Occuption_OccuptionID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Occuption_Apartment_ApartmentID",
                table: "Occuption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Occuption",
                table: "Occuption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Level",
                table: "Level");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Apartment",
                table: "Apartment");

            migrationBuilder.RenameTable(
                name: "Occuption",
                newName: "Occuptions");

            migrationBuilder.RenameTable(
                name: "Level",
                newName: "Levels");

            migrationBuilder.RenameTable(
                name: "Apartment",
                newName: "Apartments");

            migrationBuilder.RenameIndex(
                name: "IX_Occuption_ApartmentID",
                table: "Occuptions",
                newName: "IX_Occuptions_ApartmentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Occuptions",
                table: "Occuptions",
                column: "OccuptionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Levels",
                table: "Levels",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Apartments",
                table: "Apartments",
                column: "ApartmentID");

            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    classID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    startDate = table.Column<string>(nullable: true),
                    endDate = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    AdminID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.classID);
                    table.ForeignKey(
                        name: "FK_Classrooms_AspNetUsers_AdminID",
                        column: x => x.AdminID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    courseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhoaHoc = table.Column<string>(nullable: true),
                    TenKhoaHoc = table.Column<string>(nullable: true),
                    ThoiLuongKhoaHoc = table.Column<int>(nullable: false),
                    MucTieuKhoaHoc = table.Column<string>(nullable: true),
                    HinhThucDanhGia = table.Column<string>(nullable: true),
                    IDTrainer = table.Column<string>(nullable: true),
                    ImageTrainer = table.Column<string>(nullable: true),
                    IDJobPos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.courseID);
                    table.ForeignKey(
                        name: "FK_Courses_Occuptions_IDJobPos",
                        column: x => x.IDJobPos,
                        principalTable: "Occuptions",
                        principalColumn: "OccuptionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_AspNetUsers_IDTrainer",
                        column: x => x.IDTrainer,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    IDChuDe = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChuDe = table.Column<string>(nullable: true),
                    NoiDung = table.Column<string>(nullable: true),
                    IDKhoaHoc = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.IDChuDe);
                    table.ForeignKey(
                        name: "FK_Topic_Courses_IDKhoaHoc",
                        column: x => x.IDKhoaHoc,
                        principalTable: "Courses",
                        principalColumn: "courseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_AdminID",
                table: "Classrooms",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_IDJobPos",
                table: "Courses",
                column: "IDJobPos");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_IDTrainer",
                table: "Courses",
                column: "IDTrainer");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_IDKhoaHoc",
                table: "Topic",
                column: "IDKhoaHoc");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Levels_LevelID",
                table: "AspNetUsers",
                column: "LevelID",
                principalTable: "Levels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Occuptions_OccuptionID",
                table: "AspNetUsers",
                column: "OccuptionID",
                principalTable: "Occuptions",
                principalColumn: "OccuptionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Occuptions_Apartments_ApartmentID",
                table: "Occuptions",
                column: "ApartmentID",
                principalTable: "Apartments",
                principalColumn: "ApartmentID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Levels_LevelID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Occuptions_OccuptionID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Occuptions_Apartments_ApartmentID",
                table: "Occuptions");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Occuptions",
                table: "Occuptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Levels",
                table: "Levels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Apartments",
                table: "Apartments");

            migrationBuilder.RenameTable(
                name: "Occuptions",
                newName: "Occuption");

            migrationBuilder.RenameTable(
                name: "Levels",
                newName: "Level");

            migrationBuilder.RenameTable(
                name: "Apartments",
                newName: "Apartment");

            migrationBuilder.RenameIndex(
                name: "IX_Occuptions_ApartmentID",
                table: "Occuption",
                newName: "IX_Occuption_ApartmentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Occuption",
                table: "Occuption",
                column: "OccuptionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Level",
                table: "Level",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Apartment",
                table: "Apartment",
                column: "ApartmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Level_LevelID",
                table: "AspNetUsers",
                column: "LevelID",
                principalTable: "Level",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Occuption_OccuptionID",
                table: "AspNetUsers",
                column: "OccuptionID",
                principalTable: "Occuption",
                principalColumn: "OccuptionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Occuption_Apartment_ApartmentID",
                table: "Occuption",
                column: "ApartmentID",
                principalTable: "Apartment",
                principalColumn: "ApartmentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
