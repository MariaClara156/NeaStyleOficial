using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Data;

namespace NeaStyleOficial.Repositories
{
    public class ReembolsoRepository
    {
        private readonly NeaStyleContext _context;

        public ReembolsoRepository(NeaStyleContext context)
        {
            _context = context;
        }
        // CREATE - adiciona novo reembolso no banco
        public void Criar(Reembolso reembolso)
        {
            
                _context.Reembolsos.Add(reembolso);
                _context.SaveChanges(); // confirma a operação no banco
            
        }

        // READ - busca todos os reembolsos ja feitos ( achei legal incrementar essa função, pq pode ser util pra admin ou pra cliente ver o historico de reembolsos)
        public List<Reembolso> BuscarTodos()
        {
            
                return _context.Reembolsos.ToList();
            
        }

        // READ - busca reembolso por pedido Id
        public Reembolso BuscarPorId(long PedidoId)
        {
            
                return _context.Reembolsos.FirstOrDefault(r => r.PedidoId == PedidoId);   
        }

        public void Atualizar(Reembolso reembolso)
        {
            
                _context.Reembolsos.Update(reembolso);
                _context.SaveChanges();
            
        }
    }
}