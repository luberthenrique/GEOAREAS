using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Geolocalizacao.Infra.Data.Migrations
{
    public partial class Alteracao_ApiCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiCliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdCliente = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "VARCHAR(70)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApiKey = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecretKey = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdUsuarioInclusao = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DataHoraInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IdUsuarioAlteracao = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiCliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiCliente_AspNetUsers_IdUsuarioAlteracao",
                        column: x => x.IdUsuarioAlteracao,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApiCliente_AspNetUsers_IdUsuarioInclusao",
                        column: x => x.IdUsuarioInclusao,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApiCliente_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ApiCliente_IdCliente",
                table: "ApiCliente",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_ApiCliente_IdUsuarioAlteracao",
                table: "ApiCliente",
                column: "IdUsuarioAlteracao");

            migrationBuilder.CreateIndex(
                name: "IX_ApiCliente_IdUsuarioInclusao",
                table: "ApiCliente",
                column: "IdUsuarioInclusao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiCliente");
        }
    }
}
