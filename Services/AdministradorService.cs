using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Repositories;

public class AdministradorService
{
    private readonly AdministradorRepository _repo;

    public AdministradorService(AdministradorRepository repo)
    {
        _repo = repo;
    }

    public Administrador BuscarPorEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            throw new Exception("Email obrigatório!");
        return _repo.BuscarPorEmail(email);
    }

    public bool VerificarSenha(string senhaDigitada, string hashNoBanco)
    {
        var hasher = new PasswordHasher<Administrador>();
        var resultado = hasher.VerifyHashedPassword(null, hashNoBanco, senhaDigitada);
        return resultado == PasswordVerificationResult.Success;
    }

    public void AlterarSenha(string email, string senhaAntiga, string novaSenha)
    {
        var adm = _repo.BuscarPorEmail(email);
        if (adm == null) throw new Exception("Administrador não encontrado!");
        
        // Verifica se a senha antiga está correta antes de trocar
        if (!VerificarSenha(senhaAntiga, adm.Senha))
            throw new Exception("Senha atual incorreta!");

        var hasher = new PasswordHasher<Administrador>();
        adm.Senha = hasher.HashPassword(adm, novaSenha);
        _repo.Atualizar(adm);
    }
}