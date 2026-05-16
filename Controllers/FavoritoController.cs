using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using NeaStyleOficial.Services;
using NeaStyleOficial.ViewModels.Collections;

namespace NeaStyleOficial.Controllers
{
    public class FavoritoController : Controller
    {
        private readonly FavoritoService _favoritoService;
        private readonly ProdutoService  _produtoService;

        public FavoritoController(FavoritoService favoritoService, ProdutoService produtoService)
        {
            _favoritoService = favoritoService;
            _produtoService  = produtoService;
        }

        // GET — lista de favoritos do cliente
        public IActionResult Index()
        {
            var clienteId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var itens     = _favoritoService.VerFavoritos(clienteId);

            var viewModel = itens.Select(i => new FavoritoViewModel
            {
                ProdutoId   = i.ProdutoVariacao.ProdutoId,
                VariacaoId  = i.ProdutoVariacaoId,
                NomeProduto = i.ProdutoVariacao.Produto.Nome,
                ImagemUrl   = i.ProdutoVariacao.ImagemUrl,
                Preco       = i.ProdutoVariacao.Preco
            }).ToList();

            return View(viewModel);
        }

        // GET — adiciona primeira variação com estoque aos favoritos
        public IActionResult AdicionarFavorito(long produtoId)
        {
            try
            {
                var clienteId       = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var produto         = _produtoService.BuscarPorId(produtoId);
                var primeiraVariacao = produto.Variacoes.FirstOrDefault(v => v.Estoque > 0);

                if (primeiraVariacao == null)
                    throw new Exception("Produto sem estoque disponível!");

                _favoritoService.AdicionarFavorito(clienteId, primeiraVariacao);
                return Json(new { sucesso = true });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }

        // POST — remove item dos favoritos
        [HttpPost]
        public IActionResult RemoverFavorito([FromBody] RemoverFavoritoRequest request)
        {
            try
            {
                var clienteId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _favoritoService.RemoverFavorito(clienteId, request.ProdutoVariacaoId);
                return Json(new { sucesso = true });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }
    }

    // DTO para deserialização do body da requisição de remoção
    public class RemoverFavoritoRequest
    {
        public long ProdutoVariacaoId { get; set; }
    }
}