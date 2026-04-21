using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Users;
namespace NeaStyleOficial.Models.Collections
{
    public class ItemConjunto
    {
        public long ItemConjuntoId { get; set; }
        public long ProdutoId { get; set; }
        public Produto Produto { get; set; }  // ← Relacionamento com Produto
        public int Quantidade { get; set; }
        
        // Chave estrangeira para Carrinho ou Favorito
        public long ConjuntoProdutoId { get; set; }
        public ConjuntoProduto Conjunto { get; set; }  // ← Relacionamento com ConjuntoProduto  

        public ItemConjunto() { }
        public ItemConjunto(long produtoId, int quantidade)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
        }
    }
}