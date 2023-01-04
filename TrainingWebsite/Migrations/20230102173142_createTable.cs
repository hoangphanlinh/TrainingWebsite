using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingWebsite.Migrations
{
    public partial class createTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseClassrooms",
                columns: table => new
                {
                    courseID = table.Column<int>(nullable: false),
                    classID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseClassrooms", x => new { x.courseID, x.classID });
                    table.ForeignKey(
                        name: "FK_CourseClassrooms_Classrooms_classID",
                        column: x => x.classID,
                        principalTable: "Classrooms",
                        principalColumn: "classID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseClassrooms_Courses_courseID",
                        column: x => x.courseID,
                        principalTable: "Courses",
                        principalColumn: "courseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseClassrooms_classID",
                table: "CourseClassrooms",
                column: "classID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseClassrooms");
        }
    }
}
