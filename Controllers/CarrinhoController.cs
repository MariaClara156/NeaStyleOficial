using Microsoft.AspNetCore.Mvc;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Services;

namespace NeaStyleOficial.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly CarrinhoService _carrinhoService;
        private readonly ProdutoService _produtoService;

        public CarrinhoController(CarrinhoService carrinhoService, ProdutoService produtoService)
        {
            _carrinhoService = carrinhoService;
            _produtoService = produtoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        /* CRUD Carrinho */
        /* GET */
        public IActionResult GerenciarCarrinho(long clienteId)
        {
            var itens = _carrinhoService.ObterItens(clienteId);
            return View(itens);
        }
        /* POST */
        [HttpPost]
        public IActionResult AdicionarAoCarrinho(Produto produto, int quantidade)
        {
            try
            {
                // Simulando clienteId fixo para teste
                long clienteId = 1; 
                _carrinhoService.AdicionarProduto(clienteId, produto, quantidade);
                return RedirectToAction("GerenciarCarrinho", new { clienteId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(produto);
            }
        }
        [HttpPost]
        public IActionResult RemoverProduto(long clienteId, long produtoId)
        {
            try
            {
                _carrinhoService.RemoverProduto(clienteId, produtoId);
                return RedirectToAction("GerenciarCarrinho", new { clienteId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("GerenciarCarrinho", new { clienteId });
            }
        }
        public IActionResult VisualizarCarrinho(long clienteId)
        {
            var itens = _carrinhoService.ObterItens(clienteId);
            return View(itens);
        }
        public IActionResult LimparCarrinho(long clienteId)
        {
            _carrinhoService.LimparCarrinho(clienteId);
            return RedirectToAction("Index");
        }
        
        public IActionResult CalcularTotal(long clienteId)
        {
            var total = _carrinhoService.CalcularTotal(clienteId);
            return View(total);
        }
    }
}