using Microsoft.AspNetCore.Mvc;
using NeaStyleOficial.Services;

namespace NeaStyleOficial.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProdutoService _produtoService;

        public HomeController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        public IActionResult Index()
        {
            var produtos = _produtoService.ObterCatalogo();
            return View(produtos);
        }
    }
}