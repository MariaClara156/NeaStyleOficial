using Microsoft.AspNetCore.Mvc;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Models.Sales;
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
        public IActionResult GerenciarPedidos(long clienteId)
        {
            var pedidos = _pedidoService.VerPedidos(clienteId);
            return View(pedidos);
        }

        [HttpPost]
        public IActionResult CriarPedido(long clienteId)
        {
            try
            {
                _pedidoService.RealizarPedido(clienteId);
                return RedirectToAction("GerenciarPedidos", new { clienteId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("GerenciarCarrinho", new { clienteId });
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