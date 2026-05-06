// ItemCarrinhoViewModel.cs
namespace NeaStyleOficial.ViewModels
{
public class ItemCarrinhoViewModel
    {
        public long ItemId { get; set; }
        public string NomeProduto { get; set; }
        public string Variacao { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}