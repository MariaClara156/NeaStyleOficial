using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Repositories;

namespace NeaStyleOficial.Services
{
    public class PagamentoService
    {
        private readonly PagamentoRepository _pagamentoRepo;
        private readonly PedidoRepository    _pedidoRepo;

        public PagamentoService(PagamentoRepository pagamentoRepo, PedidoRepository pedidoRepo)
        {
            _pagamentoRepo = pagamentoRepo;
            _pedidoRepo    = pedidoRepo;
        }

        public void RealizarPagamento(long pedidoId, MetodoPagamento metodo, int parcelas, decimal valorPago)
        {
            var pagamento = new Pagamento(pedidoId, valorPago, parcelas, metodo, StatusPagamento.Aprovado);
            _pagamentoRepo.Criar(pagamento);

            // Atualiza o status do pedido para Confirmado
            _pedidoRepo.Atualizar(pedidoId, StatusPedido.Confirmado);
        }

        public void ProcessarPagamento(Pagamento pagamento)
        {
            pagamento.StatusPagamento = StatusPagamento.Processando;
            _pagamentoRepo.AtualizarStatus(pagamento.PedidoId, StatusPagamento.Processando);
        }

        public List<Pagamento> BuscarTodos()
        {
            return _pagamentoRepo.BuscarTodos();
        }

        public void ConfirmarPagamento(long pedidoId)
        {
            var pagamento = _pagamentoRepo.BuscarPorId(pedidoId);

            if (pagamento == null)
                throw new Exception("Pagamento não encontrado!");

            // Atualiza o status usando o enum StatusPagamento
            _pagamentoRepo.AtualizarStatus(pedidoId, StatusPagamento.Aprovado);
        }
    }
}