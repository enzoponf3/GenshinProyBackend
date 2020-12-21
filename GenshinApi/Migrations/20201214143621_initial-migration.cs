using Microsoft.EntityFrameworkCore.Migrations;

namespace GenshinFarm.Api.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PowerLvl = table.Column<int>(type: "int", nullable: false),
                    Rarity = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    WeaponType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DaysOfWeeks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysOfWeeks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FarmLocations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weekly = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rarity = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    PowerLvl = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AscensionMaterials",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PowerLvl = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CharacterId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AscensionMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AscensionMaterials_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Talents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    PowerLvl = table.Column<int>(type: "int", nullable: false),
                    CharacterId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Talents_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CharacterUser",
                columns: table => new
                {
                    CharactersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                name: "UserWeapon",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WeaponsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "CharacterMaterial",
                columns: table => new
                {
                    CharactersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TalentMaterialsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterMaterial", x => new { x.CharactersId, x.TalentMaterialsId });
                    table.ForeignKey(
                        name: "FK_CharacterMaterial_AscensionMaterials_TalentMaterialsId",
                        column: x => x.TalentMaterialsId,
                        principalTable: "AscensionMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterMaterial_Characters_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DaysOfWeekWeaponAscensionMaterial",
                columns: table => new
                {
                    DaysAvailableId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WeaponAscensionMaterialId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysOfWeekWeaponAscensionMaterial", x => new { x.DaysAvailableId, x.WeaponAscensionMaterialId });
                    table.ForeignKey(
                        name: "FK_DaysOfWeekWeaponAscensionMaterial_AscensionMaterials_WeaponAscensionMaterialId",
                        column: x => x.WeaponAscensionMaterialId,
                        principalTable: "AscensionMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DaysOfWeekWeaponAscensionMaterial_DaysOfWeeks_DaysAvailableId",
                        column: x => x.DaysAvailableId,
                        principalTable: "DaysOfWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FarmLocationMaterial",
                columns: table => new
                {
                    FarmLocationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaterialsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmLocationMaterial", x => new { x.FarmLocationId, x.MaterialsId });
                    table.ForeignKey(
                        name: "FK_FarmLocationMaterial_AscensionMaterials_MaterialsId",
                        column: x => x.MaterialsId,
                        principalTable: "AscensionMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FarmLocationMaterial_FarmLocations_FarmLocationId",
                        column: x => x.FarmLocationId,
                        principalTable: "FarmLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AscensionMaterials_CharacterId",
                table: "AscensionMaterials",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMaterial_TalentMaterialsId",
                table: "CharacterMaterial",
                column: "TalentMaterialsId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterUser_UserId",
                table: "CharacterUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DaysOfWeekWeaponAscensionMaterial_WeaponAscensionMaterialId",
                table: "DaysOfWeekWeaponAscensionMaterial",
                column: "WeaponAscensionMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmLocationMaterial_MaterialsId",
                table: "FarmLocationMaterial",
                column: "MaterialsId");

            migrationBuilder.CreateIndex(
                name: "IX_Talents_CharacterId",
                table: "Talents",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWeapon_WeaponsId",
                table: "UserWeapon",
                column: "WeaponsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterMaterial");

            migrationBuilder.DropTable(
                name: "CharacterUser");

            migrationBuilder.DropTable(
                name: "DaysOfWeekWeaponAscensionMaterial");

            migrationBuilder.DropTable(
                name: "FarmLocationMaterial");

            migrationBuilder.DropTable(
                name: "Talents");

            migrationBuilder.DropTable(
                name: "UserWeapon");

            migrationBuilder.DropTable(
                name: "DaysOfWeeks");

            migrationBuilder.DropTable(
                name: "AscensionMaterials");

            migrationBuilder.DropTable(
                name: "FarmLocations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Weapons");

            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
