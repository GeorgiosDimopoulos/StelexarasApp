using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class SetFirstNameLastnames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Tomearxes",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Paidia",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Omadarxes",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Koinotarxes",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Ekpaideutes",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Tomearxes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Paidia",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Omadarxes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Koinotarxes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Ekpaideutes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Tomearxes");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Paidia");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Omadarxes");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Koinotarxes");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Ekpaideutes");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Tomearxes",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Paidia",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Omadarxes",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Koinotarxes",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Ekpaideutes",
                newName: "FullName");
        }
    }
}
