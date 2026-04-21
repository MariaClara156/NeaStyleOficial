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
            if ((DateTime.Now - reembolso.Pedido.DataPedido).TotalDays > 30)
            throw new Exception("Prazo de 30 dias para reembolso expirado!");
            // Regra: valor do reembolso deve ser positivo, maior que zero, e condizer com o valor total do pedido
            if (reembolso.ValorReembolso <= 0 || reembolso.ValorReembolso != reembolso.Pedido.ValorTotal)
                throw new Exception
                ("Valor do reembolso deve ser maior que zero e condizer com o valor total do pedido!");
            // Simula processamento de reembolso
            Console.WriteLine($"Processando reembolso de R${reembolso.ValorReembolso} para o pedido {reembolso.PedidoId}...");
            
            /* Simula sucesso do estorno no banco */
            bool sucessoDoEstornoNoBanco = true; // Simulação: sempre retorna sucesso
            if (sucessoDoEstornoNoBanco) {
    reembolso.Pedido.Status = StatusPedido.Estornado;
    _pedidoRepo.Atualizar(reembolso.Pedido.PedidoId, StatusPedido.Estornado);
}
        }
        public Reembolso ConfirmarReembolso(long reembolsoId)
        {
            var reembolso = _reembolsoRepo.BuscarPorId(reembolsoId);
             if (reembolso == null)
                throw new Exception("Reembolso não encontrado!");
             if (reembolso.Status != StatusReembolso.EmAnalise)
                throw new Exception("Reembolso já processado!");
            reembolso.Status = StatusReembolso.Aprovado;
            reembolso.Pedido.Status = StatusPedido.Cancelado;  
            _pedidoRepo.Atualizar(reembolso.Pedido.PedidoId, StatusPedido.Estornado);
            return reembolso;
        }
    }
}