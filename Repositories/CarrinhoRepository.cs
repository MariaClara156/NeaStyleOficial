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

        public Carrinho BuscarPorClienteId(long clienteId)
        {
        // Inclui os itens do carrinho, as variações dos produtos e os próprios produtos, pra ter todas as informações necessárias pra exibir o carrinho pro cliente
            return _context.Carrinhos
                .Include(f => f.Itens)
                    .ThenInclude(i => i.ProdutoVariacao)
                        .ThenInclude(v => v.Produto)
                .FirstOrDefault(f => f.ClienteId == clienteId);
        }

        public void CalcularTotal(Carrinho carrinho)
        {
            decimal total = 0;
            foreach (var item in carrinho.Itens)
            {
                var variacao = _context.ProdutoVariacoes
                    .Include(c => c.Itens)
                    .ThenInclude(i => i.ProdutoVariacao)
                    .FirstOrDefault(v => v.ProdutoVariacaoId == item.ProdutoVariacaoId);
                if (variacao != null)
                {
                    total += variacao.Preco * item.Quantidade;
                }
            }
            carrinho.Total = total;
            _context.Carrinhos.Update(carrinho);
            _context.SaveChanges();
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