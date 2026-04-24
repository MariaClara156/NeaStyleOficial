using Microsoft.AspNetCore.Mvc;
using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Users;

namespace NeaStyleOficial.Repositories
{
    public class AdministradorRepository
    {
        private readonly NeaStyleContext _context;

        public AdministradorRepository(NeaStyleContext context)
        {
            _context = context;
        }

        public Administrador BuscarPorEmail(string email)
        {
            return _context.Administradores
            .FirstOrDefault(a => a.Email == email);
        }

        public void Atualizar(Administrador administrador)
        {
                
            _context.Administradores.Update(administrador);
            _context.SaveChanges();
        }
    }
}