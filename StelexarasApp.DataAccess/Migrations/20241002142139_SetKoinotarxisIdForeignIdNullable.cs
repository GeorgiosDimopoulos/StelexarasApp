using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SetKoinotarxisIdForeignIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Koinotites_Koinotarxes_KoinotarxisId",
                table: "Koinotites");

            migrationBuilder.DropIndex(
                name: "IX_Koinotites_KoinotarxisId",
                table: "Koinotites");

            migrationBuilder.AlterColumn<int>(
                name: "KoinotarxisId",
                table: "Koinotites",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Koinotites_KoinotarxisId",
                table: "Koinotites",
                column: "KoinotarxisId",
                unique: true,
                filter: "[KoinotarxisId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Koinotites_Koinotarxes_KoinotarxisId",
                table: "Koinotites",
                column: "KoinotarxisId",
                principalTable: "Koinotarxes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Koinotites_Koinotarxes_KoinotarxisId",
                table: "Koinotites");

            migrationBuilder.DropIndex(
                name: "IX_Koinotites_KoinotarxisId",
                table: "Koinotites");

            migrationBuilder.AlterColumn<int>(
                name: "KoinotarxisId",
                table: "Koinotites",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Koinotites_KoinotarxisId",
                table: "Koinotites",
                column: "KoinotarxisId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Koinotites_Koinotarxes_KoinotarxisId",
                table: "Koinotites",
                column: "KoinotarxisId",
                principalTable: "Koinotarxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
