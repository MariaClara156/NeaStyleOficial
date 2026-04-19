using Microsoft.EntityFrameworkCore;
using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Users;

namespace NeaStyleOficial.Repositories
{
    public class ClienteRepository
    {
        private readonly NeaStyleContext context;
        // CREATE - adiciona novo cliente no banco
        public void Criar(Cliente cliente)
        {
            {
                context.Clientes.Add(cliente);
                context.SaveChanges(); // confirma a operação no banco
            }
        }

        // READ - busca todos os clientes
        public List<Cliente> BuscarTodos()
        {
            {
                return context.Clientes.ToList();
            }
        }

        // READ - busca cliente por ID
        public Cliente BuscarPorId(long id)
        {
            {
                return context.Clientes.Find(id);
            }
        }

        // UPDATE - atualiza dados do cliente
        public void Atualizar(Cliente cliente)
        {
            {
                context.Clientes.Update(cliente);
                context.SaveChanges();
            }
        }

        // DELETE - remove cliente do banco
        public void Deletar(long id)
        {
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