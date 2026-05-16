using NeaStyleOficial.Models.Catalog;

namespace NeaStyleOficial.ViewModels.Catalog
{
    public class DetalheProdutoViewModel
    {
        public Produto Produto { get; set; }
        public List<ProdutoVariacao> Variacoes { get; set; }
        public int QuantidadeVariacao { get; set;}
        public int EstoqueTotal { get; set; }
    }
}