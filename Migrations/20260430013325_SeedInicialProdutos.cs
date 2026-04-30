using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NeaStyleOficial.Migrations
{
    /// <inheritdoc />
    public partial class SeedInicialProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cor",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EstoqueAtual",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImagemUrl",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FotoUrl",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "ProdutoId", "CategoriaProduto", "Cor", "Descricao", "EstoqueAtual", "ImagemUrl", "Nome", "Preco", "PrecoCusto", "TamanhoProduto", "TipoProduto" },
                values: new object[,]
                {
                    { 1L, 1, "Vermelho", "Vestido para formaturas e eventos.", 20, "https://example.com/vestido-chique.jpg", "Vestido Chique", 299.90m, 150.00m, 1, 2 },
                    { 2L, 0, "Azul", "Calça para o dia a dia.", 15, "https://example.com/calca-cargo.jpg", "Calça Cargo", 299.90m, 160.00m, 2, 1 },
                    { 3L, 1, "Preto", "Saia estilosa e versátil.", 25, "https://example.com/saia-jeans.jpg", "Saia Jeans", 120.00m, 90.00m, 0, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 3L);

            migrationBuilder.DropColumn(
                name: "Cor",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "EstoqueAtual",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "ImagemUrl",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "FotoUrl",
                table: "Clientes");
        }
    }
}
