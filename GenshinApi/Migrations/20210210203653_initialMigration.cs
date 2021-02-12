using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GenshinFarm.Api.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    PowerLvl = table.Column<int>(type: "integer", nullable: false),
                    Rarity = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Type = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    WeaponType = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DaysOfWeeks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysOfWeeks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FarmLocations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Weekly = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Email = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Role = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    LastTimeLoged = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Rarity = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Type = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Attack = table.Column<int>(type: "integer", nullable: false),
                    PowerLvl = table.Column<int>(type: "integer", nullable: false),
                    Desciption = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Rarity = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    CharacterId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Talents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    PowerLvl = table.Column<int>(type: "integer", nullable: false),
                    CharacterId = table.Column<string>(type: "text", nullable: true)
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
                    Lvl = table.Column<int>(type: "integer", nullable: false),
                    PowerLvl = table.Column<int>(type: "integer", nullable: false),
                    ElementId = table.Column<string>(type: "text", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "CharacterMaterial",
                columns: table => new
                {
                    CharactersId = table.Column<string>(type: "text", nullable: false),
                    TalentMaterialsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterMaterial", x => new { x.CharactersId, x.TalentMaterialsId });
                    table.ForeignKey(
                        name: "FK_CharacterMaterial_Characters_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterMaterial_Materials_TalentMaterialsId",
                        column: x => x.TalentMaterialsId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DaysOfWeekMaterial",
                columns: table => new
                {
                    DaysAvailableId = table.Column<string>(type: "text", nullable: false),
                    MaterialsId = table.Column<string>(type: "text", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "FarmLocationMaterial",
                columns: table => new
                {
                    FarmLocationId = table.Column<string>(type: "text", nullable: false),
                    MaterialsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmLocationMaterial", x => new { x.FarmLocationId, x.MaterialsId });
                    table.ForeignKey(
                        name: "FK_FarmLocationMaterial_FarmLocations_FarmLocationId",
                        column: x => x.FarmLocationId,
                        principalTable: "FarmLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FarmLocationMaterial_Materials_MaterialsId",
                        column: x => x.MaterialsId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMaterial_TalentMaterialsId",
                table: "CharacterMaterial",
                column: "TalentMaterialsId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterUser_UserId",
                table: "CharacterUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DaysOfWeekMaterial_MaterialsId",
                table: "DaysOfWeekMaterial",
                column: "MaterialsId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmLocationMaterial_MaterialsId",
                table: "FarmLocationMaterial",
                column: "MaterialsId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_CharacterId",
                table: "Materials",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Talents_CharacterId",
                table: "Talents",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserElement_UserId",
                table: "UserElement",
                column: "UserId");

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
                name: "DaysOfWeekMaterial");

            migrationBuilder.DropTable(
                name: "FarmLocationMaterial");

            migrationBuilder.DropTable(
                name: "Talents");

            migrationBuilder.DropTable(
                name: "UserElement");

            migrationBuilder.DropTable(
                name: "UserWeapon");

            migrationBuilder.DropTable(
                name: "DaysOfWeeks");

            migrationBuilder.DropTable(
                name: "FarmLocations");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Weapons");

            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
