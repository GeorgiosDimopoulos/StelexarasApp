using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class RemoveUniqueIndexFromEkpaideutisXwrosName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ekpaideutes_Tel",
                table: "Ekpaideutes");

            migrationBuilder.DropIndex(
                name: "IX_Ekpaideutes_XwrosName",
                table: "Ekpaideutes");

            migrationBuilder.AlterColumn<string>(
                name: "XwrosName",
                table: "Ekpaideutes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "XwrosName",
                table: "Ekpaideutes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Ekpaideutes_Tel",
                table: "Ekpaideutes",
                column: "Tel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ekpaideutes_XwrosName",
                table: "Ekpaideutes",
                column: "XwrosName",
                unique: true);
        }
    }
}
