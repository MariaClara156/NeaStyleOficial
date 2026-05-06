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
        public IActionResult DetalheProduto(long id)
        {
            var produto = _service.BuscarPorId(id);
            if (produto == null) return NotFound();
            
            var viewModel = new DetalheProdutoViewModel
            {
                Produto = produto,
                Variacoes = produto.Variacoes,
                EstoqueTotal = produto.Variacoes.Sum(v => v.Estoque)
            };
            
            return View(viewModel);
        }
        //FILTRAR//
        public IActionResult Filtrar(string? nome, TamanhoProduto? tamanho, CorProduto? cor, TipoProduto? tipo, CategoriaProduto? categoria)
        {
            var produtos = _service.Filtrar(nome, tamanho, cor, tipo, categoria);
            return View("Index", produtos);
        }

        public IActionResult Feminino()
        {
            var produtos = _service.Filtrar(null, null, null, null, CategoriaProduto.Feminino);
            return View(produtos);
        }
        public IActionResult Masculino()
        {
            var produtos = _service.Filtrar(null, null, null, null, CategoriaProduto.Masculino);
            return View(produtos);
        }
        public IActionResult Unissex()
        {
            var produtos = _service.Filtrar(null, null, null, null, CategoriaProduto.Unissex);
            return View(produtos);
        }
    }
}           
