using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Models.Users;

namespace NeaStyleOficial.Models.Sales
{
    public class Pagamento
    {
        public long PagamentoID { get; set; }   
        public long PedidoID { get; set; }
        public Pedido Pedido { get; set; }
        public MetodoPagamento MetodoPagamento { get; set; }
        public StatusPagamento StatusPagamento { get; set; }
        public MetodoPagamento Metodo { get; set; }
        public StatusPagamento Status { get; set; }
        public decimal ValorPago { get; set; }

        public Pagamento() { }
        public Pagamento(long pedidoID, MetodoPagamento metodoPagamento, StatusPagamento statusPagamento, decimal valorPago)
        {
            PedidoID = pedidoID;
            MetodoPagamento = metodoPagamento;
            StatusPagamento = statusPagamento;
            ValorPago = valorPago;
        }
    }
}
