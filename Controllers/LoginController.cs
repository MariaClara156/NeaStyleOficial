using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Services;
using NeaStyleOficial.Data;

namespace NeaStyleOficial.Controllers
{
    public class LoginController : Controller
    {
        private readonly ClienteService _clienteService;
        private readonly AdministradorService _administradorService;

        public LoginController(ClienteService clienteService, AdministradorService admService)
        {
            _clienteService = clienteService;
            _administradorService = admService;
        }

        // GET - abre formulário
        public IActionResult Index() => View();

        // POST - processa login
        [HttpPost]
        public async Task<IActionResult> Index(string email, string senha)
        {
            // 1. Verifica se é Administrador
            var adm = _administradorService.BuscarPorEmail(email);
            if (adm != null && _administradorService.VerificarSenha(senha, adm.Senha))
            {
                await CriarSessao(adm.UsuarioId.ToString(), adm.Nome, "Administrador");
                return RedirectToAction("Index", "Administrador");
            }

            // 2. Verifica se é Cliente
            var cliente = _clienteService.BuscarPorEmail(email);
            if (cliente != null && _clienteService.VerificarSenha(senha, cliente.Senha))
            {
                await CriarSessao(cliente.UsuarioId.ToString(), cliente.Nome, "Cliente");
                return RedirectToAction("Index", "Home");
            }

            // 3. Nenhum encontrado
            ModelState.AddModelError("", "Email ou senha incorretos!");
            return View();
        }

        // Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        // Método auxiliar que cria a sessão
        private async Task CriarSessao(string id, string nome, string perfil)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Name, nome),
                new Claim(ClaimTypes.Role, perfil) // define se é Cliente ou Administrador
            };

            var identidade = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identidade));
        }
    }
}