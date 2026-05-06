using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Models.Users;

namespace NeaStyleOficial.Models.Sales
{
    public class ItemPedido
    {
        public long ItemPedidoId { get; set; }
        // Um item de pedido pertence a um pedido
        public long PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        // Um item de pedido está associado a uma variação de produto, mas armazena o nome e preço no momento da compra para histórico
        public long ProdutoVariacaoId { get; set; }
        public string NomeProduto { get; set; }
        public string? ImagemUrl { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

        protected ItemPedido() { }
        public ItemPedido(
            long pedidoId,
            long produtoVariacaoId,
            string nomeProduto,
            int quantidade,
            decimal precoUnitario,
            string? imagemUrl = null)
        {
            if (quantidade <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero");

            if (precoUnitario < 0)
                throw new ArgumentException("Preço unitário inválido");

            PedidoId = pedidoId;
            ProdutoVariacaoId = produtoVariacaoId;
            NomeProduto = nomeProduto;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
            ImagemUrl = imagemUrl;
        }
    }
}