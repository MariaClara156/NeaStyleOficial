using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Services;

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
    }
}
