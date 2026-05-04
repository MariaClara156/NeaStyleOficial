using NeaStyleOficial.Models.Catalog;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NeaStyleOficial.ViewModels
{
    public class CadastrarProdutoViewModel
    {
    // Dados do Produto
        public long ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public decimal PrecoCusto { get; set; }
        public string Descricao { get; set; }
        public string ImagemUrl { get; set; }

        public CategoriaProduto Categoria { get; set; }
        public TipoProduto Tipo { get; set; }
    }
}