using Microsoft.AspNetCore.Mvc;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Services;

namespace NeaStyleOficial.Controllers
{
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

        // Dashboard
        public IActionResult Index()
        {
            return View();
        }
        /* CRUD Produtos */
        public IActionResult GerenciarProdutos()
        {
            var produtos = _produtoService.BuscarTodos();
            return View(produtos);
        }   
        // CORRETO - GET abre formulário vazio, POST recebe os dados
        public IActionResult CadastrarProduto()
        {
            return View(); // só abre a página vazia
        }

        [HttpPost]
        public IActionResult CadastrarProduto(Produto produto)
        {
            try
            {
                _produtoService.CadastrarProduto(produto);
                return RedirectToAction("GerenciarProdutos");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(produto);
            }
        }

        public IActionResult EditarProduto(long UsuarioId)
        {
            var produto = _produtoService.BuscarPorId(UsuarioId);
            if (produto == null)
                return NotFound();
            return View(produto);
        }
        [HttpPost]
        public IActionResult EditarProduto(Produto produto)
        {
            _produtoService.Atualizar(produto);
            return RedirectToAction("GerenciarProdutos");
        }

        public IActionResult DeletarProduto(long ProdutoId)
        {
            var produto = _produtoService.BuscarPorId(ProdutoId);
            if (produto == null)
                return NotFound();
            _produtoService.Deletar(ProdutoId);
            return RedirectToAction("GerenciarProdutos");
        }

        /* Read Pedidos */
        public IActionResult GerenciarPedidos()
        {
            var pedidos = _pedidoService.VerTodosPedidos();
            return View(pedidos);
        }
        // Relatório de vendas
        public IActionResult Relatorio()
        {
            var pedidos = _pedidoService.VerTodosPedidos();
            return View(pedidos);
        }

        /* Read, Delete Clientes */
        public IActionResult GerenciarClientes()
        {
            var clientes = _clienteService.BuscarTodos();
            return View(clientes);
        }
        public IActionResult DeletarCliente(long UsuarioId)
        {
            var cliente = _clienteService.BuscarPorId(UsuarioId);
            if (cliente == null)
                return NotFound();
            _clienteService.Deletar(UsuarioId);
            return RedirectToAction("GerenciarClientes");
        }
    }
}