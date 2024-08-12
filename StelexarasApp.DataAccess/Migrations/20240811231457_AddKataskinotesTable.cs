using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddKataskinotesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kataskinotes_Skines_SkiniId",
                table: "Kataskinotes");

            migrationBuilder.AlterColumn<int>(
                name: "SkiniId",
                table: "Kataskinotes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Kataskinotes_Skines_SkiniId",
                table: "Kataskinotes",
                column: "SkiniId",
                principalTable: "Skines",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kataskinotes_Skines_SkiniId",
                table: "Kataskinotes");

            migrationBuilder.AlterColumn<int>(
                name: "SkiniId",
                table: "Kataskinotes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Kataskinotes_Skines_SkiniId",
                table: "Kataskinotes",
                column: "SkiniId",
                principalTable: "Skines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
