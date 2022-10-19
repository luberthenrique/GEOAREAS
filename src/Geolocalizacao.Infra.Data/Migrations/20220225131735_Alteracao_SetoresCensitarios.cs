using Microsoft.EntityFrameworkCore.Migrations;

namespace Geolocalizacao.Infra.Data.Migrations
{
    public partial class Alteracao_SetoresCensitarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "ArquivoSetoresCensitarios",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Erro",
                table: "ArquivoSetoresCensitarios",
                type: "varchar(200)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Erro",
                table: "ArquivoSetoresCensitarios");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "ArquivoSetoresCensitarios",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
