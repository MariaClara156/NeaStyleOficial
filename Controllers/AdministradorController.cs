using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Services;
using NeaStyleOficial.ViewModels.Catalog;
using NeaStyleOficial.ViewModels.Sales;
using NeaStyleOficial.ViewModels.Users;

namespace NeaStyleOficial.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdministradorController : Controller
    {
        private readonly ProdutoService  _produtoService;
        private readonly ClienteService  _clienteService;
        private readonly PedidoService   _pedidoService;

        public AdministradorController(ProdutoService produtoService, ClienteService clienteService, PedidoService pedidoService)
        {
            _produtoService = produtoService;
            _clienteService = clienteService;
            _pedidoService  = pedidoService;
        }

        public IActionResult Index()
        {
            var pedidos  = _pedidoService.VerTodosPedidos();
            var produtos = _produtoService.BuscarTodos();
            var clientes = _clienteService.BuscarTodos();

            var produtosMaisVendidos = pedidos
                .SelectMany(p => p.Itens)
                .GroupBy(i => i.NomeProduto)
                .Select(g => new ProdutoVendidoViewModel
                {
                    NomeProduto      = g.Key,
                    QuantidadeVendida = g.Sum(i => i.Quantidade),
                    TotalGerado      = g.Sum(i => i.Quantidade * i.PrecoUnitario)
                })
                .OrderByDescending(x => x.QuantidadeVendida)
                .Take(5)
                .ToList();

            var vendasPorMes = pedidos
                .GroupBy(p => new { p.DataPedido.Year, p.DataPedido.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .TakeLast(6)
                .Select(g => new VendasPeriodoViewModel
                {
                    Mes        = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM/yyyy"),
                    Total      = g.Sum(p => p.ValorTotal),
                    Quantidade = g.Count()
                })
                .ToList();

            var vm = new DashboardViewModel
            {
                TotalPedidos          = pedidos.Count(),
                TotalVendas           = pedidos.Sum(p => p.ValorTotal),
                TotalClientes         = clientes.Count(),
                TotalProdutos         = produtos.Count(),
                ProdutosMaisVendidos  = produtosMaisVendidos,
                VendasPorMes          = vendasPorMes
            };

            return View(vm);
        }
        // ===================== PRODUTOS =====================

        public IActionResult ListarProdutos()
        {
            var produtos = _produtoService.BuscarTodos();

            var viewModel = produtos.Select(p => new DetalheProdutoViewModel
            {
                Produto            = p,
                Variacoes          = p.Variacoes,
                QuantidadeVariacao = p.Variacoes.Count,
                EstoqueTotal       = p.Variacoes.Sum(v => v.Estoque)
            }).ToList();

            return View(viewModel);
        }

        public IActionResult CadastrarProdutos() => View();

        public IActionResult CadastrarVariacao(long produtoId)
        {
            var produto = _produtoService.BuscarPorId(produtoId);
            if (produto == null) return NotFound();

            var viewModel = new CadastrarVariacaoViewModel { ProdutoId = produtoId };
            return View(viewModel);
        }

        public IActionResult AtualizarVariacao(long variacaoId)
        {
            var variacao = _produtoService.BuscarVariacaoPorId(variacaoId);
            if (variacao == null) return NotFound();

            var viewModel = new AtualizarVariacaoViewModel
            {
                VariacaoId = variacao.ProdutoVariacaoId,
                Estoque    = variacao.Estoque,
                Preco      = variacao.Preco,
                PrecoCusto = variacao.PrecoCusto,
                ImagemUrl  = variacao.ImagemUrl,
                Tamanho    = variacao.Tamanho,
                Cor        = variacao.Cor
            };

            return View(viewModel);
        }

        public IActionResult DeletarVariacao(long variacaoId)
        {
            var variacao = _produtoService.BuscarVariacaoPorId(variacaoId);
            if (variacao == null) return NotFound();
            return View(variacao);
        }

        public IActionResult EditarProduto(long produtoId)
        {
            var produto = _produtoService.BuscarPorId(produtoId);
            if (produto == null) return NotFound();
            return View(produto);
        }

        public IActionResult DeletarProduto(long produtoId)
        {
            var produto = _produtoService.BuscarPorId(produtoId);
            if (produto == null) return NotFound();
            return View(produto);
        }

        // ===================== PEDIDOS =====================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AlterarStatusPedido(long pedidoId, NeaStyleOficial.Models.Sales.StatusPedido status)
        {
            _pedidoService.AlterarStatus(pedidoId, status);
            return RedirectToAction("GerenciarPedidos");
        }

        public IActionResult GerenciarPedidos()
        {
            var pedidos = _pedidoService.VerTodosPedidos();

            var viewModel = pedidos.Select(p => new GerenciarPedidosViewModel
            {
                PedidoId = p.PedidoId,
                ClienteId = p.ClienteId,
                NomeCliente = p.Cliente?.Nome ?? "Cliente não encontrado",
                DataPedido = p.DataPedido,
                ValorTotal = p.ValorTotal,
                Status = p.Status,
                Itens = p.Itens
            }).ToList();

            return View(viewModel);
        }
        public IActionResult Relatorio()         => View(_pedidoService.VerTodosPedidos());

        // ===================== CLIENTES =====================

        public IActionResult GerenciarClientes() => View(_clienteService.BuscarTodos());

        public IActionResult DeletarCliente(long usuarioId)
        {
            var cliente = _clienteService.BuscarPorId(usuarioId);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        // ===================== POSTS =====================

        [HttpPost]
        public async Task<IActionResult> CadastrarProdutos(CadastrarProdutoViewModel vm)
        {
            try
            {
                var produto = new Produto
                {
                    Nome      = vm.Nome,
                    Descricao = vm.Descricao,
                    Categoria = vm.Categoria,
                    Tipo      = vm.Tipo
                };

                _produtoService.CadastrarProduto(produto);
                return RedirectToAction("CadastrarVariacao", new { produtoId = produto.ProdutoId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarVariacao(CadastrarVariacaoViewModel vm, IFormFile fotoArquivo)
        {
            try
            {
                if (fotoArquivo != null && fotoArquivo.Length > 0)
                {
                    string nomeArquivo = Guid.NewGuid() + "_" + fotoArquivo.FileName;
                    string caminho     = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "produtos");

                    if (!Directory.Exists(caminho)) Directory.CreateDirectory(caminho);

                    using var stream = new FileStream(Path.Combine(caminho, nomeArquivo), FileMode.Create);
                    await fotoArquivo.CopyToAsync(stream);
                    vm.ImagemUrl = nomeArquivo;
                }

                var variacao = new ProdutoVariacao
                {
                    Tamanho    = vm.Tamanho,
                    Cor        = vm.Cor,
                    Estoque    = vm.Estoque,
                    ProdutoId  = vm.ProdutoId,
                    ImagemUrl  = vm.ImagemUrl,
                    Preco      = vm.Preco,
                    PrecoCusto = vm.PrecoCusto
                };

                _produtoService.CadastrarVariacao(variacao);
                return RedirectToAction("ListarProdutos");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarVariacao(AtualizarVariacaoViewModel vm, IFormFile fotoArquivo)
        {
            if (fotoArquivo != null && fotoArquivo.Length > 0)
            {
                string nomeArquivo = Guid.NewGuid() + "_" + fotoArquivo.FileName;
                string caminho     = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "produtos");

                if (!Directory.Exists(caminho)) Directory.CreateDirectory(caminho);

                using var stream = new FileStream(Path.Combine(caminho, nomeArquivo), FileMode.Create);
                await fotoArquivo.CopyToAsync(stream);
                vm.ImagemUrl = nomeArquivo;
            }

            if (!ModelState.IsValid) return View(vm);

            var variacao = _produtoService.BuscarVariacaoPorId(vm.VariacaoId);
            if (variacao == null) return NotFound();

            variacao.Estoque    = vm.Estoque;
            variacao.Preco      = vm.Preco;
            variacao.PrecoCusto = vm.PrecoCusto;
            variacao.Tamanho    = vm.Tamanho;
            variacao.Cor        = vm.Cor;
            variacao.ImagemUrl  = vm.ImagemUrl;

            _produtoService.AtualizarVariacao(variacao);
            return RedirectToAction("ListarProdutos");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletarVariacaoConfirmado(long variacaoId)
        {
            _produtoService.DeletarVariacao(variacaoId);
            return RedirectToAction("ListarProdutos");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarProduto(Produto produto)
        {
            if (!ModelState.IsValid) return View(produto);

            _produtoService.Atualizar(produto);
            return RedirectToAction("ListarProdutos");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletarProdutoConfirmado(long produtoId)
        {
            _produtoService.Deletar(produtoId);
            return RedirectToAction("ListarProdutos");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletarClienteConfirmado(long usuarioId)
        {
            _clienteService.Deletar(usuarioId);
            return RedirectToAction("GerenciarClientes");
        }
    }
}