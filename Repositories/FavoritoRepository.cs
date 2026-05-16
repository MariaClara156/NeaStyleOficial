using Microsoft.EntityFrameworkCore;
using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Collections;

// ALTERADO: removido 'using NeaStyleOficial.Models.Catalog' — não utilizado diretamente nesta classe
namespace NeaStyleOficial.Repositories
{
    public class FavoritoRepository
    {
        private readonly NeaStyleContext _context;

        public FavoritoRepository(NeaStyleContext context)
        {
            _context = context;
        }

        public void Criar(Favorito favorito)
        {
            _context.Favoritos.Add(favorito);
            _context.SaveChanges();
        }

        public Favorito? BuscarPorClienteId(long clienteId)
        {
            return _context.Favoritos
                .Include(f => f.Itens)
                    .ThenInclude(i => i.ProdutoVariacao)
                        .ThenInclude(v => v.Produto)
                .FirstOrDefault(f => f.ClienteId == clienteId);
        }

        public void AdicionarItem(ItemConjunto item)
        {
            _context.ItensConjunto.Add(item);
            _context.SaveChanges();
        }

        public void RemoverItem(long clienteId, long produtoVariacaoId)
        {
            var favorito = _context.Favoritos
                .Include(f => f.Itens)
                .FirstOrDefault(f => f.ClienteId == clienteId);

            if (favorito != null)
            {
                var item = favorito.Itens.FirstOrDefault(i => i.ProdutoVariacaoId == produtoVariacaoId);

                if (item != null)
                {
                    _context.ItensConjunto.Remove(item);
                    _context.SaveChanges();
                }
            }
        }

        public void Atualizar(Favorito favorito)
        {
            _context.Favoritos.Update(favorito);
            _context.SaveChanges();
        }
    }
}