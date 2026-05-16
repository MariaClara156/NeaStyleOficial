using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using NeaStyleOficial.Services;
using NeaStyleOficial.ViewModels.Collections;

namespace NeaStyleOficial.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly CarrinhoService _carrinhoService;
        private readonly ProdutoService  _produtoService;

        public CarrinhoController(CarrinhoService carrinhoService, ProdutoService produtoService)
        {
            _carrinhoService = carrinhoService;
            _produtoService  = produtoService;
        }

        // GET — exibe o carrinho
        public IActionResult Index()
        {
            var clienteId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var itens     = _carrinhoService.ObterItens(clienteId);

            var itensViewModel = itens.Select(i => new ItemCarrinhoViewModel
            {
                ItemId        = i.ItemConjuntoId,
                ProdutoVariacaoId = i.ProdutoVariacaoId,
                NomeProduto   = i.ProdutoVariacao.Produto.Nome,
                Variacao      = $"{i.ProdutoVariacao.Cor} - {i.ProdutoVariacao.Tamanho}",
                Quantidade    = i.Quantidade,
                PrecoUnitario = i.ProdutoVariacao.Preco,
                Subtotal      = i.Quantidade * i.ProdutoVariacao.Preco,
                ImagemUrl     = i.ProdutoVariacao.ImagemUrl
            }).ToList();

            var viewModel = new CarrinhoViewModel
            {
                Itens = itensViewModel,
                Total = itensViewModel.Sum(i => i.Subtotal)
            };

            return View(viewModel);
        }

        // POST — adiciona item (chamado pela página de DetalheProduto)
        [HttpPost]
        public IActionResult AdicionarAoCarrinho(long produtoVariacaoId, int quantidade)
        {
            try
            {
                var clienteId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var variacao  = _produtoService.BuscarVariacaoPorId(produtoVariacaoId);

                _carrinhoService.AdicionarProduto(clienteId, variacao, quantidade);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Index", "Produto");
            }
        }

        // POST — remove item do carrinho
        [HttpPost]
        public IActionResult RemoverProduto(long produtoVariacaoId)
        {
            try
            {
                var clienteId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _carrinhoService.RemoverProduto(clienteId, produtoVariacaoId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST — limpa o carrinho
        [HttpPost]
        public IActionResult LimparCarrinho()
        {
            try
            {
                var clienteId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _carrinhoService.LimparCarrinho(clienteId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}