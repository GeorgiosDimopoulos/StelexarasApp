using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddToPaidiType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ekpaideuomenos_Skines_SkiniId",
                table: "Ekpaideuomenos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ekpaideuomenos",
                table: "Ekpaideuomenos");

            migrationBuilder.RenameTable(
                name: "Ekpaideuomenos",
                newName: "Ekpaideuomenoi");

            migrationBuilder.RenameIndex(
                name: "IX_Ekpaideuomenos_SkiniId",
                table: "Ekpaideuomenoi",
                newName: "IX_Ekpaideuomenoi_SkiniId");

            migrationBuilder.AddColumn<int>(
                name: "PaidiType",
                table: "Kataskinotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaidiType",
                table: "Ekpaideuomenoi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ekpaideuomenoi",
                table: "Ekpaideuomenoi",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ekpaideuomenoi_Skines_SkiniId",
                table: "Ekpaideuomenoi",
                column: "SkiniId",
                principalTable: "Skines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ekpaideuomenoi_Skines_SkiniId",
                table: "Ekpaideuomenoi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ekpaideuomenoi",
                table: "Ekpaideuomenoi");

            migrationBuilder.DropColumn(
                name: "PaidiType",
                table: "Kataskinotes");

            migrationBuilder.DropColumn(
                name: "PaidiType",
                table: "Ekpaideuomenoi");

            migrationBuilder.RenameTable(
                name: "Ekpaideuomenoi",
                newName: "Ekpaideuomenos");

            migrationBuilder.RenameIndex(
                name: "IX_Ekpaideuomenoi_SkiniId",
                table: "Ekpaideuomenos",
                newName: "IX_Ekpaideuomenos_SkiniId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ekpaideuomenos",
                table: "Ekpaideuomenos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ekpaideuomenos_Skines_SkiniId",
                table: "Ekpaideuomenos",
                column: "SkiniId",
                principalTable: "Skines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
