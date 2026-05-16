using NeaStyleOficial.Models.Catalog;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NeaStyleOficial.ViewModels.Catalog
{
    public class AtualizarVariacaoViewModel
    {
        public long VariacaoId { get; set; }
        public int Estoque { get; set; }
        public string? ImagemUrl { get; set; }
        public decimal Preco { get; set; }
        public decimal PrecoCusto { get; set; }
        public TamanhoProduto Tamanho { get; set; }
        public CorProduto Cor { get; set; }

        public long ProdutoId { get; set; }
    }
}