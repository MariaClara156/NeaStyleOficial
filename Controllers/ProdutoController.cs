using Microsoft.AspNetCore.Mvc;
using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Services;
using NeaStyleOficial.ViewModels.Catalog;

namespace NeaStyleOficial.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoService _service;

        public ProdutoController(ProdutoService service)
        {
            _service = service;
        }

        // READ — catálogo completo
        public IActionResult Index() => View(_service.ObterCatalogo());

        // READ — detalhe de um produto
        public IActionResult DetalheProduto(long produtoId)
        {
            try
            {
                var produto = _service.BuscarPorId(produtoId);
                if (produto == null) return NotFound();

                var viewModel = new DetalheProdutoViewModel
                {
                    Produto      = produto,
                    Variacoes    = produto.Variacoes,
                    EstoqueTotal = produto.Variacoes.Sum(v => v.Estoque)
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return Content($"Erro: {ex.Message} | Inner: {ex.InnerException?.Message}");
            }
        }

        // READ — filtra produtos por parâmetros opcionais
        public IActionResult Filtrar(string? nome, TamanhoProduto? tamanho, CorProduto? cor, TipoProduto? tipo, CategoriaProduto? categoria)
        {
            var produtos = _service.Filtrar(nome, tamanho, cor, tipo, categoria);
            return View("Index", produtos);
        }

        // READ — filtros fixos por categoria
        public IActionResult Feminino()
        {
            var produtos = _service.Filtrar(null, null, null, null, CategoriaProduto.Feminino);
            var viewModel = produtos.Select(p => new CatalogoProdutoViewModel
            {
                ProdutoId  = p.ProdutoId,
                Nome       = p.Nome,
                Descricao  = p.Descricao,
                Categoria  = p.Categoria,
                MenorPreco = p.Variacoes.Where(v => v.Estoque > 0).OrderBy(v => v.Preco).FirstOrDefault()?.Preco ?? 0,
                ImagemUrl  = p.Variacoes.FirstOrDefault(v => !string.IsNullOrEmpty(v.ImagemUrl))?.ImagemUrl
                            ?? p.Variacoes.FirstOrDefault()?.ImagemUrl,
                Variacoes  = p.Variacoes.ToList()
            }).ToList();

            return View(viewModel);
        }

        public IActionResult Masculino()
        {
            var produtos = _service.Filtrar(null, null, null, null, CategoriaProduto.Masculino);
            var viewModel = produtos.Select(p => new CatalogoProdutoViewModel
            {
                ProdutoId  = p.ProdutoId,
                Nome       = p.Nome,
                Descricao  = p.Descricao,
                Categoria  = p.Categoria,
                MenorPreco = p.Variacoes.Where(v => v.Estoque > 0).OrderBy(v => v.Preco).FirstOrDefault()?.Preco ?? 0,
                ImagemUrl  = p.Variacoes.FirstOrDefault(v => !string.IsNullOrEmpty(v.ImagemUrl))?.ImagemUrl
                            ?? p.Variacoes.FirstOrDefault()?.ImagemUrl,
                Variacoes  = p.Variacoes.ToList()
            }).ToList();

            return View(viewModel);
        }
    }
}