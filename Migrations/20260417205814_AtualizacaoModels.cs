using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeaStyleOficial.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "ConjuntoProdutoSequence");

            migrationBuilder.AlterColumn<int>(
                name: "Tamanho",
                table: "Produtos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Produtos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoCusto",
                table: "Produtos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Carrinhos",
                columns: table => new
                {
                    ConjuntoProdutoId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR [ConjuntoProdutoSequence]"),
                    ClienteId = table.Column<long>(type: "bigint", nullable: false),
                    Finalizado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrinhos", x => x.ConjuntoProdutoId);
                    table.ForeignKey(
                        name: "FK_Carrinhos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favoritos",
                columns: table => new
                {
                    ConjuntoProdutoId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR [ConjuntoProdutoSequence]"),
                    ClienteId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoritos", x => x.ConjuntoProdutoId);
                    table.ForeignKey(
                        name: "FK_Favoritos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    PedidoID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteID = table.Column<long>(type: "bigint", nullable: false),
                    DataPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ProdutoID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.PedidoID);
                    table.ForeignKey(
                        name: "FK_Pedidos_Clientes_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "Clientes",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_Produtos_ProdutoID",
                        column: x => x.ProdutoID,
                        principalTable: "Produtos",
                        principalColumn: "ProdutoID");
                });

            migrationBuilder.CreateTable(
                name: "ItensConjunto",
                columns: table => new
                {
                    ItemConjuntoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoID = table.Column<long>(type: "bigint", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ConjuntoId = table.Column<long>(type: "bigint", nullable: false),
                    PedidoID = table.Column<long>(type: "bigint", nullable: true),
                    ProdutoID1 = table.Column<long>(type: "bigint", nullable: true),
                    ProdutoID2 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensConjunto", x => x.ItemConjuntoId);
                    table.ForeignKey(
                        name: "FK_ItensConjunto_Pedidos_PedidoID",
                        column: x => x.PedidoID,
                        principalTable: "Pedidos",
                        principalColumn: "PedidoID");
                    table.ForeignKey(
                        name: "FK_ItensConjunto_Produtos_ProdutoID",
                        column: x => x.ProdutoID,
                        principalTable: "Produtos",
                        principalColumn: "ProdutoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensConjunto_Produtos_ProdutoID1",
                        column: x => x.ProdutoID1,
                        principalTable: "Produtos",
                        principalColumn: "ProdutoID");
                    table.ForeignKey(
                        name: "FK_ItensConjunto_Produtos_ProdutoID2",
                        column: x => x.ProdutoID2,
                        principalTable: "Produtos",
                        principalColumn: "ProdutoID");
                });

            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    PagamentoID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoID = table.Column<long>(type: "bigint", nullable: false),
                    MetodoPagamento = table.Column<int>(type: "int", nullable: false),
                    StatusPagamento = table.Column<int>(type: "int", nullable: false),
                    Metodo = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ValorPago = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.PagamentoID);
                    table.ForeignKey(
                        name: "FK_Pagamentos_Pedidos_PedidoID",
                        column: x => x.PedidoID,
                        principalTable: "Pedidos",
                        principalColumn: "PedidoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reembolsos",
                columns: table => new
                {
                    ReembolsoID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoID = table.Column<long>(type: "bigint", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataSolicitacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorReembolso = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reembolsos", x => x.ReembolsoID);
                    table.ForeignKey(
                        name: "FK_Reembolsos_Pedidos_PedidoID",
                        column: x => x.PedidoID,
                        principalTable: "Pedidos",
                        principalColumn: "PedidoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carrinhos_ClienteId",
                table: "Carrinhos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_ClienteId",
                table: "Favoritos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensConjunto_ConjuntoId",
                table: "ItensConjunto",
                column: "ConjuntoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensConjunto_PedidoID",
                table: "ItensConjunto",
                column: "PedidoID");

            migrationBuilder.CreateIndex(
                name: "IX_ItensConjunto_ProdutoID",
                table: "ItensConjunto",
                column: "ProdutoID");

            migrationBuilder.CreateIndex(
                name: "IX_ItensConjunto_ProdutoID1",
                table: "ItensConjunto",
                column: "ProdutoID1");

            migrationBuilder.CreateIndex(
                name: "IX_ItensConjunto_ProdutoID2",
                table: "ItensConjunto",
                column: "ProdutoID2");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_PedidoID",
                table: "Pagamentos",
                column: "PedidoID");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteID",
                table: "Pedidos",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ProdutoID",
                table: "Pedidos",
                column: "ProdutoID");

            migrationBuilder.CreateIndex(
                name: "IX_Reembolsos_PedidoID",
                table: "Reembolsos",
                column: "PedidoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carrinhos");

            migrationBuilder.DropTable(
                name: "Favoritos");

            migrationBuilder.DropTable(
                name: "ItensConjunto");

            migrationBuilder.DropTable(
                name: "Pagamentos");

            migrationBuilder.DropTable(
                name: "Reembolsos");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropColumn(
                name: "PrecoCusto",
                table: "Produtos");

            migrationBuilder.DropSequence(
                name: "ConjuntoProdutoSequence");

            migrationBuilder.AlterColumn<string>(
                name: "Tamanho",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "Preco",
                table: "Produtos",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
