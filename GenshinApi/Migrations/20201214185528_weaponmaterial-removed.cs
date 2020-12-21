using Microsoft.EntityFrameworkCore.Migrations;

namespace GenshinFarm.Api.Migrations
{
    public partial class weaponmaterialremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DaysOfWeeks_Materials_WeaponAscensionMaterialId",
                table: "DaysOfWeeks");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_DaysOfWeeks_DaysOfWeekId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_DaysOfWeekId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_DaysOfWeeks_WeaponAscensionMaterialId",
                table: "DaysOfWeeks");

            migrationBuilder.DropColumn(
                name: "DaysOfWeekId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "WeaponAscensionMaterialId",
                table: "DaysOfWeeks");

            migrationBuilder.CreateTable(
                name: "DaysOfWeekMaterial",
                columns: table => new
                {
                    DaysAvailableId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaterialsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysOfWeekMaterial", x => new { x.DaysAvailableId, x.MaterialsId });
                    table.ForeignKey(
                        name: "FK_DaysOfWeekMaterial_DaysOfWeeks_DaysAvailableId",
                        column: x => x.DaysAvailableId,
                        principalTable: "DaysOfWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DaysOfWeekMaterial_Materials_MaterialsId",
                        column: x => x.MaterialsId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DaysOfWeekMaterial_MaterialsId",
                table: "DaysOfWeekMaterial",
                column: "MaterialsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DaysOfWeekMaterial");

            migrationBuilder.AddColumn<string>(
                name: "DaysOfWeekId",
                table: "Materials",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeaponAscensionMaterialId",
                table: "DaysOfWeeks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Materials_DaysOfWeekId",
                table: "Materials",
                column: "DaysOfWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_DaysOfWeeks_WeaponAscensionMaterialId",
                table: "DaysOfWeeks",
                column: "WeaponAscensionMaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_DaysOfWeeks_Materials_WeaponAscensionMaterialId",
                table: "DaysOfWeeks",
                column: "WeaponAscensionMaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_DaysOfWeeks_DaysOfWeekId",
                table: "Materials",
                column: "DaysOfWeekId",
                principalTable: "DaysOfWeeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
