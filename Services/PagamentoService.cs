using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Repositories;

namespace NeaStyleOficial.Services
{
    public class PagamentoService
    {
        private readonly PagamentoRepository _pagamentoRepo;

        public PagamentoService(PagamentoRepository pagamentoRepo)
        {
            _pagamentoRepo = pagamentoRepo;
        }

        public void ProcessarPagamento(Pagamento pagamento)
        {
            pagamento.StatusPagamento = StatusPagamento.Processando;
            _pagamentoRepo.AtualizarStatus(pagamento.PedidoId, StatusPagamento.Processando);
        }

        public void ConfirmarPagamento(long pedidoId)
        {
            var pagamento = _pagamentoRepo.BuscarPorId(pedidoId);
            if (pagamento == null) 
                throw new Exception("Pagamento não encontrado!");

            pagamento.StatusPagamento = StatusPagamento.Aprovado;
            /* Usando o ENUM criado em Pagamento.cs */
            _pagamentoRepo.AtualizarStatus(pedidoId, StatusPagamento.Aprovado);
        }
    }
}