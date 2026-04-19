using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Users;

namespace NeaStyleOficial.Models.Collections
{
    public class Favorito : ConjuntoProduto
    {
        
        public Favorito() { }
        public Favorito(long clienteId) : base(clienteId)
        {
            
        }
    }
}
