using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class SetNullWhenDeletingOmadarxis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skines_Omadarxes_OmadarxisId",
                table: "Skines");

            migrationBuilder.AddForeignKey(
                name: "FK_Skines_Omadarxes_OmadarxisId",
                table: "Skines",
                column: "OmadarxisId",
                principalTable: "Omadarxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skines_Omadarxes_OmadarxisId",
                table: "Skines");

            migrationBuilder.AddForeignKey(
                name: "FK_Skines_Omadarxes_OmadarxisId",
                table: "Skines",
                column: "OmadarxisId",
                principalTable: "Omadarxes",
                principalColumn: "Id");
        }
    }
}
