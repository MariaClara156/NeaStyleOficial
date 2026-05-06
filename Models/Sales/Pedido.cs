using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Models.Users;

namespace NeaStyleOficial.Models.Sales
{
    public class Pedido
    {
        public long PedidoId { get; set; }
        // Um pedido pertence a um cliente
        public long ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; }
        // ENUM para status do pedido
        public StatusPedido Status { get; set; }
        // Um pedido pode ter vários itens (conjuntos) e pagamentos
        public List<Pagamento> Pagamentos { get; set; } = new();
        public List<Reembolso> Reembolsos { get; set; } = new();
        public List<ItemPedido> Itens { get; set; } = new();

        protected Pedido() { }
        public Pedido(long clienteId, DateTime dataPedido, decimal valorTotal, StatusPedido status)
        {
            if (valorTotal < 0)
                throw new ArgumentException("Valor total do pedido não pode ser negativo");

            ClienteId = clienteId;
            DataPedido = dataPedido;
            ValorTotal = valorTotal;
            Status = status;
        }
    }
}
