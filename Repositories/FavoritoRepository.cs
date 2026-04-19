using Microsoft.EntityFrameworkCore;
using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Collections; // Verifique se a pasta é esta mesma

namespace NeaStyleOficial.Repositories
{
    public class FavoritoRepository
    {
        private readonly NeaStyleContext _context;

        // Construtor: O ASP.NET injeta o contexto aqui automaticamente
        public FavoritoRepository(NeaStyleContext context)
        {
            _context = context;
        }

        public void Criar(Favorito favorito)
        {
            _context.Favoritos.Add(favorito);
            _context.SaveChanges();
        }

        public Favorito BuscarPorClienteId(int clienteId)
        {
            return _context.Favoritos
                .Include(f => f.Itens)
                    .ThenInclude(i => i.Produto) // Dica: Inclua o produto para ver nome/preço na tela!
                .FirstOrDefault(f => f.ClienteId == clienteId);
        }

        public void RemoverItem(int favoritoId, int produtoId)
        {
            // Buscamos o conjunto de favoritos
            var favorito = _context.Favoritos
                .Include(f => f.Itens)
                .FirstOrDefault(f => f.ConjuntoProdutoId == favoritoId);

            if (favorito != null)
            {
                // Procuramos o item específico dentro da lista
                var itemToRemove = favorito.Itens.FirstOrDefault(i => i.ProdutoId == produtoId);
                
                if (itemToRemove != null)
                {
                    _context.ItensConjunto.Remove(itemToRemove); // Remove da tabela de ligação
                    _context.SaveChanges();
                }
            }
        }
    }
}