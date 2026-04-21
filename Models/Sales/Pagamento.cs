using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Collections;

namespace NeaStyleOficial.Models.Sales
{
    public class Pagamento
    {
        public long PagamentoId { get; set; }   
        public long PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public decimal ValorPago { get; set; }
        public int Parcelas { get; set; }
        public MetodoPagamento MetodoPagamento { get; set; }
        public StatusPagamento StatusPagamento { get; set; }

        public Pagamento() { }
        public Pagamento(long pedidoId, decimal valorPago, int parcelas, MetodoPagamento metodoPagamento, StatusPagamento statusPagamento)
        {
            PedidoId = pedidoId;
            ValorPago = valorPago;
            Parcelas = parcelas;
            MetodoPagamento = metodoPagamento;
            StatusPagamento = statusPagamento;
        }
    }
}
