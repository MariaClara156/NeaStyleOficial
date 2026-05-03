using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeaStyleOficial.Migrations
{
    /// <inheritdoc />
    public partial class ReestruturacaoProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TamanhoProduto",
                table: "ProdutoVariacoes",
                newName: "Tamanho");

            migrationBuilder.RenameColumn(
                name: "CorProduto",
                table: "ProdutoVariacoes",
                newName: "Cor");

            migrationBuilder.RenameColumn(
                name: "TipoProduto",
                table: "Produtos",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "CategoriaProduto",
                table: "Produtos",
                newName: "Categoria");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tamanho",
                table: "ProdutoVariacoes",
                newName: "TamanhoProduto");

            migrationBuilder.RenameColumn(
                name: "Cor",
                table: "ProdutoVariacoes",
                newName: "CorProduto");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Produtos",
                newName: "TipoProduto");

            migrationBuilder.RenameColumn(
                name: "Categoria",
                table: "Produtos",
                newName: "CategoriaProduto");
        }
    }
}
