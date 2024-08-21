using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPaidiDiscriminator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paidi_Skines_SkiniId",
                table: "Paidi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paidi",
                table: "Paidi");

            migrationBuilder.RenameTable(
                name: "Paidi",
                newName: "Paidia");

            migrationBuilder.RenameIndex(
                name: "IX_Paidi_SkiniId",
                table: "Paidia",
                newName: "IX_Paidia_SkiniId");

            migrationBuilder.AlterColumn<bool>(
                name: "SeAdeia",
                table: "Paidia",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paidia",
                table: "Paidia",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Paidia_Skines_SkiniId",
                table: "Paidia",
                column: "SkiniId",
                principalTable: "Skines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paidia_Skines_SkiniId",
                table: "Paidia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paidia",
                table: "Paidia");

            migrationBuilder.RenameTable(
                name: "Paidia",
                newName: "Paidi");

            migrationBuilder.RenameIndex(
                name: "IX_Paidia_SkiniId",
                table: "Paidi",
                newName: "IX_Paidi_SkiniId");

            migrationBuilder.AlterColumn<bool>(
                name: "SeAdeia",
                table: "Paidi",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paidi",
                table: "Paidi",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Paidi_Skines_SkiniId",
                table: "Paidi",
                column: "SkiniId",
                principalTable: "Skines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
