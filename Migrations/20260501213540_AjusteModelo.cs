using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NeaStyleOficial.Migrations
{
    /// <inheritdoc />
    public partial class AjusteModelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensConjunto_Produtos_ProdutoId1",
                table: "ItensConjunto");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensConjunto_Produtos_ProdutoId2",
                table: "ItensConjunto");

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
                name: "EstoqueAtual",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "TamanhoProduto",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Cor",
                table: "ItensConjunto");

            migrationBuilder.DropColumn(
                name: "Tamanho",
                table: "ItensConjunto");

            migrationBuilder.RenameColumn(
                name: "ProdutoId2",
                table: "ItensConjunto",
                newName: "ProdutoVariacaoId3");

            migrationBuilder.RenameColumn(
                name: "ProdutoId1",
                table: "ItensConjunto",
                newName: "ProdutoVariacaoId2");

            migrationBuilder.RenameIndex(
                name: "IX_ItensConjunto_ProdutoId2",
                table: "ItensConjunto",
                newName: "IX_ItensConjunto_ProdutoVariacaoId3");

            migrationBuilder.RenameIndex(
                name: "IX_ItensConjunto_ProdutoId1",
                table: "ItensConjunto",
                newName: "IX_ItensConjunto_ProdutoVariacaoId2");

            migrationBuilder.AddColumn<long>(
                name: "ProdutoVariacaoId",
                table: "ItensConjunto",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProdutoVariacaoId1",
                table: "ItensConjunto",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProdutoVariacoes",
                columns: table => new
                {
                    ProdutoVariacaoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TamanhoProduto = table.Column<int>(type: "int", nullable: false),
                    CorProduto = table.Column<int>(type: "int", nullable: false),
                    Estoque = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoVariacoes", x => x.ProdutoVariacaoId);
                    table.ForeignKey(
                        name: "FK_ProdutoVariacoes_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItensConjunto_ProdutoVariacaoId",
                table: "ItensConjunto",
                column: "ProdutoVariacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensConjunto_ProdutoVariacaoId1",
                table: "ItensConjunto",
                column: "ProdutoVariacaoId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoVariacoes_ProdutoId",
                table: "ProdutoVariacoes",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensConjunto_ProdutoVariacoes_ProdutoVariacaoId",
                table: "ItensConjunto",
                column: "ProdutoVariacaoId",
                principalTable: "ProdutoVariacoes",
                principalColumn: "ProdutoVariacaoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensConjunto_ProdutoVariacoes_ProdutoVariacaoId1",
                table: "ItensConjunto",
                column: "ProdutoVariacaoId1",
                principalTable: "ProdutoVariacoes",
                principalColumn: "ProdutoVariacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensConjunto_ProdutoVariacoes_ProdutoVariacaoId2",
                table: "ItensConjunto",
                column: "ProdutoVariacaoId2",
                principalTable: "ProdutoVariacoes",
                principalColumn: "ProdutoVariacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensConjunto_ProdutoVariacoes_ProdutoVariacaoId3",
                table: "ItensConjunto",
                column: "ProdutoVariacaoId3",
                principalTable: "ProdutoVariacoes",
                principalColumn: "ProdutoVariacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensConjunto_ProdutoVariacoes_ProdutoVariacaoId",
                table: "ItensConjunto");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensConjunto_ProdutoVariacoes_ProdutoVariacaoId1",
                table: "ItensConjunto");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensConjunto_ProdutoVariacoes_ProdutoVariacaoId2",
                table: "ItensConjunto");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensConjunto_ProdutoVariacoes_ProdutoVariacaoId3",
                table: "ItensConjunto");

            migrationBuilder.DropTable(
                name: "ProdutoVariacoes");

            migrationBuilder.DropIndex(
                name: "IX_ItensConjunto_ProdutoVariacaoId",
                table: "ItensConjunto");

            migrationBuilder.DropIndex(
                name: "IX_ItensConjunto_ProdutoVariacaoId1",
                table: "ItensConjunto");

            migrationBuilder.DropColumn(
                name: "ProdutoVariacaoId",
                table: "ItensConjunto");

            migrationBuilder.DropColumn(
                name: "ProdutoVariacaoId1",
                table: "ItensConjunto");

            migrationBuilder.RenameColumn(
                name: "ProdutoVariacaoId3",
                table: "ItensConjunto",
                newName: "ProdutoId2");

            migrationBuilder.RenameColumn(
                name: "ProdutoVariacaoId2",
                table: "ItensConjunto",
                newName: "ProdutoId1");

            migrationBuilder.RenameIndex(
                name: "IX_ItensConjunto_ProdutoVariacaoId3",
                table: "ItensConjunto",
                newName: "IX_ItensConjunto_ProdutoId2");

            migrationBuilder.RenameIndex(
                name: "IX_ItensConjunto_ProdutoVariacaoId2",
                table: "ItensConjunto",
                newName: "IX_ItensConjunto_ProdutoId1");

            migrationBuilder.AddColumn<string>(
                name: "Cor",
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

            migrationBuilder.AddColumn<int>(
                name: "TamanhoProduto",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Cor",
                table: "ItensConjunto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tamanho",
                table: "ItensConjunto",
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

            migrationBuilder.AddForeignKey(
                name: "FK_ItensConjunto_Produtos_ProdutoId1",
                table: "ItensConjunto",
                column: "ProdutoId1",
                principalTable: "Produtos",
                principalColumn: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensConjunto_Produtos_ProdutoId2",
                table: "ItensConjunto",
                column: "ProdutoId2",
                principalTable: "Produtos",
                principalColumn: "ProdutoId");
        }
    }
}
