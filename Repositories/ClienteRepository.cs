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

        // READ - busca cliente por ID (herdado de Usuario)
        public Cliente BuscarPorId(long usuarioId)
        {   
            {
                return context.Clientes.Find(usuarioId);
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
        public void Deletar(long UsuarioId)
        {
            {
                // Primeiro busca o cliente, depois remove
                var cliente = context.Clientes.Find(UsuarioId);
                if (cliente != null)
                {
                    context.Clientes.Remove(cliente);
                    context.SaveChanges();
                }
            }
        }
    }
}