using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingWebsite.Migrations
{
    public partial class AddTableTaiLieu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaiLieus",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    videoLink = table.Column<string>(nullable: true),
                    filelink = table.Column<string>(nullable: true),
                    ChuDeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiLieus", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TaiLieus_Topic_ChuDeID",
                        column: x => x.ChuDeID,
                        principalTable: "Topic",
                        principalColumn: "IDChuDe",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaiLieus_ChuDeID",
                table: "TaiLieus",
                column: "ChuDeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaiLieus");
        }
    }
}
