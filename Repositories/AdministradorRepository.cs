using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Users;

// Repositório para operações relacionadas a administradores, injetado com o contexto do EF Core
namespace NeaStyleOficial.Repositories
{
    public class AdministradorRepository
    {
        private readonly NeaStyleContext _context;

        // Construtor que recebe o contexto do EF Core por injeção de dependência
        public AdministradorRepository(NeaStyleContext context)
        {
            _context = context;
        }

        // Busca um administrador por email — usado no login
        public Administrador BuscarPorEmail(string email)
        {
            return _context.Administradores
                .FirstOrDefault(a => a.Email == email);
        }

        // Atualiza os dados de um administrador — usado na edição de perfil
        public void Atualizar(Administrador administrador)
        {
            _context.Administradores.Update(administrador);
            _context.SaveChanges();
        }
    }
}