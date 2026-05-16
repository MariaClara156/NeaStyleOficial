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

        public void AdicionarFavorito(long clienteId, ProdutoVariacao produtoVariacao)
        {
            if (produtoVariacao == null)
                throw new Exception("Variação inválida!");

            // Busca os favoritos do cliente; se não existirem, cria uma nova lista
            var favoritos = _favoritoRepo.BuscarPorClienteId(clienteId);
            if (favoritos == null)
            {
                favoritos = new Favorito(clienteId);
                _favoritoRepo.Criar(favoritos);
                favoritos = _favoritoRepo.BuscarPorClienteId(clienteId);
            }

            if (favoritos.Itens.Count >= 100)
                throw new Exception("Limite de 100 produtos nos favoritos atingido!");

            // Evita duplicatas
            if (favoritos.Itens.Any(i => i.ProdutoVariacaoId == produtoVariacao.ProdutoVariacaoId))
                return;

            _favoritoRepo.AdicionarItem(new ItemConjunto
            {
                ProdutoVariacaoId = produtoVariacao.ProdutoVariacaoId,
                Quantidade        = 1,
                FavoritoId        = favoritos.ConjuntoProdutoId
            });
        }

        public List<ItemConjunto> VerFavoritos(long clienteId)
        {
            var favoritos = _favoritoRepo.BuscarPorClienteId(clienteId);
            if (favoritos == null) return new List<ItemConjunto>();
            return favoritos.Itens;
        }

        public void RemoverFavorito(long clienteId, long produtoVariacaoId)
        {
            _favoritoRepo.RemoverItem(clienteId, produtoVariacaoId);
        }
    }
}