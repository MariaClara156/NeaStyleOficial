namespace NeaStyleOficial.ViewModels.Collections
{
public class ItemCarrinhoViewModel
    {
        public long ItemId { get; set; }
        public long    ProdutoVariacaoId { get; set; }
        public string NomeProduto { get; set; }
        public string Variacao { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public string? ImagemUrl     { get; set; }
    }
}