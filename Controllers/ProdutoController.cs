using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Services;
using NeaStyleOficial.ViewModels;

namespace NeaStyleOficial.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoService _service;

        // ASP.NET injeta o service automaticamente
        public ProdutoController(ProdutoService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var produtos = _service.BuscarTodos();
            return View(produtos);
        }
        public IActionResult Detalhes(long produtoId)
        {
            var produto = _service.BuscarPorId(produtoId);
            var variacoes = _service.BuscarVariacaoPorId(produtoId);
            var estoque = _service.CalcularEstoqueTotal(produtoId);

            var vm = new DetalheProdutoViewModel
            {
                Produto = produto,
                Variacoes = produto.Variacoes,
                EstoqueTotal = estoque
            };

            return View(vm);
        }
        //FILTRAR//
        public IActionResult Filtrar(string? nome, TamanhoProduto? tamanho, CorProduto? cor, TipoProduto? tipo, CategoriaProduto? categoria)
        {
            var produtos = _service.Filtrar(nome, tamanho, cor, tipo, categoria);
            return View("Index", produtos);
        }
    }
}           
