using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SopalS.Migrations
{
    /// <inheritdoc />
    public partial class Initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emplacement",
                columns: table => new
                {
                    Codeemp = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    libele = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emplacement", x => x.Codeemp);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateur", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conteneur",
                columns: table => new
                {
                    Ref = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeBarres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateMiseEnService = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodiciteEtalonnage = table.Column<int>(type: "int", nullable: false),
                    DateDernierEtalonnage = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DernierPoids = table.Column<double>(type: "float", nullable: false),
                    Unite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserUpdate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmplacementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conteneur", x => x.Ref);
                    table.ForeignKey(
                        name: "FK_Conteneur_Emplacement_EmplacementId",
                        column: x => x.EmplacementId,
                        principalTable: "Emplacement",
                        principalColumn: "Codeemp",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoEtalonnages",
                columns: table => new
                {
                    Ref = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Poids = table.Column<float>(type: "real", nullable: false),
                    Unite = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoEtalonnages", x => new { x.Ref, x.Date });
                    table.ForeignKey(
                        name: "FK_HistoEtalonnages_Conteneur_Ref",
                        column: x => x.Ref,
                        principalTable: "Conteneur",
                        principalColumn: "Ref",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conteneur_EmplacementId",
                table: "Conteneur",
                column: "EmplacementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoEtalonnages");

            migrationBuilder.DropTable(
                name: "Utilisateur");

            migrationBuilder.DropTable(
                name: "Conteneur");

            migrationBuilder.DropTable(
                name: "Emplacement");
        }
    }
}
