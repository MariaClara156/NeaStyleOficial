using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Models.Collections;

namespace NeaStyleOficial.Models.Collections
{
    public abstract class ConjuntoProduto
    {
        public long ConjuntoProdutoId { get; set; }
        public long ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        
        // Relacionamento com os itens (Produtos)
        public List<ItemConjunto> Itens { get; set; } = new();
        
        public ConjuntoProduto() { }
        public ConjuntoProduto(long clienteId)
        {
            ClienteId = clienteId;
        }
    }
}
