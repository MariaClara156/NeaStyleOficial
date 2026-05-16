using Microsoft.AspNetCore.Mvc;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Services;
using NeaStyleOficial.ViewModels.Sales;

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

        public IActionResult Index(long pedidoId)
        {
            var pedido = _pedidoService.VerDetalhesPedido(pedidoId);

            var viewModel = new PagamentoViewModel
            {
                PedidoId = pedido.PedidoId,
                ValorTotal = pedido.ValorTotal,
                ItensPedido = pedido.Itens ?? new List<ItemPedido>() 
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ProcessarPagamento(PagamentoViewModel vm)
        {
            try
            {
                _pagamentoService.RealizarPagamento(vm.PedidoId, vm.MetodoPagamento, vm.Parcelas, vm.ValorTotal);
                return RedirectToAction("Confirmacao", new { pedidoId = vm.PedidoId });
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                return RedirectToAction("Index", "Pedido");
            }
        }

        public IActionResult Confirmacao(long pedidoId)
        {
            try
            {
                var pedido = _pedidoService.VerDetalhesPedido(pedidoId);
                return View(pedido);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                return RedirectToAction("Index", "Pedido");
            }
        }
    }
}