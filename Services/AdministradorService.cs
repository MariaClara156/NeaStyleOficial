using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Repositories;
using Microsoft.AspNetCore.Identity;

namespace NeaStyleOficial.Services
{
    // Serviço de negócios para lógica relacionada a administradores (autenticação, alteração de senha, etc.)
    public class AdministradorService
    {
        private readonly AdministradorRepository _admRepo;

        public AdministradorService(AdministradorRepository admRepo)
        {
            _admRepo = admRepo;
        }

        public Administrador? BuscarPorEmail(string email)
        {
            return _admRepo.BuscarPorEmail(email);
        }

        public bool VerificarSenha(Administrador adm, string senhaDigitada)
        {
            var hasher = new PasswordHasher<Administrador>();
            return hasher.VerifyHashedPassword(adm, adm.Senha, senhaDigitada) 
                != PasswordVerificationResult.Failed;
        }

        public void AlterarSenha(string email, string senhaAntiga, string novaSenha)
        {
            var adm = _admRepo.BuscarPorEmail(email);

            if (adm == null)
                throw new Exception("Administrador não encontrado!");

            if (!VerificarSenha(adm, senhaAntiga))   // ← passa o objeto adm
                throw new Exception("Senha atual incorreta!");

            var hasher = new PasswordHasher<Administrador>();
            adm.Senha = hasher.HashPassword(adm, novaSenha);   // ← salva com hash
            _admRepo.Atualizar(adm);
        }
    }
}