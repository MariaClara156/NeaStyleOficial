using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Users;
// ItemConjunto representa um item específico dentro de um conjunto (Carrinho, Favoritos ou Conjunto personalizado)
namespace NeaStyleOficial.Models.Collections
{
    public class ItemConjunto
    {
        public long ItemConjuntoId { get; set; }
        public long ProdutoVariacaoId { get; set; }
        public ProdutoVariacao ProdutoVariacao { get; set; }
        public int Quantidade { get; set; }
        // Chave estrangeira para Carrinho ou Favorito
        public long ConjuntoProdutoId { get; set; }
        public ConjuntoProduto Conjunto { get; set; } 

        protected ItemConjunto() { }
        public ItemConjunto(long produtoVariacaoId, int quantidade)
        {
            if (quantidade <= 0)
            throw new ArgumentException("Quantidade inválida");

            ProdutoVariacaoId = produtoVariacaoId;
            Quantidade = quantidade;
        }
    }
}