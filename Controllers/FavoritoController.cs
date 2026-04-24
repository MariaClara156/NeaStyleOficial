using Microsoft.AspNetCore.Mvc;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Services;

namespace NeaStyleOficial.Controllers
{
    public class FavoritoController : Controller
    {
        private readonly FavoritoService _favoritoService;
        private readonly ProdutoService _produtoService;

        public FavoritoController(FavoritoService favoritoService, ProdutoService produtoService)
        {
            _favoritoService = favoritoService;
            _produtoService = produtoService;
        }
        public IActionResult Index()
        {
            return View();
        }
        /* CRUD Favorito */
        /* GET */
        public IActionResult GerenciarFavoritos(long clienteId)
        {
            var favoritos = _favoritoService.VerFavoritos(clienteId);
            return View(favoritos);
        }

        /* POST */
        [HttpPost]
        public IActionResult AdicionarFavorito(Produto produto)
        {
            try
            {
                // TODO: substituir pelo clienteId do usuário logado após implementar autenticação
                long clienteId = 1; 
                _favoritoService.AdicionarFavorito(clienteId, produto);
                return RedirectToAction("GerenciarFavoritos", new { clienteId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(produto);
            }
        }

        [HttpPost]
        public IActionResult RemoverFavorito(long clienteId, long produtoId)
        {
            try
            {
                _favoritoService.RemoverFavorito(clienteId, produtoId);
                return RedirectToAction("GerenciarFavoritos", new { clienteId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("GerenciarFavoritos", new { clienteId });
            }
        }
    }
}