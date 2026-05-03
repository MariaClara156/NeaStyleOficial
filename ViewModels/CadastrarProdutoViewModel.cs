using NeaStyleOficial.Models.Catalog;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NeaStyleOficial.ViewModels
{
    public class CadastrarProdutoViewModel
    {
    // Dados do Produto
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public decimal PrecoCusto { get; set; }
        public string Descricao { get; set; }
        public string ImagemUrl { get; set; }

        public List<SelectListItem> Categorias { get; set; }
        public List<SelectListItem> Tamanhos { get; set; }
        public List<SelectListItem> Cores { get; set; }
        public List<SelectListItem> Tipos { get; set; }
    }
}