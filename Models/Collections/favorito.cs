using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Users;
// Favorito é um tipo específico de ConjuntoProduto, usado para armazenar os itens que o cliente marcou como favoritos
namespace NeaStyleOficial.Models.Collections
{
    public class Favorito : ConjuntoProduto
    {
        // Não precisa de propriedades para favoritos, pois herda tudo de ConjuntoProduto, que já tem a relação com o cliente e os produtos
        protected Favorito() { }
        public Favorito(long clienteId) : base(clienteId) { }
    }
}
