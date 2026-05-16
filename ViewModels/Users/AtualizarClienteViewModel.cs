using NeaStyleOficial.Models.Users;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NeaStyleOficial.ViewModels.Users
{
    public class AtualizarClienteViewModel
    {
        public long ClienteId { get; set; }
        
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
    }
}