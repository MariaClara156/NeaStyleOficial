using NeaStyleOficial.Models.Catalog;

// ItemConjunto representa um item específico dentro de um conjunto (Carrinho, Favoritos ou Conjunto personalizado)
namespace NeaStyleOficial.Models.Collections
{
    public class ItemConjunto
    {
        public long           ProdutoVariacaoId { get; set; }
        public ProdutoVariacao ProdutoVariacao  { get; set; }
        public long           ItemConjuntoId    { get; set; }
        public int            Quantidade        { get; set; }

        public long?    CarrinhoId { get; set; }
        public Carrinho Carrinho   { get; set; }
        public long?    FavoritoId { get; set; }
        public Favorito Favorito   { get; set; }

        public ItemConjunto() { }

        public ItemConjunto(long produtoVariacaoId, int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("Quantidade inválida");

            ProdutoVariacaoId = produtoVariacaoId;
            Quantidade        = quantidade;
        }
    }
}