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
            if (pagamento.ValorPago <= 0)
                throw new Exception("O valor do pagamento deve ser maior que zero.");

            if (pagamento.MetodoPagamento == MetodoPagamento.CartaoCredito && pagamento.Parcelas > 6 && pagamento.Parcelas != 1)
                throw new Exception("O pagamento deve ser feito entre 1 e 6 parcelas!");
            
           if (pagamento.MetodoPagamento == MetodoPagamento.Boleto && pagamento.Parcelas != 1)
                throw new Exception("O pagamento com boleto deve ser à vista (1 parcela)!");
            
            if (pagamento.MetodoPagamento == MetodoPagamento.Pix && pagamento.Parcelas != 1)
                throw new Exception("O pagamento com Pix deve ser à vista (1 parcela)!");
            
            if (pagamento.MetodoPagamento == MetodoPagamento.CartaoDebito && pagamento.Parcelas != 1)
                throw new Exception("O pagamento com cartão de débito deve ser à vista (1 parcela)!");
            
            pagamento.StatusPagamento = StatusPagamento.Processando;
            _pagamentoRepo.AtualizarStatus(pagamento.PedidoId, StatusPagamento.Processando);
        }

        public void ConfirmarPagamento(long PedidoId)
        {
            var pagamento = _pagamentoRepo.BuscarPorId(PedidoId);
            if (pagamento == null) 
                throw new Exception("Pagamento não encontrado!");

            pagamento.StatusPagamento = StatusPagamento.Aprovado;
            /* Usando o ENUM criado em Pagamento.cs */
            _pagamentoRepo.AtualizarStatus(PedidoId, StatusPagamento.Aprovado);
        }
    }
}