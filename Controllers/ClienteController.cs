using Microsoft.AspNetCore.Mvc;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Services;

namespace NeaStyleOficial.Controllers
{
    public class ClienteController : Controller
    {
        // Controller conversa só com o Service
        private readonly ClienteService _service;

        // ASP.NET injeta o service automaticamente
        public ClienteController(ClienteService service)
        {
            _service = service;
        }

        // READ - exibe lista de clientes
        // Rota: /Cliente/Index
        public async Task<IActionResult> Index()
        {
            var clientes = _service.BuscarTodos();
            return View(clientes); // manda os dados pra View exibir
        }

        // READ - exibe formulário de cadastro
        // Rota: /Cliente/CadastrarCliente
        public IActionResult CadastrarCliente()
        {
            return View(); // só abre a página vazia
        }

        // CREATE - recebe os dados do formulário e salva
        // Rota: POST /Cliente/CadastrarCliente
        [HttpPost]
        public IActionResult CadastrarCliente(Cliente cliente)
        {
            try
            {
                _service.CadastrarCliente(cliente);
                // redireciona pra lista após salvar
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // se der erro no Service, mostra na tela
                ModelState.AddModelError("", ex.Message);
                return View(cliente);
            }
        }

        // UPDATE - exibe formulário preenchido pra editar
        // Rota: /Cliente/Editar/1
        public IActionResult Editar(long UsuarioId)
        {
            var cliente = _service.BuscarPorId(UsuarioId);
            return View(cliente);
        }

        // UPDATE - recebe os dados editados e atualiza
        // Rota: POST /Cliente/Editar
        [HttpPost]
        public IActionResult Editar(Cliente cliente)
        {
            try
            {
                _service.Atualizar(cliente);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(cliente);
            }
        }

        // DELETE - remove cliente
        // Rota: /Cliente/Deletar/1
        public IActionResult Deletar(long UsuarioId)
        {
            _service.Deletar(UsuarioId);
            return RedirectToAction("Index");
        }
    }
}