using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Models.Collections;
// ConjuntoProduto é a classe base para Carrinho, Favoritos e Conjuntos personalizados
namespace NeaStyleOficial.Models.Collections
{
    public abstract class ConjuntoProduto
    {
        public long ConjuntoProdutoId { get; set; }
        // Relacionamento com o cliente (usuário) que possui o conjunto
        public long ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        // Relacionamento com os itens (Produtos)
        public List<ItemConjunto> Itens { get; set; } = new();
        
        protected ConjuntoProduto() { }
        public ConjuntoProduto(long clienteId)
        {
            ClienteId = clienteId;
        }
    }
}
