using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Users;
// Carrinho é um tipo específico de ConjuntoProduto, usado para armazenar os itens que o cliente deseja comprar
namespace NeaStyleOficial.Models.Collections
{
    public class Carrinho : ConjuntoProduto
    {
        // Propriedade para indicar se o carrinho foi finalizado (pedido realizado) ou ainda está em aberto
        public bool Finalizado { get; set; }
        
        protected Carrinho() { }
        public Carrinho(long clienteId) : base(clienteId)
        {
            Finalizado = false;
        }
    }
}
