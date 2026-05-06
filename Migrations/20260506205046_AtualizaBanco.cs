using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeaStyleOficial.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemUrl",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Preco",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "PrecoCusto",
                table: "Produtos");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoCusto",
                table: "ProdutoVariacoes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecoCusto",
                table: "ProdutoVariacoes");

            migrationBuilder.AddColumn<string>(
                name: "ImagemUrl",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Preco",
                table: "Produtos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoCusto",
                table: "Produtos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
