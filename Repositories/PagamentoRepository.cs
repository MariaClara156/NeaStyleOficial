using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Data;

namespace NeaStyleOficial.Repositories
{
    public class PagamentoRepository
    {
        private readonly NeaStyleContext _context;
        public PagamentoRepository(NeaStyleContext context)
        {
            _context = context;
        }

        // CREATE - adiciona novo pagamento no banco
        public void Criar(Pagamento pagamento)
        {
                _context.Pagamentos.Add(pagamento);
                _context.SaveChanges();
        }

        // READ - busca todos os pagamentos ja feitos ( achei legal incrementar essa função, pq pode ser util pra admin ou pra cliente ver o historico de pagamentos)
        public List<Pagamento> BuscarTodos()
        {
                return _context.Pagamentos.ToList();
        }

        // READ - busca pagamento por pedido Id
        public Pagamento BuscarPorId(long pedidoId)
        {
                return _context.Pagamentos.FirstOrDefault(p => p.PedidoId == pedidoId);
        }

        // UPDATE - atualiza o status do pagamento (pode ser util pra admin ou pra cliente acompanhar o status do pagamento)
        public void AtualizarStatus(long pedidoId, StatusPagamento novoStatus)
        {
                var pagamento = _context.Pagamentos.FirstOrDefault(p => p.PedidoId == pedidoId);
                if (pagamento == null) 
                throw new Exception("Pagamento não encontrado!");
                pagamento.StatusPagamento = novoStatus;
                _context.Pagamentos.Update(pagamento);
                _context.SaveChanges();
        }
    }
}