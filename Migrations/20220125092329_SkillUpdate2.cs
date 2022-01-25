using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_rpg.Migrations
{
    public partial class SkillUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Damage", "Name" },
                values: new object[] { 1, 20, "zap" });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Damage", "Name" },
                values: new object[] { 2, 35, "arrows" });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Damage", "Name" },
                values: new object[] { 3, 50, "fireball" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
