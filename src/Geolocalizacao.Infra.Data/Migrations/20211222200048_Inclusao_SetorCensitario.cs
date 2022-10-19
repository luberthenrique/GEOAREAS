using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Geolocalizacao.Infra.Data.Migrations
{
    public partial class Inclusao_SetorCensitario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArquivoSetoresCensitarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NomeArquivo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IdUsuarioInclusao = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsuarioInclusaoId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivoSetoresCensitarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArquivoSetoresCensitarios_AspNetUsers_UsuarioInclusaoId",
                        column: x => x.UsuarioInclusaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ArquivoSetoresCensitarios_UsuarioInclusaoId",
                table: "ArquivoSetoresCensitarios",
                column: "UsuarioInclusaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArquivoSetoresCensitarios");
        }
    }
}
