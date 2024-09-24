using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Duties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tomearxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Thesi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tomearxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Koinotarxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Thesi = table.Column<int>(type: "int", nullable: false),
                    TomearxisId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Koinotarxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Koinotarxes_Tomearxes_TomearxisId",
                        column: x => x.TomearxisId,
                        principalTable: "Tomearxes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tomeis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TomearxisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tomeis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tomeis_Tomearxes_TomearxisId",
                        column: x => x.TomearxisId,
                        principalTable: "Tomearxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Omadarxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Thesi = table.Column<int>(type: "int", nullable: false),
                    KoinotarxisId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Omadarxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Omadarxes_Koinotarxes_KoinotarxisId",
                        column: x => x.KoinotarxisId,
                        principalTable: "Koinotarxes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ekpaideutis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    TomeasId = table.Column<int>(type: "int", nullable: false),
                    Thesi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ekpaideutis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ekpaideutis_Tomeis_TomeasId",
                        column: x => x.TomeasId,
                        principalTable: "Tomeis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Koinotites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    KoinotarxisId = table.Column<int>(type: "int", nullable: false),
                    TomeasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Koinotites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Koinotites_Koinotarxes_KoinotarxisId",
                        column: x => x.KoinotarxisId,
                        principalTable: "Koinotarxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Koinotites_Tomeis_TomeasId",
                        column: x => x.TomeasId,
                        principalTable: "Tomeis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    OmadarxisId = table.Column<int>(type: "int", nullable: false),
                    KoinotitaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skines_Koinotites_KoinotitaId",
                        column: x => x.KoinotitaId,
                        principalTable: "Koinotites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Skines_Omadarxes_OmadarxisId",
                        column: x => x.OmadarxisId,
                        principalTable: "Omadarxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ekpaideuomenoi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    SeAdeia = table.Column<bool>(type: "bit", nullable: false),
                    SkiniId = table.Column<int>(type: "int", nullable: false),
                    PaidiType = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Kataskinotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    SkiniId = table.Column<int>(type: "int", nullable: true),
                    PaidiType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kataskinotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kataskinotes_Skines_SkiniId",
                        column: x => x.SkiniId,
                        principalTable: "Skines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ekpaideuomenoi_SkiniId",
                table: "Ekpaideuomenoi",
                column: "SkiniId");

            migrationBuilder.CreateIndex(
                name: "IX_Ekpaideutis_TomeasId",
                table: "Ekpaideutis",
                column: "TomeasId");

            migrationBuilder.CreateIndex(
                name: "IX_Kataskinotes_SkiniId",
                table: "Kataskinotes",
                column: "SkiniId");

            migrationBuilder.CreateIndex(
                name: "IX_Koinotarxes_TomearxisId",
                table: "Koinotarxes",
                column: "TomearxisId");

            migrationBuilder.CreateIndex(
                name: "IX_Koinotites_KoinotarxisId",
                table: "Koinotites",
                column: "KoinotarxisId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Koinotites_TomeasId",
                table: "Koinotites",
                column: "TomeasId");

            migrationBuilder.CreateIndex(
                name: "IX_Omadarxes_KoinotarxisId",
                table: "Omadarxes",
                column: "KoinotarxisId");

            migrationBuilder.CreateIndex(
                name: "IX_Skines_KoinotitaId",
                table: "Skines",
                column: "KoinotitaId");

            migrationBuilder.CreateIndex(
                name: "IX_Skines_OmadarxisId",
                table: "Skines",
                column: "OmadarxisId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tomeis_TomearxisId",
                table: "Tomeis",
                column: "TomearxisId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Duties");

            migrationBuilder.DropTable(
                name: "Ekpaideuomenoi");

            migrationBuilder.DropTable(
                name: "Ekpaideutis");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Kataskinotes");

            migrationBuilder.DropTable(
                name: "Skines");

            migrationBuilder.DropTable(
                name: "Koinotites");

            migrationBuilder.DropTable(
                name: "Omadarxes");

            migrationBuilder.DropTable(
                name: "Tomeis");

            migrationBuilder.DropTable(
                name: "Koinotarxes");

            migrationBuilder.DropTable(
                name: "Tomearxes");
        }
    }
}
