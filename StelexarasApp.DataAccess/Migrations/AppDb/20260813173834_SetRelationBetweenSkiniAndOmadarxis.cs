using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class SetRelationBetweenSkiniAndOmadarxis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Koinotites_Koinotarxes_KoinotarxisId",
                table: "Koinotites");

            migrationBuilder.DropForeignKey(
                name: "FK_Tomeis_Tomearxes_TomearxisId",
                table: "Tomeis");

            migrationBuilder.AddForeignKey(
                name: "FK_Koinotites_Koinotarxes_KoinotarxisId",
                table: "Koinotites",
                column: "KoinotarxisId",
                principalTable: "Koinotarxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tomeis_Tomearxes_TomearxisId",
                table: "Tomeis",
                column: "TomearxisId",
                principalTable: "Tomearxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Koinotites_Koinotarxes_KoinotarxisId",
                table: "Koinotites");

            migrationBuilder.DropForeignKey(
                name: "FK_Tomeis_Tomearxes_TomearxisId",
                table: "Tomeis");

            migrationBuilder.AddForeignKey(
                name: "FK_Koinotites_Koinotarxes_KoinotarxisId",
                table: "Koinotites",
                column: "KoinotarxisId",
                principalTable: "Koinotarxes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tomeis_Tomearxes_TomearxisId",
                table: "Tomeis",
                column: "TomearxisId",
                principalTable: "Tomearxes",
                principalColumn: "Id");
        }
    }
}
