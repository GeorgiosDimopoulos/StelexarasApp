using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddEidikoStelexos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnwtatosXwroi_Anwtata_AnwtatosId",
                table: "AnwtatosXwroi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Anwtata",
                table: "Anwtata");

            migrationBuilder.RenameTable(
                name: "Anwtata",
                newName: "Anwtatos");

            migrationBuilder.RenameIndex(
                name: "IX_Anwtata_Tel",
                table: "Anwtatos",
                newName: "IX_Anwtatos_Tel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Anwtatos",
                table: "Anwtatos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EidikoStelexos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Thesi = table.Column<int>(type: "int", nullable: false),
                    XwrosName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EidikoStelexos", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AnwtatosXwroi_Anwtatos_AnwtatosId",
                table: "AnwtatosXwroi",
                column: "AnwtatosId",
                principalTable: "Anwtatos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnwtatosXwroi_Anwtatos_AnwtatosId",
                table: "AnwtatosXwroi");

            migrationBuilder.DropTable(
                name: "EidikoStelexos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Anwtatos",
                table: "Anwtatos");

            migrationBuilder.RenameTable(
                name: "Anwtatos",
                newName: "Anwtata");

            migrationBuilder.RenameIndex(
                name: "IX_Anwtatos_Tel",
                table: "Anwtata",
                newName: "IX_Anwtata_Tel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Anwtata",
                table: "Anwtata",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnwtatosXwroi_Anwtata_AnwtatosId",
                table: "AnwtatosXwroi",
                column: "AnwtatosId",
                principalTable: "Anwtata",
                principalColumn: "Id");
        }
    }
}
