using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeaStyleOficial.Migrations
{
    /// <inheritdoc />
    public partial class PrecoVariacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Preco",
                table: "ProdutoVariacoes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "ItensPedido",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<long>(type: "bigint", nullable: false),
                    ProdutoVariacaoId = table.Column<long>(type: "bigint", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PedidoId1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensPedido_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "PedidoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItensPedido_Pedidos_PedidoId1",
                        column: x => x.PedidoId1,
                        principalTable: "Pedidos",
                        principalColumn: "PedidoId");
                    table.ForeignKey(
                        name: "FK_ItensPedido_ProdutoVariacoes_ProdutoVariacaoId",
                        column: x => x.ProdutoVariacaoId,
                        principalTable: "ProdutoVariacoes",
                        principalColumn: "ProdutoVariacaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedido_PedidoId",
                table: "ItensPedido",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedido_PedidoId1",
                table: "ItensPedido",
                column: "PedidoId1");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedido_ProdutoVariacaoId",
                table: "ItensPedido",
                column: "ProdutoVariacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensPedido");

            migrationBuilder.DropColumn(
                name: "Preco",
                table: "ProdutoVariacoes");
        }
    }
}
