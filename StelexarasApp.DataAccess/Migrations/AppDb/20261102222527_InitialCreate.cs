using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StelexarasApp.DataAccess.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anwtatos",
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
                    table.PrimaryKey("PK_Anwtatos", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Ekpaideutes",
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
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Omadarxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Thesi = table.Column<int>(type: "int", nullable: false),
                    XwrosName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Omadarxes", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Tomearxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Thesi = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    XwrosName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tomearxes", x => x.Id);
                });

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
                        name: "FK_AnwtatosXwroi_Anwtatos_AnwtatosId",
                        column: x => x.AnwtatosId,
                        principalTable: "Anwtatos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Koinotarxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeAdeia = table.Column<bool>(type: "bit", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Thesi = table.Column<int>(type: "int", nullable: false),
                    XwrosName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
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
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TomearxisId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tomeis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tomeis_Tomearxes_TomearxisId",
                        column: x => x.TomearxisId,
                        principalTable: "Tomearxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Koinotites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KoinotarxisId = table.Column<int>(type: "int", nullable: true),
                    TomeasId = table.Column<int>(type: "int", nullable: false),
                    Hlikia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Koinotites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Koinotites_Koinotarxes_KoinotarxisId",
                        column: x => x.KoinotarxisId,
                        principalTable: "Koinotarxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Koinotites_Tomeis_TomeasId",
                        column: x => x.TomeasId,
                        principalTable: "Tomeis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Skines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: true),
                    OmadarxisId = table.Column<int>(type: "int", nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Skines_Omadarxes_OmadarxisId",
                        column: x => x.OmadarxisId,
                        principalTable: "Omadarxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Paidia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    SeAdeia = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    PaidiType = table.Column<int>(type: "int", nullable: false),
                    SkiniId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paidia", x => x.Id);
                    table.CheckConstraint("CK_Paidia_Sex", "[Sex] IN (0, 1)");
                    table.ForeignKey(
                        name: "FK_Paidia_Parents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Parents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Paidia_Skines_SkiniId",
                        column: x => x.SkiniId,
                        principalTable: "Skines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anwtatos_Tel",
                table: "Anwtatos",
                column: "Tel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnwtatosXwroi_AnwtatosId",
                table: "AnwtatosXwroi",
                column: "AnwtatosId");

            migrationBuilder.CreateIndex(
                name: "IX_AnwtatosXwroi_Name",
                table: "AnwtatosXwroi",
                column: "Name",
                unique: true);

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
                name: "IX_Omadarxes_Tel",
                table: "Omadarxes",
                column: "Tel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paidia_ParentId",
                table: "Paidia",
                column: "ParentId");

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
                name: "AnwtatosXwroi");

            migrationBuilder.DropTable(
                name: "Duties");

            migrationBuilder.DropTable(
                name: "EidikoStelexos");

            migrationBuilder.DropTable(
                name: "Ekpaideutes");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Paidia");

            migrationBuilder.DropTable(
                name: "Anwtatos");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropTable(
                name: "Skines");

            migrationBuilder.DropTable(
                name: "Koinotites");

            migrationBuilder.DropTable(
                name: "Omadarxes");

            migrationBuilder.DropTable(
                name: "Koinotarxes");

            migrationBuilder.DropTable(
                name: "Tomeis");

            migrationBuilder.DropTable(
                name: "Tomearxes");
        }
    }
}
