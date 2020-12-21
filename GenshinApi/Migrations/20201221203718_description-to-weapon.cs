using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GenshinFarm.Api.Migrations
{
    public partial class descriptiontoweapon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Desciption",
                table: "Weapons",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastTimeLoged",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desciption",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "LastTimeLoged",
                table: "Users");
        }
    }
}
