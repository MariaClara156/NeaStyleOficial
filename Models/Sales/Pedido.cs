using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Models.Users;

namespace NeaStyleOficial.Models.Sales
{
    public class Pedido
    {
        public long PedidoId { get; set; }
        public long ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; } // decimal é melhor que double pra dinheiro!
        public StatusPedido Status { get; set; }
        
        // Lista de itens do pedido
        public List<ItemConjunto> Itens { get; set; } = new();
        
        public List<Pagamento> Pagamentos { get; set; } = new();
        public List<Reembolso> Reembolsos { get; set; } = new();


        public Pedido() { }
        public Pedido(long clienteId, DateTime dataPedido, decimal valorTotal, StatusPedido status)
        {
            ClienteId = clienteId;
            DataPedido = dataPedido;
            ValorTotal = valorTotal;
            Status = status;
        }
    }
}
