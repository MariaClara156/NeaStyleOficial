using Microsoft.EntityFrameworkCore;
using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Models.Users;

namespace NeaStyleOficial.Repositories
{
    public class PedidoRepository
    {
        // CREATE - adiciona novo pedido no banco
        public void Criar(Pedido pedido)
        {
            using (var context = new NeaStyleContext())
            {
                context.Pedidos.Add(pedido);
                context.SaveChanges(); // confirma a operação no banco
            }
        }

        // READ - busca todos os pedidos ja feitos
        public List<Pedido> BuscarTodos()
        {
            using (var context = new NeaStyleContext())
            {
                return context.Pedidos.ToList();
            }
        }

        // READ - busca pedido por ID
        public Pedido BuscarPorId(long id)
        {
            using (var context = new NeaStyleContext())
            {
                return context.Pedidos.Find(id);
            }
        }

        public List<Pedido> BuscarPorClienteID(long ClienteID)
        {
            using (var context = new NeaStyleContext())
            {
                return context.Pedidos.Where(p => p.ClienteID == ClienteID).ToList();
            }
        }

        public void AtualizarStatusPedido(long pedidoId, StatusPedido novoStatus)
        {
            using (var context = new NeaStyleContext())
            {
                var pedido = context.Pedidos.Find(pedidoId); // minúsculo!
                if (pedido != null)
                {
                    pedido.Status = novoStatus;
                    context.SaveChanges();
                }
            }
        }

        public void CancelarPedido(long pedidoId)
        {
            using (var context = new NeaStyleContext())
            {
                var pedido = context.Pedidos.Find(pedidoId); 
                if (pedido != null)
                {
                    pedido.Status = StatusPedido.Cancelado;
                    context.SaveChanges();
                }
            }
        }
    }
}