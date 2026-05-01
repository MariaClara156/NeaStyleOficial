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
        public IActionResult AdicionarAoCarrinho(long produtoVariacaoId, int quantidade)
        {
            try
            {
                // TODO: substituir pelo clienteId do usuário logado
                long clienteId = 1;

                // Busca a variação do produto pelo ID
                var variacao = _produtoService.BuscarVariacaoPorId(produtoVariacaoId);

                // Chama o Service corretamente
                _carrinhoService.AdicionarProduto(clienteId, variacao, quantidade);

                return RedirectToAction("Index", "Carrinho");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Index", "Produto");
            }
        }

        public IActionResult DetalheProduto(long id)
        {
            var produto = _produtoService.BuscarPorId(id);
            if (produto == null) return NotFound();
            return View(produto); // passa um produto, não uma lista!
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