using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class GeneralQuickUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ekpaideutis_Tomeis_TomeasId",
                table: "Ekpaideutis");

            migrationBuilder.DropIndex(
                name: "IX_Ekpaideutis_TomeasId",
                table: "Ekpaideutis");

            migrationBuilder.DropColumn(
                name: "TomeasId",
                table: "Ekpaideutis");

            migrationBuilder.AddColumn<string>(
                name: "Tel",
                table: "Tomearxes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tel",
                table: "Omadarxes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tel",
                table: "Koinotarxes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tel",
                table: "Ekpaideutis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tel",
                table: "Tomearxes");

            migrationBuilder.DropColumn(
                name: "Tel",
                table: "Omadarxes");

            migrationBuilder.DropColumn(
                name: "Tel",
                table: "Koinotarxes");

            migrationBuilder.DropColumn(
                name: "Tel",
                table: "Ekpaideutis");

            migrationBuilder.AddColumn<int>(
                name: "TomeasId",
                table: "Ekpaideutis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ekpaideutis_TomeasId",
                table: "Ekpaideutis",
                column: "TomeasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ekpaideutis_Tomeis_TomeasId",
                table: "Ekpaideutis",
                column: "TomeasId",
                principalTable: "Tomeis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
