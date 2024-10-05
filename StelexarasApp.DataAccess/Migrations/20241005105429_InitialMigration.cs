using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
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
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ekpaideutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Thesi = table.Column<int>(type: "int", nullable: false),
                    XwrosName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ekpaideutes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Thesi = table.Column<int>(type: "int", nullable: false),
                    XwrosName = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Thesi = table.Column<int>(type: "int", nullable: false),
                    TomearxisId = table.Column<int>(type: "int", nullable: true),
                    XwrosName = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TomearxisId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tomeis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tomeis_Tomearxes_TomearxisId",
                        column: x => x.TomearxisId,
                        principalTable: "Tomearxes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Omadarxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Thesi = table.Column<int>(type: "int", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KoinotarxisId = table.Column<int>(type: "int", nullable: true),
                    XwrosName = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Koinotites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KoinotarxisId = table.Column<int>(type: "int", nullable: true),
                    TomeasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Koinotites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Koinotites_Koinotarxes_KoinotarxisId",
                        column: x => x.KoinotarxisId,
                        principalTable: "Koinotarxes",
                        principalColumn: "Id");
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
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OmadarxisId = table.Column<int>(type: "int", nullable: true),
                    Sex = table.Column<int>(type: "int", nullable: false),
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Paidia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    SeAdeia = table.Column<bool>(type: "bit", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    PaidiType = table.Column<int>(type: "int", nullable: false),
                    SkiniId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paidia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paidia_Skines_SkiniId",
                        column: x => x.SkiniId,
                        principalTable: "Skines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Duties_Name",
                table: "Duties",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ekpaideutes_Tel",
                table: "Ekpaideutes",
                column: "Tel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Koinotarxes_Tel",
                table: "Koinotarxes",
                column: "Tel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Koinotarxes_TomearxisId",
                table: "Koinotarxes",
                column: "TomearxisId");

            migrationBuilder.CreateIndex(
                name: "IX_Koinotites_KoinotarxisId",
                table: "Koinotites",
                column: "KoinotarxisId",
                unique: true,
                filter: "[KoinotarxisId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Koinotites_Name",
                table: "Koinotites",
                column: "Name",
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
                name: "IX_Omadarxes_Tel",
                table: "Omadarxes",
                column: "Tel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paidia_SkiniId",
                table: "Paidia",
                column: "SkiniId");

            migrationBuilder.CreateIndex(
                name: "IX_Skines_KoinotitaId",
                table: "Skines",
                column: "KoinotitaId");

            migrationBuilder.CreateIndex(
                name: "IX_Skines_Name",
                table: "Skines",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skines_OmadarxisId",
                table: "Skines",
                column: "OmadarxisId",
                unique: true,
                filter: "[OmadarxisId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tomearxes_Tel",
                table: "Tomearxes",
                column: "Tel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tomeis_Name",
                table: "Tomeis",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tomeis_TomearxisId",
                table: "Tomeis",
                column: "TomearxisId",
                unique: true,
                filter: "[TomearxisId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Duties");

            migrationBuilder.DropTable(
                name: "Ekpaideutes");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Paidia");

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
