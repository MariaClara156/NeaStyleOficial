using Microsoft.AspNetCore.Mvc;
using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Users;

namespace NeaStyleOficial.Repositories
{
    // Repositório específico pra operações relacionadas a administradores, como buscar por email e atualizar dados
    // Ele é injetado com o contexto do Entity Framework Core pra acessar o banco de dados
    public class AdministradorRepository
    {
        // O contexto do Entity Framework Core, que representa a sessão com o banco de dados e é usado pra consultar e salvar dados
        private readonly NeaStyleContext _context;
        // Construtor que recebe o contexto do Entity Framework Core por injeção de dependência
        public AdministradorRepository(NeaStyleContext context)
        {
            _context = context;
        }
        // Método pra buscar um administrador por email, usado no login
        public Administrador BuscarPorEmail(string email)
        {
            //Retorna o primeiro administrador encontrado com o email fornecido, ou null se nenhum for encontrado
            return _context.Administradores
            .FirstOrDefault(a => a.Email == email);
        }
        // Método pra atualizar os dados de um administrador, usado na edição de perfil
        public void Atualizar(Administrador administrador)
        {
            //Update atualiza o estado do administrador no contexto, e SaveChanges salva as alterações no banco de dados
            _context.Administradores.Update(administrador);
            _context.SaveChanges();
        }
    }
}