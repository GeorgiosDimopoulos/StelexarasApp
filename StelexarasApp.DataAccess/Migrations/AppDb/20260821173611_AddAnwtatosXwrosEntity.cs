using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddAnwtatosXwrosEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnwtatosXwroi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AnwtatosId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnwtatosXwroi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnwtatosXwroi_Anwtata_AnwtatosId",
                        column: x => x.AnwtatosId,
                        principalTable: "Anwtata",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnwtatosXwroi_AnwtatosId",
                table: "AnwtatosXwroi",
                column: "AnwtatosId");

            migrationBuilder.CreateIndex(
                name: "IX_AnwtatosXwroi_Name",
                table: "AnwtatosXwroi",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnwtatosXwroi");
        }
    }
}
