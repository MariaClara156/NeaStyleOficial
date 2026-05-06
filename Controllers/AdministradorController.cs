using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Services;
using NeaStyleOficial.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace NeaStyleOficial.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdministradorController : Controller
    {
        private readonly ProdutoService _produtoService;
        private readonly ClienteService _clienteService;
        private readonly PedidoService _pedidoService;

        public AdministradorController(ProdutoService produtoService, ClienteService clienteService, PedidoService pedidoService)
        {
            _produtoService = produtoService;
            _clienteService = clienteService;
            _pedidoService = pedidoService;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }
        //-------------PRODUTOS------------
        public IActionResult ListarProdutos()
        {
            var produtos = _produtoService.BuscarTodos();
            
            var viewModel = produtos.Select(p => new DetalheProdutoViewModel
            {
                Produto = p,
                Variacoes = p.Variacoes,
                EstoqueTotal = p.Variacoes.Sum(v => v.Estoque)
            }).ToList();

            return View(viewModel);
        }   
        public IActionResult CadastrarProdutos()
        {
            return View();
        }
        public IActionResult CadastrarVariacao(long produtoId)
        {
            var produto = _produtoService.BuscarPorId(produtoId);
            if (produto == null)
                return NotFound();
            ViewBag.ProdutoId = produtoId;
            return View();
        }
        
        public IActionResult EditarProduto(long produtoId)
        {
            var produto = _produtoService.BuscarPorId(produtoId);
            if (produto == null)
                return NotFound();
            return View(produto);
        }
        public IActionResult DeletarProduto(long produtoId)
        {
            var produto = _produtoService.BuscarPorId(produtoId);
            if (produto == null)
                return NotFound();

            return View(produto);
        }
        //--------PEDIDOS----------------
        public IActionResult GerenciarPedidos()
        {
            var pedidos = _pedidoService.VerTodosPedidos();
            return View(pedidos);
        }
        public IActionResult Relatorio()
        {
            var pedidos = _pedidoService.VerTodosPedidos();
            return View(pedidos);
        }
        //----------CLIENTES-------------
        public IActionResult GerenciarClientes()
        {
            var clientes = _clienteService.BuscarTodos();
            return View(clientes);
        }
        public IActionResult DeletarCliente(long usuarioId)
        {
            var cliente = _clienteService.BuscarPorId(usuarioId);
            if (cliente == null)
                return NotFound();

            return View(cliente);
        }
        //----------------POST--------------
        [HttpPost]
        public async Task<IActionResult> CadastrarProdutos(CadastrarProdutoViewModel vm, IFormFile fotoArquivo)
        {
            try
            {
                // Upload fica no Controller
                if (fotoArquivo != null && fotoArquivo.Length > 0)
                {
                    string nomeArquivo = Guid.NewGuid() + "_" + fotoArquivo.FileName;
                    string caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "produtos");
                    if (!Directory.Exists(caminho)) Directory.CreateDirectory(caminho);
                    using var stream = new FileStream(Path.Combine(caminho, nomeArquivo), FileMode.Create);
                    await fotoArquivo.CopyToAsync(stream);
                    vm.ImagemUrl = nomeArquivo;
                }
                var produto = new Produto
                {
                    Nome = vm.Nome,
                    Descricao = vm.Descricao,
                    Categoria = vm.Categoria,
                    Tipo = vm.Tipo             
                };
                _produtoService.CadastrarProduto(produto); // Service continua recebendo Produto
                // Redireciona pra tela de variação passando o ID do produto recém criado
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
                // Upload fica no Controller
                if (fotoArquivo != null && fotoArquivo.Length > 0)
                {
                    string nomeArquivo = Guid.NewGuid() + "_" + fotoArquivo.FileName;
                    string caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "produtos");
                    if (!Directory.Exists(caminho)) Directory.CreateDirectory(caminho);
                    using var stream = new FileStream(Path.Combine(caminho, nomeArquivo), FileMode.Create);
                    await fotoArquivo.CopyToAsync(stream);
                    vm.ImagemUrl = nomeArquivo;
                }

                var variacao = new ProdutoVariacao
                {
                    Tamanho = vm.Tamanho,
                    Cor = vm.Cor,
                    Estoque = vm.Estoque,
                    ProdutoId = vm.ProdutoId
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
        [ValidateAntiForgeryToken]
        public IActionResult EditarProduto(Produto produto)
        {
            if (!ModelState.IsValid)
                return View(produto);

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