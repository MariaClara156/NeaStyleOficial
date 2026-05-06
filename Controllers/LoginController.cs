using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Data;

namespace NeaStyleOficial.Controllers
{
    public class LoginController : Controller
    {
    private readonly NeaStyleContext _context;
    private readonly PasswordHasher<Usuario> hasher = new PasswordHasher<Usuario>();

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
            var adm = _context.Administradores
                .FirstOrDefault(a => a.Email == email);

            if (adm != null)
            {
                var resultado = hasher.VerifyHashedPassword(adm, adm.Senha, senha);

                if (resultado == PasswordVerificationResult.Success)
                {
                    await CriarSessao(adm.UsuarioId.ToString(), adm.Nome, "Administrador");
                    return RedirectToAction("Index", "Administrador");
                }
            }
            var cliente = _context.Clientes
                .FirstOrDefault(c => c.Email == email);

            if (cliente != null)
            {
                var resultado = hasher.VerifyHashedPassword(cliente, cliente.Senha, senha);

                if (resultado == PasswordVerificationResult.Success)
                {
                    await CriarSessao(cliente.UsuarioId.ToString(), cliente.Nome, "Cliente");
                    return RedirectToAction("Index", "Produto");
                }
            }

            ModelState.AddModelError("", "E-mail ou senha incorretos.");
            return View();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View();
        }
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