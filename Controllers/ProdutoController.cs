using Microsoft.AspNetCore.Mvc;
using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Services;

namespace NeaStyleOficial.Controllers
{
    public class ProdutoController : Controller
    {
        private ProdutoService _service = new ProdutoService();

        public IActionResult Index()
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

        public IActionResult Editar(long id)
        {
            var produto = _service.BuscarPorId(id);
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

        public IActionResult Deletar(long id)
        {
            _service.Deletar(id);
            return RedirectToAction("Index");
        }
    }
}
