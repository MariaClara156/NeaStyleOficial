// using de collection e sales para relacionar cliente com carrinho, favoritos e pedidos
using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Models.Sales;
using System;

namespace NeaStyleOficial.Models.Users
{
    // Cliente herda de Usuario, adicionando propriedades específicas para clientes
    public class Cliente : Usuario
    {
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        // Relacionamentos: um cliente pode ter vários carrinhos, favoritos e pedidos
        public List<Carrinho> Carrinhos { get; set; } = new();
        public List<Favorito> Favoritos { get; set; } = new();
        public List<Pedido> Pedidos { get; set; } = new();

        protected Cliente() { }
        // Construtor para criar um cliente, utilizando o construtor base para inicializar as propriedades de Usuario
        public Cliente(string nome, string email, string senha, string endereco, string telefone, DateTime dataNascimento)
            : base(nome, email, senha)
        {
            Endereco = endereco;
            Telefone = telefone;
            DataNascimento = dataNascimento;
        }
    }
}
