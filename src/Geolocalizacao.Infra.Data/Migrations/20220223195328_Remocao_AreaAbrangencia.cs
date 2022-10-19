using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Geolocalizacao.Infra.Data.Migrations
{
    public partial class Remocao_AreaAbrangencia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AreaAbrangencia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AreaAbrangencia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IdMunicipio = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdUsuarioAlteracao = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IdUsuarioInclusao = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Latitude = table.Column<double>(type: "double", nullable: false),
                    Localidade = table.Column<string>(type: "VARCHAR(200)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Longitude = table.Column<double>(type: "double", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Uf = table.Column<string>(type: "CHAR(2)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaAbrangencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AreaAbrangencia_AspNetUsers_IdUsuarioAlteracao",
                        column: x => x.IdUsuarioAlteracao,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AreaAbrangencia_AspNetUsers_IdUsuarioInclusao",
                        column: x => x.IdUsuarioInclusao,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AreaAbrangencia_IdUsuarioAlteracao",
                table: "AreaAbrangencia",
                column: "IdUsuarioAlteracao");

            migrationBuilder.CreateIndex(
                name: "IX_AreaAbrangencia_IdUsuarioInclusao",
                table: "AreaAbrangencia",
                column: "IdUsuarioInclusao");
        }
    }
}
