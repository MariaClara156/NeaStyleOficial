// Favorito é um tipo específico de ConjuntoProduto, usado para armazenar os itens que o cliente marcou como favoritos
namespace NeaStyleOficial.Models.Collections
{
    public class Favorito : ConjuntoProduto
    {
        // Não precisa de propriedades adicionais — herda tudo de ConjuntoProduto (relação com cliente e produtos)
        public Favorito() { }

        public Favorito(long clienteId) : base(clienteId) { }
    }
}