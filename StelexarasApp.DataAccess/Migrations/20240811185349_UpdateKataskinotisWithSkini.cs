using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateKataskinotisWithSkini : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SkiniId",
                table: "Kataskinotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Kataskinotes_SkiniId",
                table: "Kataskinotes",
                column: "SkiniId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kataskinotes_Skines_SkiniId",
                table: "Kataskinotes",
                column: "SkiniId",
                principalTable: "Skines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kataskinotes_Skines_SkiniId",
                table: "Kataskinotes");

            migrationBuilder.DropIndex(
                name: "IX_Kataskinotes_SkiniId",
                table: "Kataskinotes");

            migrationBuilder.DropColumn(
                name: "SkiniId",
                table: "Kataskinotes");
        }
    }
}
