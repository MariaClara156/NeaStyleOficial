using Microsoft.AspNetCore.Mvc;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Services;

namespace NeaStyleOficial.Controllers
{
    public class ReembolsoController : Controller
    {
        private readonly ReembolsoService _reembolsoService;
        private readonly PedidoService _pedidoService;

        public ReembolsoController(ReembolsoService reembolsoService, PedidoService pedidoService)
        {
            _reembolsoService = reembolsoService;
            _pedidoService = pedidoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProcessarReembolso(Reembolso reembolso)
        {
            try
            {
                _reembolsoService.ProcessarReembolso(reembolso);
                return RedirectToAction("Confirmacao", new { reembolsoId = reembolso.ReembolsoId });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Index");
            }
        }

        public IActionResult Confirmacao(long reembolsoId)
        {
            try
            {
                var reembolso = _reembolsoService.ConfirmarReembolso(reembolsoId);
                var pedido = _pedidoService.VerDetalhesPedido(reembolso.PedidoId);
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