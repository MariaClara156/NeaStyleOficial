using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Users;

namespace NeaStyleOficial.Repositories
{
    public class ClienteRepository
    {
        private readonly NeaStyleContext _context;
        public ClienteRepository(NeaStyleContext context)
        {
            _context = context;
        }

        // CREATE - adiciona novo cliente no banco
        public void Criar(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }

        // READ - busca todos os clientes
        public List<Cliente> BuscarTodos()
        {
            // Retorna uma lista de todos os clientes no banco de dados
            return _context.Clientes.ToList();
        }

        // READ - busca cliente por ID (herdado de Usuario)
        public Cliente BuscarPorId(long usuarioId)
        {   
            return _context.Clientes.Find(usuarioId);
        }
        public Cliente BuscarPorEmail(string email)
        {
            return _context.Clientes
            .FirstOrDefault(c => c.Email == email);
        }

        // UPDATE - atualiza dados do cliente
        public void Atualizar(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            _context.SaveChanges();
        }

        // DELETE - remove cliente do banco
        public void Deletar(long usuarioId)
        {
            // Primeiro busca o cliente, depois remove
            var cliente = _context.Clientes.Find(usuarioId);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
            }
        }
    }
}