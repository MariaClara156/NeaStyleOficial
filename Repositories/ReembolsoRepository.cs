using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Sales;

namespace NeaStyleOficial.Repositories
{
    public class ReembolsoRepository
    {
        private readonly NeaStyleContext _context;

        public ReembolsoRepository(NeaStyleContext context)
        {
            _context = context;
        }

        // CREATE — adiciona novo reembolso no banco
        public void Criar(Reembolso reembolso)
        {
            _context.Reembolsos.Add(reembolso);
            _context.SaveChanges();
        }

        // READ — busca todos os reembolsos realizados
        public List<Reembolso> BuscarTodos()
        {
            return _context.Reembolsos.ToList();
        }

        // READ — busca reembolso por PedidoId
        public Reembolso BuscarPorId(long pedidoId)
        {
            return _context.Reembolsos.FirstOrDefault(r => r.PedidoId == pedidoId);
        }

        // UPDATE — atualiza dados do reembolso
        public void Atualizar(Reembolso reembolso)
        {
            _context.Reembolsos.Update(reembolso);
            _context.SaveChanges();
        }
    }
}