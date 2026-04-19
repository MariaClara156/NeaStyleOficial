using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Users;

namespace NeaStyleOficial.Models.Collections
{
    public class Carrinho : ConjuntoProduto
    {
        public bool Finalizado { get; set; } // Para saber se virou pedido
        
        public Carrinho() { }
        public Carrinho(long clienteId) : base(clienteId)
        {
            
        }
    }
}
