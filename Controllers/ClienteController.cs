using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Services;
using Microsoft.AspNetCore.Authorization;

namespace NeaStyleOficial.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class ClienteController : Controller
    {
        private readonly ClienteService _service;

        public ClienteController(ClienteService service)
        {
            _service = service;
        }

        // READ — lista de clientes
        public async Task<IActionResult> Index()
        {
            var clientes = _service.BuscarTodos();
            return View(clientes);
        }

        [AllowAnonymous]
        // GET — formulário de cadastro
        public IActionResult CadastrarCliente() => View();
        [AllowAnonymous]
        // POST — salva novo cliente
        [HttpPost]
        public IActionResult CadastrarCliente(Cliente cliente)
        {
            try
            {
                _service.CadastrarCliente(cliente);
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(cliente);
            }
        }

        // READ — perfil do cliente logado
        public IActionResult Perfil()
        {
            var clienteId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var cliente   = _service.BuscarPorId(clienteId);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        // GET — formulário de edição preenchido
        public IActionResult EditarPerfil(long usuarioId)
        {
            var cliente = _service.BuscarPorId(usuarioId);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        // POST — salva edição
        [HttpPost]
        public IActionResult EditarPerfil(Cliente clienteAtualizado)
        {
            // Verifica se os dados são válidos (DataAnnotations)
            if (!ModelState.IsValid)
            {
                return View(clienteAtualizado);
            }

            try
            {
                // Passa o objeto que veio da tela para o service salvar
                _service.Atualizar(clienteAtualizado);
                return RedirectToAction("Perfil");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao atualizar: " + ex.Message);
                return View(clienteAtualizado);
            }
        }
        // DELETE — remove cliente e redireciona
        public IActionResult Deletar(long usuarioId)
        {
            _service.Deletar(usuarioId);
            return RedirectToAction("Index");
        }
    }
}