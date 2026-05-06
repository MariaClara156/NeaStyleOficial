using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Services;
using NeaStyleOficial.ViewModels;

namespace NeaStyleOficial.Controllers
{
    public class ProdutoVariacaoController : Controller
    {
        private readonly ProdutoService _service;
        
        public ProdutoVariacaoController(ProdutoService service)
        {
            _service = service;
        }
        public IActionResult Criar(long produtoId)
        {
            var variacao = new ProdutoVariacao
            {
                ProdutoId = produtoId
            };

            return View(variacao);
        }

        public IActionResult Index()
        {
            var produtos = _service.BuscarTodos();
            return View(produtos);
        }

        [HttpPost]
        public IActionResult Criar(ProdutoVariacao variacao)
        {
            if (ModelState.IsValid)
            {
                _service.CadastrarVariacao(variacao);
                return RedirectToAction("Index", "Produto");
            }

            return View(variacao);
        }
    }
}           
