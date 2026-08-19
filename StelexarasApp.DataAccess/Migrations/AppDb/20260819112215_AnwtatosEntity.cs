using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AnwtatosEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anwtata",
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
                    table.PrimaryKey("PK_Anwtata", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ekpaideutes_Tel",
                table: "Ekpaideutes",
                column: "Tel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Anwtata_Tel",
                table: "Anwtata",
                column: "Tel",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anwtata");

            migrationBuilder.DropIndex(
                name: "IX_Ekpaideutes_Tel",
                table: "Ekpaideutes");
        }
    }
}
