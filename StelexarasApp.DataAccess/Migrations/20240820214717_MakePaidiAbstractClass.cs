using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MakePaidiAbstractClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kataskinotes_Skines_SkiniId",
                table: "Kataskinotes");

            migrationBuilder.DropTable(
                name: "Ekpaideuomenoi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kataskinotes",
                table: "Kataskinotes");

            migrationBuilder.RenameTable(
                name: "Kataskinotes",
                newName: "Paidi");

            migrationBuilder.RenameIndex(
                name: "IX_Kataskinotes_SkiniId",
                table: "Paidi",
                newName: "IX_Paidi_SkiniId");

            migrationBuilder.AlterColumn<int>(
                name: "SkiniId",
                table: "Paidi",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SeAdeia",
                table: "Paidi",
                type: "bit",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paidi_Skines_SkiniId",
                table: "Paidi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paidi",
                table: "Paidi");

            migrationBuilder.DropColumn(
                name: "SeAdeia",
                table: "Paidi");

            migrationBuilder.RenameTable(
                name: "Paidi",
                newName: "Kataskinotes");

            migrationBuilder.RenameIndex(
                name: "IX_Paidi_SkiniId",
                table: "Kataskinotes",
                newName: "IX_Kataskinotes_SkiniId");

            migrationBuilder.AlterColumn<int>(
                name: "SkiniId",
                table: "Kataskinotes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kataskinotes",
                table: "Kataskinotes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Ekpaideuomenoi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkiniId = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaidiType = table.Column<int>(type: "int", nullable: false),
                    SeAdeia = table.Column<bool>(type: "bit", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ekpaideuomenoi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ekpaideuomenoi_Skines_SkiniId",
                        column: x => x.SkiniId,
                        principalTable: "Skines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ekpaideuomenoi_SkiniId",
                table: "Ekpaideuomenoi",
                column: "SkiniId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kataskinotes_Skines_SkiniId",
                table: "Kataskinotes",
                column: "SkiniId",
                principalTable: "Skines",
                principalColumn: "Id");
        }
    }
}
