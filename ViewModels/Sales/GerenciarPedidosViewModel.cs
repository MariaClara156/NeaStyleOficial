using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Models.Users;

namespace NeaStyleOficial.ViewModels.Sales
{
    public class GerenciarPedidosViewModel
    {
        public long PedidoId { get; set; }
        public long ClienteId { get; set; }
        public string NomeCliente { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public StatusPedido Status { get; set; }
        public List<ItemPedido> Itens { get; set; }
    }
}