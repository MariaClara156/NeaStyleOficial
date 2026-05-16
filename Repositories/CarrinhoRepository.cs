using Microsoft.EntityFrameworkCore;
using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Collections;

namespace NeaStyleOficial.Repositories
{
    public class CarrinhoRepository
    {
        private readonly NeaStyleContext _context;

        public CarrinhoRepository(NeaStyleContext context)
        {
            _context = context;
        }

        public void Criar(Carrinho carrinho)
        {
            _context.Carrinhos.Add(carrinho);
            _context.SaveChanges();
        }

        // Inclui itens, variações e produtos — necessário para exibir o carrinho completo ao cliente
        public Carrinho BuscarPorClienteId(long clienteId)
        {
            return _context.Carrinhos
                .Include(c => c.Itens)
                    .ThenInclude(i => i.ProdutoVariacao)
                        .ThenInclude(v => v.Produto)
                .FirstOrDefault(c => c.ClienteId == clienteId);
        }

        public void FinalizarCompra(long carrinhoId)
        {
            var carrinho = _context.Carrinhos
                .Include(c => c.Itens)
                .FirstOrDefault(c => c.ConjuntoProdutoId == carrinhoId);

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
                .Include(c => c.Itens)
                .FirstOrDefault(c => c.ConjuntoProdutoId == carrinhoId);

            if (carrinho != null)
            {
                carrinho.Itens.Clear();
                _context.SaveChanges();
            }
        }
    }
}