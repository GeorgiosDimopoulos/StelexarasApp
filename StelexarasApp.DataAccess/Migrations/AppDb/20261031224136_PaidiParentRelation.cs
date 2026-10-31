using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class PaidiParentRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AFM = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.Id);
                });

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Paidia",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Keep the existing data: one Parent per distinct ParentTel, then point every paidi at it.
            migrationBuilder.Sql(
                @"INSERT INTO Parents (FirstName, LastName, Age, Sex, Phone, AFM)
                  SELECT DISTINCT '', '', 0, 0, ParentTel, '' FROM Paidia");
            migrationBuilder.Sql(
                @"UPDATE p SET ParentId = (SELECT MIN(pa.Id) FROM Parents pa WHERE pa.Phone = p.ParentTel)
                  FROM Paidia p");

            migrationBuilder.CreateIndex(
                name: "IX_Paidia_ParentId",
                table: "Paidia",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Paidia_Parents_ParentId",
                table: "Paidia",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropColumn(
                name: "ParentTel",
                table: "Paidia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParentTel",
                table: "Paidia",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(
                @"UPDATE p SET ParentTel = pa.Phone FROM Paidia p JOIN Parents pa ON pa.Id = p.ParentId");

            migrationBuilder.DropForeignKey(
                name: "FK_Paidia_Parents_ParentId",
                table: "Paidia");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Paidia_ParentId",
                table: "Paidia");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Paidia");
        }
    }
}
