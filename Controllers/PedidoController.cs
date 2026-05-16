using Microsoft.AspNetCore.Mvc;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.ViewModels.Sales;
using System.Security.Claims;
using NeaStyleOficial.Services;

namespace NeaStyleOficial.Controllers
{
    public class PedidoController : Controller
    {
        private readonly PedidoService _pedidoService;
        private readonly CarrinhoService _carrinhoService;

        public PedidoController(PedidoService pedidoService, CarrinhoService carrinhoService)
        {
            _pedidoService = pedidoService;
            _carrinhoService = carrinhoService;
        }
        public IActionResult Index()
        {
            return View();
        }

        /* CRUD Pedido */
        /* GET */
        public IActionResult GerenciarPedidos()
        {
            var clienteId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var pedidos = _pedidoService.VerPedidos(clienteId);

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

        [HttpPost]
        public IActionResult CriarPedido()
        {
            try
            {
                var clienteId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var pedidoId = _pedidoService.RealizarPedido(clienteId);
        return RedirectToAction("Index", "Pagamento", new { pedidoId });
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                return RedirectToAction("Index", "Carrinho");
            }
        }

        public IActionResult DetalhesPedido(long PedidoId)
        {
            try
            {
                var pedido = _pedidoService.VerDetalhesPedido(PedidoId);
                return View(pedido);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("GerenciarPedidos");
            }
        }
        [HttpPost]
        public IActionResult CancelarPedido(long PedidoId)
        {
            try
            {
                _pedidoService.CancelarPedido(PedidoId);
                return RedirectToAction("GerenciarPedidos");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("GerenciarPedidos");
            }
        }

        public IActionResult VerTodosPedidos()
        {
            var pedidos = _pedidoService.VerTodosPedidos();
            return View(pedidos);
        }
    }
}