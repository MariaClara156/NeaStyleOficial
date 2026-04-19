using Microsoft.EntityFrameworkCore;
using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Collections; // Verifique se a pasta é esta mesma

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

        public Carrinho BuscarPorClienteId(int clienteId)
        {
            return _context.Carrinhos
                .Include(f => f.Itens)
                    .ThenInclude(i => i.Produto) // Dica: Inclua o produto para ver nome/preço na tela!
                .FirstOrDefault(f => f.ClienteId == clienteId);
        }

        public void FinalizarCompra(int carrinhoId)
        {
            var carrinho = _context.Carrinhos
                .Include(f => f.Itens)
                .FirstOrDefault(f => f.ConjuntoProdutoId == carrinhoId);

            if (carrinho != null)
            {
                // Aqui você pode implementar a lógica para criar um pedido, processar pagamento, etc.
                // Depois de finalizar, você pode limpar o carrinho ou marcar como finalizado.
                carrinho.Finalizado = true; // Supondo que você tenha essa propriedade para marcar o carrinho como finalizado
                _context.SaveChanges();
            }
        }
    }
}