using NeaStyleOficial.Models.Catalog;

namespace NeaStyleOficial.ViewModels.Catalog
{
    public class CatalogoProdutoViewModel
    {
        public long ProdutoId { get; set; }
        public string? ImagemUrl { get; set; } // String para a URL da foto principal
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<ProdutoVariacao> Variacoes { get; set; } = new();
        public CategoriaProduto Categoria  { get; set; }
        public decimal MenorPreco { get; set; }
        
    }
}