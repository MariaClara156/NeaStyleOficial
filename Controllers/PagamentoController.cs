using Microsoft.AspNetCore.Mvc;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Services;

namespace NeaStyleOficial.Controllers
{
    public class PagamentoController : Controller
    {
        private readonly PagamentoService _pagamentoService;
        private readonly PedidoService _pedidoService;

        public PagamentoController(PagamentoService pagamentoService, PedidoService pedidoService)
        {
            _pagamentoService = pagamentoService;
            _pedidoService = pedidoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProcessarPagamento(Pagamento pagamento)
        {
            try
            {
                _pagamentoService.ProcessarPagamento(pagamento);
                return RedirectToAction("Confirmacao", new { PedidoId = pagamento.PedidoId });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Index");
            }
        }

        public IActionResult Confirmacao(long PedidoId)
        {
            try
            {
                _pagamentoService.ConfirmarPagamento(PedidoId);
                var pedido = _pedidoService.VerDetalhesPedido(PedidoId);
                return View(pedido);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Index");
            }
        }
    }
}