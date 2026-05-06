using System;

namespace NeaStyleOficial.Models.Users
{
    public abstract class Usuario
    {
        // Propriedades : set privado para evitar alterações externas, garantindo integridade dos dados
        public long UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        // Construtor protegido para uso do EF Core
        protected Usuario() { }
        // Construtor público para criação de novos usuários, com validação básica
        public Usuario(string nome, string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome obrigatório!");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email obrigatório!");

            if (string.IsNullOrWhiteSpace(senha) || senha.Length < 6)
                throw new ArgumentException("Senha deve ter mínimo 6 caracteres!");

            Nome = nome;
            Email = email;
            Senha = senha;
        }
    }
}
