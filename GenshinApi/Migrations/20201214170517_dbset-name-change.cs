using Microsoft.EntityFrameworkCore.Migrations;

namespace GenshinFarm.Api.Migrations
{
    public partial class dbsetnamechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AscensionMaterials_Characters_CharacterId",
                table: "AscensionMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterMaterial_AscensionMaterials_TalentMaterialsId",
                table: "CharacterMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_DaysOfWeekWeaponAscensionMaterial_AscensionMaterials_WeaponAscensionMaterialId",
                table: "DaysOfWeekWeaponAscensionMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_FarmLocationMaterial_AscensionMaterials_MaterialsId",
                table: "FarmLocationMaterial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AscensionMaterials",
                table: "AscensionMaterials");

            migrationBuilder.RenameTable(
                name: "AscensionMaterials",
                newName: "Materials");

            migrationBuilder.RenameIndex(
                name: "IX_AscensionMaterials_CharacterId",
                table: "Materials",
                newName: "IX_Materials_CharacterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materials",
                table: "Materials",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterMaterial_Materials_TalentMaterialsId",
                table: "CharacterMaterial",
                column: "TalentMaterialsId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DaysOfWeekWeaponAscensionMaterial_Materials_WeaponAscensionMaterialId",
                table: "DaysOfWeekWeaponAscensionMaterial",
                column: "WeaponAscensionMaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FarmLocationMaterial_Materials_MaterialsId",
                table: "FarmLocationMaterial",
                column: "MaterialsId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Characters_CharacterId",
                table: "Materials",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterMaterial_Materials_TalentMaterialsId",
                table: "CharacterMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_DaysOfWeekWeaponAscensionMaterial_Materials_WeaponAscensionMaterialId",
                table: "DaysOfWeekWeaponAscensionMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_FarmLocationMaterial_Materials_MaterialsId",
                table: "FarmLocationMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Characters_CharacterId",
                table: "Materials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Materials",
                table: "Materials");

            migrationBuilder.RenameTable(
                name: "Materials",
                newName: "AscensionMaterials");

            migrationBuilder.RenameIndex(
                name: "IX_Materials_CharacterId",
                table: "AscensionMaterials",
                newName: "IX_AscensionMaterials_CharacterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AscensionMaterials",
                table: "AscensionMaterials",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AscensionMaterials_Characters_CharacterId",
                table: "AscensionMaterials",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterMaterial_AscensionMaterials_TalentMaterialsId",
                table: "CharacterMaterial",
                column: "TalentMaterialsId",
                principalTable: "AscensionMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DaysOfWeekWeaponAscensionMaterial_AscensionMaterials_WeaponAscensionMaterialId",
                table: "DaysOfWeekWeaponAscensionMaterial",
                column: "WeaponAscensionMaterialId",
                principalTable: "AscensionMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FarmLocationMaterial_AscensionMaterials_MaterialsId",
                table: "FarmLocationMaterial",
                column: "MaterialsId",
                principalTable: "AscensionMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
