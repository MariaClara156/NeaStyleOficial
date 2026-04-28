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
            // Regra: email não pode ser vazio
            if (string.IsNullOrEmpty(cliente.Email))
                throw new Exception("Email obrigatório!");

            // Regra: senha mínimo 6 caracteres
            if (cliente.Senha.Length < 6)
                throw new Exception("Senha deve ter no mínimo 6 caracteres!");
                // criptografa a senha antes de salvar
            var hasher = new PasswordHasher<Cliente>();
            cliente.Senha = hasher.HashPassword(cliente, cliente.Senha);
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

        public bool VerificarSenha(string senhaDigitada, string hashNoBanco)
        {
            var hasher = new PasswordHasher<Cliente>();
            var resultado = hasher.VerifyHashedPassword(new Cliente(), hashNoBanco, senhaDigitada);
            return resultado == PasswordVerificationResult.Success;
        }

        public void AlterarSenha(string email, string senhaAntiga, string novaSenha)
        {   
            var cliente = _clienteRepo.BuscarPorEmail(email);
            if (cliente == null) throw new Exception("Cliente não encontrado!");
            // Verifica se a senha antiga está correta antes de trocar
            if (!VerificarSenha(senhaAntiga, cliente.Senha))
                throw new Exception("Senha atual incorreta!");

            if (string.IsNullOrEmpty(novaSenha) || novaSenha.Length < 6)
            throw new Exception("Nova senha deve ter no mínimo 6 caracteres!");

            var hasher = new PasswordHasher<Cliente>();
            cliente.Senha = hasher.HashPassword(cliente, novaSenha);
            _clienteRepo.Atualizar(cliente);
        }

        public void Atualizar(Cliente cliente) => _clienteRepo.Atualizar(cliente);

        public void Deletar(long usuarioId) => _clienteRepo.Deletar(usuarioId);
    }
}