using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace Geolocalizacao.Infra.Data.Migrations
{
    public partial class Inlcusao_Cliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArquivoSetoresCensitarios_AspNetUsers_UsuarioInclusaoId",
                table: "ArquivoSetoresCensitarios");

            migrationBuilder.DropTable(
                name: "bairros");

            migrationBuilder.DropIndex(
                name: "IX_ArquivoSetoresCensitarios_UsuarioInclusaoId",
                table: "ArquivoSetoresCensitarios");

            migrationBuilder.DropColumn(
                name: "UsuarioInclusaoId",
                table: "ArquivoSetoresCensitarios");

            migrationBuilder.AlterColumn<string>(
                name: "NomeArquivo",
                table: "ArquivoSetoresCensitarios",
                type: "varchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Cnpj = table.Column<string>(type: "VARCHAR(14)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InscricaoMunicipal = table.Column<string>(type: "VARCHAR(18)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RazaoSocial = table.Column<string>(type: "VARCHAR(300)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observacao = table.Column<string>(type: "VARCHAR(300)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Logradouro = table.Column<string>(type: "VARCHAR(120)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Numero = table.Column<string>(type: "VARCHAR(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Complemento = table.Column<string>(type: "VARCHAR(60)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bairro = table.Column<string>(type: "VARCHAR(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cidade = table.Column<string>(type: "VARCHAR(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Uf = table.Column<string>(type: "CHAR(2)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cep = table.Column<string>(type: "VARCHAR(8)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefone1 = table.Column<string>(type: "VARCHAR(11)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefone2 = table.Column<string>(type: "VARCHAR(11)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "VARCHAR(254)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdUsuarioInclusao = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DataHoraInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IdUsuarioAlteracao = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ArquivoSetoresCensitarios_IdUsuarioInclusao",
                table: "ArquivoSetoresCensitarios",
                column: "IdUsuarioInclusao");

            migrationBuilder.AddForeignKey(
                name: "FK_ArquivoSetoresCensitarios_AspNetUsers_IdUsuarioInclusao",
                table: "ArquivoSetoresCensitarios",
                column: "IdUsuarioInclusao",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArquivoSetoresCensitarios_AspNetUsers_IdUsuarioInclusao",
                table: "ArquivoSetoresCensitarios");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropIndex(
                name: "IX_ArquivoSetoresCensitarios_IdUsuarioInclusao",
                table: "ArquivoSetoresCensitarios");

            migrationBuilder.AlterColumn<string>(
                name: "NomeArquivo",
                table: "ArquivoSetoresCensitarios",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioInclusaoId",
                table: "ArquivoSetoresCensitarios",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "bairros",
                columns: table => new
                {
                    OGR_FID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cd_setor = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_dist = table.Column<string>(type: "varchar(9)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_mun = table.Column<string>(type: "varchar(7)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_sit = table.Column<string>(type: "varchar(1)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_subdist = table.Column<string>(type: "varchar(11)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cd_uf = table.Column<string>(type: "varchar(2)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SHAPE = table.Column<Geometry>(type: "geometry", nullable: false),
                    nm_mun = table.Column<string>(type: "varchar(60)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nm_dist = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nm_subdist = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nm_uf = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nm_sit = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sigla_uf = table.Column<string>(type: "varchar(2)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bairros", x => x.OGR_FID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ArquivoSetoresCensitarios_UsuarioInclusaoId",
                table: "ArquivoSetoresCensitarios",
                column: "UsuarioInclusaoId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ArquivoSetoresCensitarios_AspNetUsers_UsuarioInclusaoId",
                table: "ArquivoSetoresCensitarios",
                column: "UsuarioInclusaoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
