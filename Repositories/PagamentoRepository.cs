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
                _context.SaveChanges(); // confirma a operação no banco
            
        }

        // READ - busca todos os pagamentos ja feitos ( achei legal incrementar essa função, pq pode ser util pra admin ou pra cliente ver o historico de pagamentos)
        public List<Pagamento> BuscarTodos()
        {
            
                return _context.Pagamentos.ToList();
            
        }

        // READ - busca pagamento por pedido Id
        public Pagamento BuscarPorPedidoId(long pedidoId)
        {
            
                return _context.Pagamentos.FirstOrDefault(p => p.PedidoId == pedidoId);
            
        }
    }
}