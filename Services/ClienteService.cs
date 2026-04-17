using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Repositories;

namespace NeaStyleOficial.Services
{
    public class ClienteService
    {
        // Instancia o Repository — Service nunca fala direto com o banco
        private ClienteRepository _repo = new ClienteRepository();

        public void CadastrarCliente(Cliente cliente)
        {
            // Regra: email não pode ser vazio
            if (string.IsNullOrEmpty(cliente.Email))
                throw new Exception("Email obrigatório!");

            // Regra: senha mínimo 6 caracteres
            if (cliente.Senha.Length < 6)
                throw new Exception("Senha deve ter no mínimo 6 caracteres!");

            _repo.Criar(cliente);
        }

        public List<Cliente> BuscarTodos() => _repo.BuscarTodos();

        public Cliente BuscarPorId(long id) => _repo.BuscarPorId(id);

        public void Atualizar(Cliente cliente) => _repo.Atualizar(cliente);

        public void Deletar(long id) => _repo.Deletar(id);
    }
}