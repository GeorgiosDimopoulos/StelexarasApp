using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class CheckKoinotarxisIdInOmadarxes2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KoinotarxisId",
                table: "Omadarxes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Omadarxes_KoinotarxisId",
                table: "Omadarxes",
                column: "KoinotarxisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Omadarxes_Koinotarxes_KoinotarxisId",
                table: "Omadarxes",
                column: "KoinotarxisId",
                principalTable: "Koinotarxes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Omadarxes_Koinotarxes_KoinotarxisId",
                table: "Omadarxes");

            migrationBuilder.DropIndex(
                name: "IX_Omadarxes_KoinotarxisId",
                table: "Omadarxes");

            migrationBuilder.DropColumn(
                name: "KoinotarxisGl",
                table: "Omadarxes");
        }
    }
}
