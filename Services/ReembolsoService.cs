using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Repositories;

namespace NeaStyleOficial.Services
{
    public class ReembolsoService
    {
        private readonly ReembolsoRepository _reembolsoRepo;
        private readonly PedidoRepository _pedidoRepo;

        public ReembolsoService(ReembolsoRepository reembolsoRepo, PedidoRepository pedidoRepo)
        {
            _reembolsoRepo = reembolsoRepo;
            _pedidoRepo = pedidoRepo;
        }

        public void ProcessarReembolso(Reembolso reembolso)
        {
            var pedido = _pedidoRepo.BuscarPorId(reembolso.PedidoId);
            if (pedido == null)
                throw new Exception("Pedido não encontrado!");

            if ((DateTime.Now - pedido.DataPedido).TotalDays > 30)
                throw new Exception("Prazo de 30 dias expirado!");

            if (reembolso.ValorReembolso <= 0 || reembolso.ValorReembolso > pedido.ValorTotal)
                throw new Exception("Valor inválido!");

            reembolso.Status = StatusReembolso.EmAnalise;

            _reembolsoRepo.Atualizar(reembolso);
        }
        public Reembolso ConfirmarReembolso(long reembolsoId)
        {
            var reembolso = _reembolsoRepo.BuscarPorId(reembolsoId);
            if (reembolso == null)
                throw new Exception("Reembolso não encontrado!");

            if (reembolso.Status != StatusReembolso.EmAnalise)
                throw new Exception("Reembolso já processado!");

            reembolso.Status = StatusReembolso.Aprovado;

            var pedido = _pedidoRepo.BuscarPorId(reembolso.PedidoId);
            pedido.Status = StatusPedido.Estornado;

            _pedidoRepo.Atualizar(pedido.PedidoId, StatusPedido.Estornado);
            _reembolsoRepo.Atualizar(reembolso);

            return reembolso;
        }
    }
}