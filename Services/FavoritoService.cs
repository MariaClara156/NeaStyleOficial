using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Repositories;

namespace NeaStyleOficial.Services
{
    public class FavoritoService
    {
        private readonly FavoritoRepository _favoritoRepo;

        public FavoritoService(FavoritoRepository favoritoRepo)
        {
            _favoritoRepo = favoritoRepo;
        }

        public void AdicionarFavorito(long clienteId, Produto produto)
        {
            if (produto == null)
                throw new Exception("Produto inválido!");

            // Busca o conjunto de favoritos do cliente para validar
            var favoritos = _favoritoRepo.BuscarPorClienteId(clienteId);
            if (favoritos == null)
            {
                favoritos = new Favorito(clienteId);
                _favoritoRepo.Criar(favoritos);
            }
            // Regra de Negócio: Limite de 100 favoritos
            if (favoritos.Itens.Count >= 100)
                throw new Exception("Limite de 100 produtos nos favoritos atingido!");

            // Verifica se já está favoritado para não duplicar
            if (favoritos.Itens.Any(i => i.ProdutoId == produto.ProdutoId))
                return; // Se já existe, não faz nada (ou avisa o usuário)

            favoritos.Itens.Add(new ItemConjunto { 
                ProdutoId = produto.ProdutoId, 
                Quantidade = 1 // Sempre 1 para favoritos
            });

            _favoritoRepo.Atualizar(favoritos);
        }

        public List<ItemConjunto> VerFavoritos(long clienteId)
        {
            var favoritos = _favoritoRepo.BuscarPorClienteId(clienteId);
            return favoritos.Itens;
        }

        public void RemoverFavorito(long clienteId, long produtoId)
        {
            _favoritoRepo.RemoverItem(clienteId, produtoId);
        }
    }
}