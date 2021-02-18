using Microsoft.EntityFrameworkCore.Migrations;

namespace GenshinFarm.Api.Migrations
{
    public partial class Teams_AscensionCategory_CharacterWeapon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterUser");

            migrationBuilder.DropTable(
                name: "UserElement");

            migrationBuilder.DropTable(
                name: "UserWeapon");

            migrationBuilder.DropColumn(
                name: "PowerLvl",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "PowerLvl",
                table: "Characters");

            migrationBuilder.AddColumn<string>(
                name: "WeaponId",
                table: "Materials",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AscensionCategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AscensionCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterWeapons",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Lvl = table.Column<int>(type: "integer", nullable: false),
                    PowerLvl = table.Column<int>(type: "integer", nullable: false),
                    WeaponPowerLvl = table.Column<int>(type: "integer", nullable: false),
                    WeaponId = table.Column<string>(type: "text", nullable: true),
                    CharacterId = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterWeapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterWeapons_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CharacterWeapons_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CharacterWeapons_Weapons_WeaponId",
                        column: x => x.WeaponId,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    CharacterId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AscensionCategoryMaterial",
                columns: table => new
                {
                    AscensionCategoriesId = table.Column<string>(type: "text", nullable: false),
                    MaterialsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AscensionCategoryMaterial", x => new { x.AscensionCategoriesId, x.MaterialsId });
                    table.ForeignKey(
                        name: "FK_AscensionCategoryMaterial_AscensionCategories_AscensionCate~",
                        column: x => x.AscensionCategoriesId,
                        principalTable: "AscensionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AscensionCategoryMaterial_Materials_MaterialsId",
                        column: x => x.MaterialsId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterWeaponTeam",
                columns: table => new
                {
                    CharacterWeaponsId = table.Column<string>(type: "text", nullable: false),
                    TeamsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterWeaponTeam", x => new { x.CharacterWeaponsId, x.TeamsId });
                    table.ForeignKey(
                        name: "FK_CharacterWeaponTeam_CharacterWeapons_CharacterWeaponsId",
                        column: x => x.CharacterWeaponsId,
                        principalTable: "CharacterWeapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterWeaponTeam_Teams_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_WeaponId",
                table: "Materials",
                column: "WeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_AscensionCategoryMaterial_MaterialsId",
                table: "AscensionCategoryMaterial",
                column: "MaterialsId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterWeapons_CharacterId",
                table: "CharacterWeapons",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterWeapons_UserId",
                table: "CharacterWeapons",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterWeapons_WeaponId",
                table: "CharacterWeapons",
                column: "WeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterWeaponTeam_TeamsId",
                table: "CharacterWeaponTeam",
                column: "TeamsId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CharacterId",
                table: "Teams",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_UserId",
                table: "Teams",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Weapons_WeaponId",
                table: "Materials",
                column: "WeaponId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Weapons_WeaponId",
                table: "Materials");

            migrationBuilder.DropTable(
                name: "AscensionCategoryMaterial");

            migrationBuilder.DropTable(
                name: "CharacterWeaponTeam");

            migrationBuilder.DropTable(
                name: "AscensionCategories");

            migrationBuilder.DropTable(
                name: "CharacterWeapons");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Materials_WeaponId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "WeaponId",
                table: "Materials");

            migrationBuilder.AddColumn<int>(
                name: "PowerLvl",
                table: "Weapons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PowerLvl",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CharacterUser",
                columns: table => new
                {
                    CharactersId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterUser", x => new { x.CharactersId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CharacterUser_Characters_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserElement",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ElementId = table.Column<string>(type: "text", nullable: true),
                    Lvl = table.Column<int>(type: "integer", nullable: false),
                    PowerLvl = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "UserWeapon",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    WeaponsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWeapon", x => new { x.UserId, x.WeaponsId });
                    table.ForeignKey(
                        name: "FK_UserWeapon_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWeapon_Weapons_WeaponsId",
                        column: x => x.WeaponsId,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterUser_UserId",
                table: "CharacterUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserElement_UserId",
                table: "UserElement",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWeapon_WeaponsId",
                table: "UserWeapon",
                column: "WeaponsId");
        }
    }
}
