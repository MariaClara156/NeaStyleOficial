using Microsoft.EntityFrameworkCore;
using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Collections;

namespace NeaStyleOficial.Repositories
{
    public class CarrinhoRepository
    {
        private readonly NeaStyleContext _context;

        // Construtor: O ASP.NET injeta o contexto aqui automaticamente
        public CarrinhoRepository(NeaStyleContext context)
        {
            _context = context;
        }

        public void Criar(Carrinho carrinho)
        {
            _context.Carrinhos.Add(carrinho);
            _context.SaveChanges();
        }

        public Carrinho BuscarPorClienteId(long clienteId)
        {
            return _context.Carrinhos
                .Include(f => f.Itens)
                    .ThenInclude(i => i.Produto)
                .FirstOrDefault(f => f.ClienteId == clienteId);
        }

        public void FinalizarCompra(long carrinhoId)
        {
            var carrinho = _context.Carrinhos
                .Include(f => f.Itens)
                .FirstOrDefault(f => f.ConjuntoProdutoId == carrinhoId);

            if (carrinho != null)
            {
                carrinho.Finalizado = true;
                _context.SaveChanges();
            }
        }

        public void Atualizar(Carrinho carrinho)
        {
            _context.Carrinhos.Update(carrinho);
            _context.SaveChanges();
        }

        public void Limpar(long carrinhoId)
        {
            var carrinho = _context.Carrinhos
                .Include(f => f.Itens)
                .FirstOrDefault(f => f.ConjuntoProdutoId == carrinhoId);

            if (carrinho != null)
            {
                carrinho.Itens.Clear();
                _context.SaveChanges();
            }
        }
    }
}