using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateKoinotitaTomeasRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Koinotites_Tomeis_TomeasId",
                table: "Koinotites");

            migrationBuilder.DropForeignKey(
                name: "FK_Skines_Koinotites_KoinotitaId",
                table: "Skines");

            migrationBuilder.AddForeignKey(
                name: "FK_Koinotites_Tomeis_TomeasId",
                table: "Koinotites",
                column: "TomeasId",
                principalTable: "Tomeis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skines_Koinotites_KoinotitaId",
                table: "Skines",
                column: "KoinotitaId",
                principalTable: "Koinotites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Koinotites_Tomeis_TomeasId",
                table: "Koinotites");

            migrationBuilder.DropForeignKey(
                name: "FK_Skines_Koinotites_KoinotitaId",
                table: "Skines");

            migrationBuilder.AddForeignKey(
                name: "FK_Koinotites_Tomeis_TomeasId",
                table: "Koinotites",
                column: "TomeasId",
                principalTable: "Tomeis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skines_Koinotites_KoinotitaId",
                table: "Skines",
                column: "KoinotitaId",
                principalTable: "Koinotites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
