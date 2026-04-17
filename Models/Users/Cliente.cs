using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Models.Sales;
using System;

namespace NeaStyleOficial.Models.Users
{
    public class Cliente : Usuario
    {
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public List<Carrinho> Carrinhos { get; set; } = new();
        public List<Favorito> Favoritos { get; set; } = new();
        public List<Pedido> Pedidos { get; set; } = new();

        public Cliente() { }
        public Cliente(string nome, string email, string senha, string endereco, string telefone, DateTime dataNascimento)
            : base(nome, email, senha)
        {
            Endereco = endereco;
            Telefone = telefone;
            DataNascimento = dataNascimento;
        }
    }
}
