using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_rpg.Migrations
{
    public partial class AddCharacterClassConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Users_UserId",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "Victory",
                table: "Characters",
                newName: "Victories");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ClassConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class = table.Column<int>(type: "int", nullable: false),
                    MaxStrength = table.Column<int>(type: "int", nullable: false),
                    MaxDefence = table.Column<int>(type: "int", nullable: false),
                    MaxIntelligence = table.Column<int>(type: "int", nullable: false),
                    MaxWeaponDamage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassConfigurations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassConfigurations_Class",
                table: "ClassConfigurations",
                column: "Class",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Users_UserId",
                table: "Characters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Users_UserId",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "ClassConfigurations");

            migrationBuilder.RenameColumn(
                name: "Victories",
                table: "Characters",
                newName: "Victory");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Characters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Users_UserId",
                table: "Characters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
