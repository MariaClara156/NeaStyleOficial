using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index(string busca = null)
        {
            var produtos = _service.BuscarTodos();
            return View(produtos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Criar(Produto produto)
        {
            try
            {
                _service.CadastrarProduto(produto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(produto);
            }
        }

        public IActionResult Editar(long ProdutoId)
        {
            var produto = _service.BuscarPorId(ProdutoId);
            return View(produto);
        }

        public IActionResult Editar(Produto produto)
        {
            try
            {
                _service.Atualizar(produto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(produto);
            }
        }

        public IActionResult Deletar(long ProdutoId)
        {
            _service.Deletar(ProdutoId);
            return RedirectToAction("Index");
        }
    }
}
