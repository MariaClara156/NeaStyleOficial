using Microsoft.AspNetCore.Mvc;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Services;
using NeaStyleOficial.ViewModels;

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

    // GET → exibe o carrinho
    public IActionResult Index()
{
    var itens = _carrinhoService.ObterItens(clienteId: 1); // TODO: pegar do usuário logado

    var itensViewModel = itens.Select(i => new ItemCarrinhoViewModel
    {
        ItemId = i.ItemConjuntoId,
        NomeProduto = i.ProdutoVariacao.Produto.Nome,
        Variacao = $"{i.ProdutoVariacao.Cor} - {i.ProdutoVariacao.Tamanho}",
        Quantidade = i.Quantidade,
        PrecoUnitario = i.ProdutoVariacao.Preco,
        Subtotal = i.Quantidade * i.ProdutoVariacao.Preco
    }).ToList();

    var viewModel = new CarrinhoViewModel
    {
        Itens = itensViewModel,
        Total = itensViewModel.Sum(i => i.Subtotal)
    };

    return View(viewModel);
}

    // POST → adiciona item (chamado pela página de DetalheProduto)
    [HttpPost]
    public IActionResult AdicionarAoCarrinho(long produtoVariacaoId, int quantidade)
    {
        try
        {
            long clienteId = 1; // TODO: pegar do usuário logado
            var variacao = _produtoService.BuscarVariacaoPorId(produtoVariacaoId);
            _carrinhoService.AdicionarProduto(clienteId, variacao, quantidade);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return RedirectToAction("Index", "Produto");
        }
    }

    // POST → remove item do carrinho
    [HttpPost]
    public IActionResult RemoverProduto(long produtoId)
    {
        long clienteId = 1; // TODO: pegar do usuário logado
        _carrinhoService.RemoverProduto(clienteId, produtoId);
        return RedirectToAction("Index");
    }

    // POST → limpa o carrinho
    [HttpPost]
    public IActionResult LimparCarrinho()
    {
        long clienteId = 1; // TODO: pegar do usuário logado
        _carrinhoService.LimparCarrinho(clienteId);
        return RedirectToAction("Index");
    }
}
}