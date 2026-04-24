using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeaStyleOficial.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoConstrutorProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensConjunto_Pedidos_PedidoID",
                table: "ItensConjunto");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensConjunto_Produtos_ProdutoID",
                table: "ItensConjunto");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensConjunto_Produtos_ProdutoID1",
                table: "ItensConjunto");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensConjunto_Produtos_ProdutoID2",
                table: "ItensConjunto");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagamentos_Pedidos_PedidoID",
                table: "Pagamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_ClienteID",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Produtos_ProdutoID",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Reembolsos_Pedidos_PedidoID",
                table: "Reembolsos");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Metodo",
                table: "Pagamentos");

            migrationBuilder.RenameColumn(
                name: "PedidoID",
                table: "Reembolsos",
                newName: "PedidoId");

            migrationBuilder.RenameColumn(
                name: "ReembolsoID",
                table: "Reembolsos",
                newName: "ReembolsoId");

            migrationBuilder.RenameIndex(
                name: "IX_Reembolsos_PedidoID",
                table: "Reembolsos",
                newName: "IX_Reembolsos_PedidoId");

            migrationBuilder.RenameColumn(
                name: "ProdutoID",
                table: "Produtos",
                newName: "ProdutoId");

            migrationBuilder.RenameColumn(
                name: "Tamanho",
                table: "Produtos",
                newName: "TipoProduto");

            migrationBuilder.RenameColumn(
                name: "Categoria",
                table: "Produtos",
                newName: "TamanhoProduto");

            migrationBuilder.RenameColumn(
                name: "ProdutoID",
                table: "Pedidos",
                newName: "ProdutoId");

            migrationBuilder.RenameColumn(
                name: "ClienteID",
                table: "Pedidos",
                newName: "ClienteId");

            migrationBuilder.RenameColumn(
                name: "PedidoID",
                table: "Pedidos",
                newName: "PedidoId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_ProdutoID",
                table: "Pedidos",
                newName: "IX_Pedidos_ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_ClienteID",
                table: "Pedidos",
                newName: "IX_Pedidos_ClienteId");

            migrationBuilder.RenameColumn(
                name: "PedidoID",
                table: "Pagamentos",
                newName: "PedidoId");

            migrationBuilder.RenameColumn(
                name: "PagamentoID",
                table: "Pagamentos",
                newName: "PagamentoId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Pagamentos",
                newName: "Parcelas");

            migrationBuilder.RenameIndex(
                name: "IX_Pagamentos_PedidoID",
                table: "Pagamentos",
                newName: "IX_Pagamentos_PedidoId");

            migrationBuilder.RenameColumn(
                name: "ProdutoID2",
                table: "ItensConjunto",
                newName: "ProdutoId2");

            migrationBuilder.RenameColumn(
                name: "ProdutoID1",
                table: "ItensConjunto",
                newName: "ProdutoId1");

            migrationBuilder.RenameColumn(
                name: "ProdutoID",
                table: "ItensConjunto",
                newName: "ProdutoId");

            migrationBuilder.RenameColumn(
                name: "PedidoID",
                table: "ItensConjunto",
                newName: "PedidoId");

            migrationBuilder.RenameColumn(
                name: "ConjuntoId",
                table: "ItensConjunto",
                newName: "ConjuntoProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_ItensConjunto_ProdutoID2",
                table: "ItensConjunto",
                newName: "IX_ItensConjunto_ProdutoId2");

            migrationBuilder.RenameIndex(
                name: "IX_ItensConjunto_ProdutoID1",
                table: "ItensConjunto",
                newName: "IX_ItensConjunto_ProdutoId1");

            migrationBuilder.RenameIndex(
                name: "IX_ItensConjunto_ProdutoID",
                table: "ItensConjunto",
                newName: "IX_ItensConjunto_ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_ItensConjunto_PedidoID",
                table: "ItensConjunto",
                newName: "IX_ItensConjunto_PedidoId");

            migrationBuilder.RenameIndex(
                name: "IX_ItensConjunto_ConjuntoId",
                table: "ItensConjunto",
                newName: "IX_ItensConjunto_ConjuntoProdutoId");

            migrationBuilder.RenameColumn(
                name: "UsuarioID",
                table: "Clientes",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "UsuarioID",
                table: "Administradores",
                newName: "UsuarioId");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaProduto",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensConjunto_Pedidos_PedidoId",
                table: "ItensConjunto",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "PedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensConjunto_Produtos_ProdutoId",
                table: "ItensConjunto",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "ProdutoId",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Pagamentos_Pedidos_PedidoId",
                table: "Pagamentos",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "PedidoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_ClienteId",
                table: "Pedidos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Produtos_ProdutoId",
                table: "Pedidos",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reembolsos_Pedidos_PedidoId",
                table: "Reembolsos",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "PedidoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensConjunto_Pedidos_PedidoId",
                table: "ItensConjunto");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensConjunto_Produtos_ProdutoId",
                table: "ItensConjunto");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensConjunto_Produtos_ProdutoId1",
                table: "ItensConjunto");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensConjunto_Produtos_ProdutoId2",
                table: "ItensConjunto");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagamentos_Pedidos_PedidoId",
                table: "Pagamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_ClienteId",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Produtos_ProdutoId",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Reembolsos_Pedidos_PedidoId",
                table: "Reembolsos");

            migrationBuilder.DropColumn(
                name: "CategoriaProduto",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "PedidoId",
                table: "Reembolsos",
                newName: "PedidoID");

            migrationBuilder.RenameColumn(
                name: "ReembolsoId",
                table: "Reembolsos",
                newName: "ReembolsoID");

            migrationBuilder.RenameIndex(
                name: "IX_Reembolsos_PedidoId",
                table: "Reembolsos",
                newName: "IX_Reembolsos_PedidoID");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "Produtos",
                newName: "ProdutoID");

            migrationBuilder.RenameColumn(
                name: "TipoProduto",
                table: "Produtos",
                newName: "Tamanho");

            migrationBuilder.RenameColumn(
                name: "TamanhoProduto",
                table: "Produtos",
                newName: "Categoria");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "Pedidos",
                newName: "ProdutoID");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Pedidos",
                newName: "ClienteID");

            migrationBuilder.RenameColumn(
                name: "PedidoId",
                table: "Pedidos",
                newName: "PedidoID");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_ProdutoId",
                table: "Pedidos",
                newName: "IX_Pedidos_ProdutoID");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_ClienteId",
                table: "Pedidos",
                newName: "IX_Pedidos_ClienteID");

            migrationBuilder.RenameColumn(
                name: "PedidoId",
                table: "Pagamentos",
                newName: "PedidoID");

            migrationBuilder.RenameColumn(
                name: "PagamentoId",
                table: "Pagamentos",
                newName: "PagamentoID");

            migrationBuilder.RenameColumn(
                name: "Parcelas",
                table: "Pagamentos",
                newName: "Status");

            migrationBuilder.RenameIndex(
                name: "IX_Pagamentos_PedidoId",
                table: "Pagamentos",
                newName: "IX_Pagamentos_PedidoID");

            migrationBuilder.RenameColumn(
                name: "ProdutoId2",
                table: "ItensConjunto",
                newName: "ProdutoID2");

            migrationBuilder.RenameColumn(
                name: "ProdutoId1",
                table: "ItensConjunto",
                newName: "ProdutoID1");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "ItensConjunto",
                newName: "ProdutoID");

            migrationBuilder.RenameColumn(
                name: "PedidoId",
                table: "ItensConjunto",
                newName: "PedidoID");

            migrationBuilder.RenameColumn(
                name: "ConjuntoProdutoId",
                table: "ItensConjunto",
                newName: "ConjuntoId");

            migrationBuilder.RenameIndex(
                name: "IX_ItensConjunto_ProdutoId2",
                table: "ItensConjunto",
                newName: "IX_ItensConjunto_ProdutoID2");

            migrationBuilder.RenameIndex(
                name: "IX_ItensConjunto_ProdutoId1",
                table: "ItensConjunto",
                newName: "IX_ItensConjunto_ProdutoID1");

            migrationBuilder.RenameIndex(
                name: "IX_ItensConjunto_ProdutoId",
                table: "ItensConjunto",
                newName: "IX_ItensConjunto_ProdutoID");

            migrationBuilder.RenameIndex(
                name: "IX_ItensConjunto_PedidoId",
                table: "ItensConjunto",
                newName: "IX_ItensConjunto_PedidoID");

            migrationBuilder.RenameIndex(
                name: "IX_ItensConjunto_ConjuntoProdutoId",
                table: "ItensConjunto",
                newName: "IX_ItensConjunto_ConjuntoId");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Clientes",
                newName: "UsuarioID");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Administradores",
                newName: "UsuarioID");

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Metodo",
                table: "Pagamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensConjunto_Pedidos_PedidoID",
                table: "ItensConjunto",
                column: "PedidoID",
                principalTable: "Pedidos",
                principalColumn: "PedidoID");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensConjunto_Produtos_ProdutoID",
                table: "ItensConjunto",
                column: "ProdutoID",
                principalTable: "Produtos",
                principalColumn: "ProdutoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensConjunto_Produtos_ProdutoID1",
                table: "ItensConjunto",
                column: "ProdutoID1",
                principalTable: "Produtos",
                principalColumn: "ProdutoID");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensConjunto_Produtos_ProdutoID2",
                table: "ItensConjunto",
                column: "ProdutoID2",
                principalTable: "Produtos",
                principalColumn: "ProdutoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagamentos_Pedidos_PedidoID",
                table: "Pagamentos",
                column: "PedidoID",
                principalTable: "Pedidos",
                principalColumn: "PedidoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_ClienteID",
                table: "Pedidos",
                column: "ClienteID",
                principalTable: "Clientes",
                principalColumn: "UsuarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Produtos_ProdutoID",
                table: "Pedidos",
                column: "ProdutoID",
                principalTable: "Produtos",
                principalColumn: "ProdutoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reembolsos_Pedidos_PedidoID",
                table: "Reembolsos",
                column: "PedidoID",
                principalTable: "Pedidos",
                principalColumn: "PedidoID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
