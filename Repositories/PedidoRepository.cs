using Microsoft.EntityFrameworkCore;
using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Sales;

// ALTERADO: removidos usings de Catalog e Users — não utilizados diretamente nesta classe
namespace NeaStyleOficial.Repositories
{
    public class PedidoRepository
    {
        private readonly NeaStyleContext _context;

        public PedidoRepository(NeaStyleContext context)
        {
            _context = context;
        }

        // CREATE — adiciona novo pedido no banco
        public void Criar(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
        }

        // READ — busca todos os pedidos realizados
        public List<Pedido> BuscarTodos()
        {
            return _context.Pedidos
                .Include(p => p.Itens)
                .Include(p => p.Cliente)
                .ToList();
        }

        // READ — busca pedido por ID
        public Pedido BuscarPorId(long pedidoId)
        {
            return _context.Pedidos
                .Include(p => p.Itens)
                .Include(p => p.Cliente)
                .FirstOrDefault(p => p.PedidoId == pedidoId);
        }

        // READ — busca pedidos por ClienteId
        public List<Pedido> BuscarPorClienteId(long clienteId)
        {
            return _context.Pedidos
                .Include(p => p.Itens)
                .Where(p => p.ClienteId == clienteId)
                .ToList();
        }

        // UPDATE — atualiza o status do pedido
        public void Atualizar(long pedidoId, StatusPedido novoStatus)
        {
            var pedido = _context.Pedidos.Find(pedidoId);

            if (pedido != null)
            {
                pedido.Status = novoStatus;
                _context.SaveChanges();
            }
        }
    }
}