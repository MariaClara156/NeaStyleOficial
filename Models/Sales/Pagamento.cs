using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Collections;

namespace NeaStyleOficial.Models.Sales
{
    public class Pagamento
    {
        public long PagamentoId { get; set; }
        // Um pagamento pertence a um pedido
        public long PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public decimal ValorPago { get; set; }
        // Para pagamentos parcelados, indica o número de parcelas (1 para pagamento à vista)
        public int Parcelas { get; set; }
        // ENUM para método de pagamento (ex: Cartão de Crédito, Boleto, Pix)
        public MetodoPagamento MetodoPagamento { get; set; }
        public StatusPagamento StatusPagamento { get; set; }

        protected Pagamento() { }
        public Pagamento(long pedidoId, decimal valorPago, int parcelas, MetodoPagamento metodoPagamento, StatusPagamento statusPagamento)
        {
                if (valorPago < 0)
                    throw new ArgumentException("Valor pago não pode ser negativo");
                if(metodoPagamento == MetodoPagamento.CartaoCredito && parcelas < 1)
                    throw new ArgumentException("Número de parcelas deve ser pelo menos 1 para cartão de crédito");
                if(metodoPagamento != MetodoPagamento.CartaoCredito && parcelas != 1)
                    throw new ArgumentException("Número de parcelas deve ser 1 para métodos de pagamento que não sejam cartão de crédito");
            PedidoId = pedidoId;
            ValorPago = valorPago;
            Parcelas = parcelas;
            MetodoPagamento = metodoPagamento;
            StatusPagamento = statusPagamento;
        }
    }
}
