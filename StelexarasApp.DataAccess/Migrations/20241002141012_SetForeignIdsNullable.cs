using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SetForeignIdsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skines_Omadarxes_OmadarxisId",
                table: "Skines");

            migrationBuilder.DropForeignKey(
                name: "FK_Tomeis_Tomearxes_TomearxisId",
                table: "Tomeis");

            migrationBuilder.DropIndex(
                name: "IX_Tomeis_TomearxisId",
                table: "Tomeis");

            migrationBuilder.DropIndex(
                name: "IX_Skines_OmadarxisId",
                table: "Skines");

            migrationBuilder.AlterColumn<int>(
                name: "TomearxisId",
                table: "Tomeis",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "XwrosName",
                table: "Tomearxes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OmadarxisId",
                table: "Skines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "XwrosName",
                table: "Omadarxes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XwrosName",
                table: "Koinotarxes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XwrosName",
                table: "Ekpaideutes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tomeis_TomearxisId",
                table: "Tomeis",
                column: "TomearxisId",
                unique: true,
                filter: "[TomearxisId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Skines_OmadarxisId",
                table: "Skines",
                column: "OmadarxisId",
                unique: true,
                filter: "[OmadarxisId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Skines_Omadarxes_OmadarxisId",
                table: "Skines",
                column: "OmadarxisId",
                principalTable: "Omadarxes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tomeis_Tomearxes_TomearxisId",
                table: "Tomeis",
                column: "TomearxisId",
                principalTable: "Tomearxes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skines_Omadarxes_OmadarxisId",
                table: "Skines");

            migrationBuilder.DropForeignKey(
                name: "FK_Tomeis_Tomearxes_TomearxisId",
                table: "Tomeis");

            migrationBuilder.DropIndex(
                name: "IX_Tomeis_TomearxisId",
                table: "Tomeis");

            migrationBuilder.DropIndex(
                name: "IX_Skines_OmadarxisId",
                table: "Skines");

            migrationBuilder.DropColumn(
                name: "XwrosName",
                table: "Tomearxes");

            migrationBuilder.DropColumn(
                name: "XwrosName",
                table: "Omadarxes");

            migrationBuilder.DropColumn(
                name: "XwrosName",
                table: "Koinotarxes");

            migrationBuilder.DropColumn(
                name: "XwrosName",
                table: "Ekpaideutes");

            migrationBuilder.AlterColumn<int>(
                name: "TomearxisId",
                table: "Tomeis",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OmadarxisId",
                table: "Skines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tomeis_TomearxisId",
                table: "Tomeis",
                column: "TomearxisId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skines_OmadarxisId",
                table: "Skines",
                column: "OmadarxisId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Skines_Omadarxes_OmadarxisId",
                table: "Skines",
                column: "OmadarxisId",
                principalTable: "Omadarxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tomeis_Tomearxes_TomearxisId",
                table: "Tomeis",
                column: "TomearxisId",
                principalTable: "Tomearxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
