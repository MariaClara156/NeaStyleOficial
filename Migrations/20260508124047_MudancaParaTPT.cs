using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeaStyleOficial.Migrations
{
    /// <inheritdoc />
    public partial class MudancaParaTPT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensConjunto_Pedidos_PedidoId",
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

            migrationBuilder.DropForeignKey(
                name: "FK_ItensConjunto_Produtos_ProdutoId",
                table: "ItensConjunto");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensPedido_Pedidos_PedidoId",
                table: "ItensPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensPedido_Pedidos_PedidoId1",
                table: "ItensPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensPedido_ProdutoVariacoes_ProdutoVariacaoId",
                table: "ItensPedido");

            migrationBuilder.DropIndex(
                name: "IX_ItensPedido_PedidoId1",
                table: "ItensPedido");

            migrationBuilder.DropIndex(
                name: "IX_ItensPedido_ProdutoVariacaoId",
                table: "ItensPedido");

            migrationBuilder.DropIndex(
                name: "IX_ItensConjunto_ConjuntoProdutoId",
                table: "ItensConjunto");

            migrationBuilder.DropIndex(
                name: "IX_ItensConjunto_PedidoId",
                table: "ItensConjunto");

            migrationBuilder.DropIndex(
                name: "IX_ItensConjunto_ProdutoId",
                table: "ItensConjunto");

            migrationBuilder.DropIndex(
                name: "IX_ItensConjunto_ProdutoVariacaoId1",
                table: "ItensConjunto");

            migrationBuilder.DeleteData(
                table: "Administradores",
                keyColumn: "UsuarioId",
                keyValue: 1L);

            migrationBuilder.DropColumn(
                name: "PedidoId1",
                table: "ItensPedido");

            migrationBuilder.DropColumn(
                name: "ConjuntoProdutoId",
                table: "ItensConjunto");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "ItensConjunto");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "ItensConjunto");

            migrationBuilder.DropColumn(
                name: "ProdutoVariacaoId1",
                table: "ItensConjunto");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ItensPedido",
                newName: "ItemPedidoId");

            migrationBuilder.RenameColumn(
                name: "ProdutoVariacaoId3",
                table: "ItensConjunto",
                newName: "FavoritoId");

            migrationBuilder.RenameColumn(
                name: "ProdutoVariacaoId2",
                table: "ItensConjunto",
                newName: "CarrinhoId");

            migrationBuilder.RenameIndex(
                name: "IX_ItensConjunto_ProdutoVariacaoId3",
                table: "ItensConjunto",
                newName: "IX_ItensConjunto_FavoritoId");

            migrationBuilder.RenameIndex(
                name: "IX_ItensConjunto_ProdutoVariacaoId2",
                table: "ItensConjunto",
                newName: "IX_ItensConjunto_CarrinhoId");

            migrationBuilder.AddColumn<long>(
                name: "ClienteUsuarioId",
                table: "Pedidos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagemUrl",
                table: "ItensPedido",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeProduto",
                table: "ItensPedido",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteUsuarioId",
                table: "Pedidos",
                column: "ClienteUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensConjunto_Carrinhos_CarrinhoId",
                table: "ItensConjunto",
                column: "CarrinhoId",
                principalTable: "Carrinhos",
                principalColumn: "ConjuntoProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensConjunto_Favoritos_FavoritoId",
                table: "ItensConjunto",
                column: "FavoritoId",
                principalTable: "Favoritos",
                principalColumn: "ConjuntoProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensPedido_Pedidos_PedidoId",
                table: "ItensPedido",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "PedidoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_ClienteUsuarioId",
                table: "Pedidos",
                column: "ClienteUsuarioId",
                principalTable: "Clientes",
                principalColumn: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensConjunto_Carrinhos_CarrinhoId",
                table: "ItensConjunto");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensConjunto_Favoritos_FavoritoId",
                table: "ItensConjunto");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensPedido_Pedidos_PedidoId",
                table: "ItensPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_ClienteUsuarioId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_ClienteUsuarioId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ClienteUsuarioId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ImagemUrl",
                table: "ItensPedido");

            migrationBuilder.DropColumn(
                name: "NomeProduto",
                table: "ItensPedido");

            migrationBuilder.RenameColumn(
                name: "ItemPedidoId",
                table: "ItensPedido",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "FavoritoId",
                table: "ItensConjunto",
                newName: "ProdutoVariacaoId3");

            migrationBuilder.RenameColumn(
                name: "CarrinhoId",
                table: "ItensConjunto",
                newName: "ProdutoVariacaoId2");

            migrationBuilder.RenameIndex(
                name: "IX_ItensConjunto_FavoritoId",
                table: "ItensConjunto",
                newName: "IX_ItensConjunto_ProdutoVariacaoId3");

            migrationBuilder.RenameIndex(
                name: "IX_ItensConjunto_CarrinhoId",
                table: "ItensConjunto",
                newName: "IX_ItensConjunto_ProdutoVariacaoId2");

            migrationBuilder.AddColumn<long>(
                name: "PedidoId1",
                table: "ItensPedido",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ConjuntoProdutoId",
                table: "ItensConjunto",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PedidoId",
                table: "ItensConjunto",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProdutoId",
                table: "ItensConjunto",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProdutoVariacaoId1",
                table: "ItensConjunto",
                type: "bigint",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Administradores",
                columns: new[] { "UsuarioId", "Cargo", "Email", "Nome", "Senha" },
                values: new object[] { 1L, "Gerente", "mariaclara4290@gmail.com", "Admin Master", "admin123" });

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedido_PedidoId1",
                table: "ItensPedido",
                column: "PedidoId1");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedido_ProdutoVariacaoId",
                table: "ItensPedido",
                column: "ProdutoVariacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensConjunto_ConjuntoProdutoId",
                table: "ItensConjunto",
                column: "ConjuntoProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensConjunto_PedidoId",
                table: "ItensConjunto",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensConjunto_ProdutoId",
                table: "ItensConjunto",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensConjunto_ProdutoVariacaoId1",
                table: "ItensConjunto",
                column: "ProdutoVariacaoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensConjunto_Pedidos_PedidoId",
                table: "ItensConjunto",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "PedidoId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ItensConjunto_Produtos_ProdutoId",
                table: "ItensConjunto",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "ProdutoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensPedido_Pedidos_PedidoId",
                table: "ItensPedido",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "PedidoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensPedido_Pedidos_PedidoId1",
                table: "ItensPedido",
                column: "PedidoId1",
                principalTable: "Pedidos",
                principalColumn: "PedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensPedido_ProdutoVariacoes_ProdutoVariacaoId",
                table: "ItensPedido",
                column: "ProdutoVariacaoId",
                principalTable: "ProdutoVariacoes",
                principalColumn: "ProdutoVariacaoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
