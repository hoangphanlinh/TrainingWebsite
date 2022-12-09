using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingWebsite.Migrations
{
    public partial class DeleteTableTrainer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trainers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    MaTrainer = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDTaiKhoan = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.MaTrainer);
                    table.ForeignKey(
                        name: "FK_Trainers_AspNetUsers_IDTaiKhoan",
                        column: x => x.IDTaiKhoan,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_IDTaiKhoan",
                table: "Trainers",
                column: "IDTaiKhoan");
        }
    }
}
