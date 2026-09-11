using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class CheckKoinotarxisIdInOmadarxes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Omadarxes_Koinotarxes_KoinotarxisId",
                table: "Omadarxes");

            migrationBuilder.DropIndex(        
                name: "IX_Omadarxes_KoinotarxisId",
                table: "Omadarxes");

            migrationBuilder.DropColumn(
                name: "KoinotarxisId",
                table: "Omadarxes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
