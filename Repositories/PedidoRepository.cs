using Microsoft.EntityFrameworkCore;
using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Models.Users;

namespace NeaStyleOficial.Repositories
{
    public class PedidoRepository
    {
        private readonly NeaStyleContext _context;
        public PedidoRepository(NeaStyleContext context)
        {
            _context = context;
        }

        // CREATE - adiciona novo pedido no banco
        public void Criar(Pedido pedido)
        {
                _context.Pedidos.Add(pedido);
                _context.SaveChanges();
        }

        // READ - busca todos os pedidos ja feitos
        public List<Pedido> BuscarTodos()
        {
                return _context.Pedidos.ToList();
        }

        // READ - busca pedido por ID
        public Pedido BuscarPorId(long pedidoId)
        {
                return _context.Pedidos.Find(pedidoId);
        }

        public List<Pedido> BuscarPorClienteId(long clienteId)
        {    
                return _context.Pedidos.Where(p => p.ClienteId == clienteId).ToList();
        }

        public void Atualizar(long pedidoId, StatusPedido novoStatus)
        {
                var pedido = _context.Pedidos.Find(pedidoId);
                if (pedido != null)
                {
                    pedido.Status = novoStatus;
                    _context.SaveChanges();
                }
        }

        public void CancelarPedido(long pedidoId)
        {
                var pedido = _context.Pedidos.Find(pedidoId); 
                if (pedido != null)
                {
                    pedido.Status = StatusPedido.Cancelado;
                    _context.SaveChanges();
                }
        }
    }
}