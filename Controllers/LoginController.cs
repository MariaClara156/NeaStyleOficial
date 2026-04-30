using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Services;
using NeaStyleOficial.Data;

namespace NeaStyleOficial.Controllers
{
    public class LoginController : Controller
    {
    private readonly NeaStyleContext _context;

    public LoginController(NeaStyleContext context)
    {
        _context = context;
    }

    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> Index(string email, string senha)
    {
        try
        {
            // 1. Tenta buscar no banco se existe um Administrador com esse email e senha
            var adm = _context.Administradores
                .FirstOrDefault(a => a.Email == email && a.Senha == senha);

        if (adm != null)
        {
            await CriarSessao(adm.UsuarioId.ToString(), adm.Nome, "Administrador");
            return RedirectToAction("Index", "Administrador");
        }

        // 2. Se não achou adm, tenta buscar um Cliente
        var cliente = _context.Clientes
            .FirstOrDefault(c => c.Email == email && c.Senha == senha);

        if (cliente != null)
        {
            await CriarSessao(cliente.UsuarioId.ToString(), cliente.Nome, "Cliente");
            return RedirectToAction("Index", "Home");
        }
        }
        
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Ocorreu um erro ao tentar fazer login.");
            return View();
        }
        // 3. Se chegou aqui, nada bateu
        ModelState.AddModelError("", "E-mail ou senha incorretos.");
        return View();
    }

    private async Task CriarSessao(string id, string nome, string perfil)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, id),
            new Claim(ClaimTypes.Name, nome),
            new Claim(ClaimTypes.Role, perfil) 
        };

        var identidade = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identidade));
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Login");
    }
    }
}