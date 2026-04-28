namespace NeaStyleOficial.Models.Users
{
    public abstract class Usuario
    {
        public long UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public Usuario() { }
        public Usuario(string nome, string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome obrigatório!");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email obrigatório!");
            if (string.IsNullOrWhiteSpace(senha) || senha.Length < 6) 
            throw new ArgumentException("Senha deve ter mínimo 6 caracteres!");
            Nome = nome;
            Email = email;
            Senha = senha;
        }
    }
}
