using Microsoft.AspNetCore.Identity;
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
            _clienteRepo.Criar(cliente);
        }

        public List<Cliente> BuscarTodos() => _clienteRepo.BuscarTodos();

        public Cliente BuscarPorId(long usuarioId) => _clienteRepo.BuscarPorId(usuarioId);

        public Cliente BuscarPorEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new Exception("Email obrigatório!");
            return _clienteRepo.BuscarPorEmail(email);
        }

        public bool VerificarSenha(string senhaDigitada, string senhaBanco)
    {
        return senhaDigitada == senhaBanco;
    }

    public void AlterarSenha(string email, string senhaAntiga, string novaSenha)
    {
        var cliente = _clienteRepo.BuscarPorEmail(email);
        if (cliente == null) throw new Exception("Cliente não encontrado!");
        
        // Verifica se a senha antiga está correta antes de trocar
        if (!VerificarSenha(senhaAntiga, cliente.Senha))
            throw new Exception("Senha atual incorreta!");
        cliente.Senha = novaSenha;
        _clienteRepo.Atualizar(cliente);
    }
        public void Atualizar(Cliente cliente) => _clienteRepo.Atualizar(cliente);
        public void Deletar(long usuarioId) => _clienteRepo.Deletar(usuarioId);
    }
}