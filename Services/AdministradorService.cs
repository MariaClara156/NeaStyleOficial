using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Repositories;
// Serviço de negócios pra lógica relacionada a administradores, como autenticação, alteração de senha, etc.
// Ele usa o AdministradorRepository pra acessar os dados dos administradores no banco de dados, e o PasswordHasher do ASP.NET Core Identity pra lidar com hashing de senhas
public class AdministradorService
{
    private readonly AdministradorRepository _repo;
    public AdministradorService(AdministradorRepository repo)
    {
        _repo = repo;
    }

    public Administrador? BuscarPorEmail(string email)
    {
        return _repo.BuscarPorEmail(email);
    }

    public bool VerificarSenha(string senhaDigitada, string senhaBanco)
    {
        return senhaDigitada == senhaBanco;
    }

    public void AlterarSenha(string email, string senhaAntiga, string novaSenha)
    {
        var adm = _repo.BuscarPorEmail(email);
        if (adm == null) throw new Exception("Administrador não encontrado!");
        
        // Verifica se a senha antiga está correta antes de trocar
        if (!VerificarSenha(senhaAntiga, adm.Senha))
            throw new Exception("Senha atual incorreta!");
        adm.Senha = novaSenha;
        _repo.Atualizar(adm);
    }
}