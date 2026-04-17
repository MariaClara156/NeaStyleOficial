using System;

namespace NeaStyleOficial.Models.Users
{
    public class Administrador : Usuario
    {
        public string Cargo { get; set; }

        public Administrador() { }
        public Administrador(string nome, string email, string senha, string cargo)
            : base(nome, email, senha)
        {
            Cargo = cargo;
        }
        
    }
}
