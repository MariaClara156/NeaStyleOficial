using NeaStyleOficial.Models.Sales;

namespace NeaStyleOficial.ViewModels.Sales
{
    public class PagamentoViewModel
    {
        // Dados do pedido pra exibir na tela
        public long PedidoId { get; set; }
        public decimal ValorTotal { get; set; }
        public List<ItemPedido> ItensPedido { get; set; }

        // Dados que o cliente vai preencher
        public MetodoPagamento MetodoPagamento { get; set; }
        public int Parcelas { get; set; }
    }
}