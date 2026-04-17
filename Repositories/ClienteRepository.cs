using Microsoft.EntityFrameworkCore;
using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Users;

namespace NeaStyleOficial.Repositories
{
    public class ClienteRepository
    {
        // CREATE - adiciona novo cliente no banco
        public void Criar(Cliente cliente)
        {
            using (var context = new NeaStyleContext())
            {
                context.Clientes.Add(cliente);
                context.SaveChanges(); // confirma a operação no banco
            }
        }

        // READ - busca todos os clientes
        public List<Cliente> BuscarTodos()
        {
            using (var context = new NeaStyleContext())
            {
                return context.Clientes.ToList();
            }
        }

        // READ - busca cliente por ID
        public Cliente BuscarPorId(long id)
        {
            using (var context = new NeaStyleContext())
            {
                return context.Clientes.Find(id);
            }
        }

        // UPDATE - atualiza dados do cliente
        public void Atualizar(Cliente cliente)
        {
            using (var context = new NeaStyleContext())
            {
                context.Clientes.Update(cliente);
                context.SaveChanges();
            }
        }

        // DELETE - remove cliente do banco
        public void Deletar(long id)
        {
            using (var context = new NeaStyleContext())
            {
                // Primeiro busca o cliente, depois remove
                var cliente = context.Clientes.Find(id);
                if (cliente != null)
                {
                    context.Clientes.Remove(cliente);
                    context.SaveChanges();
                }
            }
        }
    }
}