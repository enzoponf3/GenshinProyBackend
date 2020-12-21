using Microsoft.EntityFrameworkCore.Migrations;

namespace GenshinFarm.Api.Migrations
{
    public partial class addeduserElementtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PowerLvl",
                table: "Materials");

            migrationBuilder.AddColumn<string>(
                name: "Rarity",
                table: "Materials",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UserElement",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Lvl = table.Column<int>(type: "int", nullable: false),
                    PowerLvl = table.Column<int>(type: "int", nullable: false),
                    ElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserElement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserElement_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserElement_UserId",
                table: "UserElement",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserElement");

            migrationBuilder.DropColumn(
                name: "Rarity",
                table: "Materials");

            migrationBuilder.AddColumn<int>(
                name: "PowerLvl",
                table: "Materials",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
