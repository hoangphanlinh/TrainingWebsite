using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingWebsite.Migrations
{
    public partial class AddTableResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    TraineeID = table.Column<string>(nullable: false),
                    ExamID = table.Column<int>(nullable: false),
                    ResultQuiz = table.Column<string>(nullable: true),
                    ResultEssay = table.Column<string>(nullable: true),
                    SatrtDate = table.Column<string>(nullable: true),
                    StartTime = table.Column<string>(nullable: true),
                    FinishDate = table.Column<string>(nullable: true),
                    FinishTime = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Score = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => new { x.ExamID, x.TraineeID });
                    table.ForeignKey(
                        name: "FK_Results_Exams_ExamID",
                        column: x => x.ExamID,
                        principalTable: "Exams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_AspNetUsers_TraineeID",
                        column: x => x.TraineeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Results_TraineeID",
                table: "Results",
                column: "TraineeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");
        }
    }
}
