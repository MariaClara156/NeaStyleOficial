using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Repositories;

namespace NeaStyleOficial.Services
{
    public class ClienteService
    {
        private readonly ClienteRepository _clienteRepo;

        public ClienteService(ClienteRepository repo)
        {
            _clienteRepo = repo;
        }

        public void CadastrarCliente(Cliente cliente)
        {
            // Regra: email não pode ser vazio
            if (string.IsNullOrEmpty(cliente.Email))
                throw new Exception("Email obrigatório!");

            // Regra: senha mínimo 6 caracteres
            if (cliente.Senha.Length < 6)
                throw new Exception("Senha deve ter no mínimo 6 caracteres!");

            _clienteRepo.Criar(cliente);
        }

        public List<Cliente> BuscarTodos() => _clienteRepo.BuscarTodos();

        public Cliente BuscarPorId(long UsuarioId) => _clienteRepo.BuscarPorId(UsuarioId);

        public void Atualizar(Cliente cliente) => _clienteRepo.Atualizar(cliente);

        public void Deletar(long UsuarioId) => _clienteRepo.Deletar(UsuarioId);
    }
}