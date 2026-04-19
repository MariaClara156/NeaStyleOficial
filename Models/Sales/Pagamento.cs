using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Models.Users;

namespace NeaStyleOficial.Models.Sales
{
    public class Pagamento
    {
        public long PagamentoId { get; set; }   
        public long PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public MetodoPagamento MetodoPagamento { get; set; }
        public StatusPagamento StatusPagamento { get; set; }
        public decimal ValorPago { get; set; }

        public Pagamento() { }
        public Pagamento(long pedidoId, MetodoPagamento metodoPagamento, StatusPagamento statusPagamento, decimal valorPago)
        {
            PedidoId = pedidoId;
            MetodoPagamento = metodoPagamento;
            StatusPagamento = statusPagamento;
            ValorPago = valorPago;
        }
    }
}
