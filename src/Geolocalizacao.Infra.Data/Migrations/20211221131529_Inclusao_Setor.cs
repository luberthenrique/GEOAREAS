using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace Geolocalizacao.Infra.Data.Migrations
{
    public partial class Inclusao_Setor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bairros",
                columns: table => new
                {
                    OGR_FID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SHAPE = table.Column<Geometry>(type: "geometry", nullable: false),
                    cd_setor = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_sit = table.Column<string>(type: "varchar(1)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nm_sit = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_uf = table.Column<string>(type: "varchar(2)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nm_uf = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sigla_uf = table.Column<string>(type: "varchar(2)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_mun = table.Column<string>(type: "varchar(7)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nm_mun = table.Column<string>(type: "varchar(60)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_dist = table.Column<string>(type: "varchar(9)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nm_dist = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_subdist = table.Column<string>(type: "varchar(11)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nm_subdist = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bairros", x => x.OGR_FID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_bairros_OGR_FID",
                table: "bairros",
                column: "OGR_FID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bairros_SHAPE",
                table: "bairros",
                column: "SHAPE")
                .Annotation("MySql:SpatialIndex", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bairros");
        }
    }
}
