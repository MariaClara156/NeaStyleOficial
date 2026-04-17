using Microsoft.AspNetCore.Mvc;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Services;

namespace NeaStyleOficial.Controllers
{
    public class ClienteController : Controller
    {
        // Controller conversa só com o Service
        private ClienteService _service = new ClienteService();

        // READ - exibe lista de clientes
        // Rota: /Cliente/Index
        public IActionResult Index()
        {
            var clientes = _service.BuscarTodos();
            return View(clientes); // manda os dados pra View exibir
        }

        // READ - exibe formulário de cadastro
        // Rota: /Cliente/Criar
        public IActionResult Criar()
        {
            return View(); // só abre a página vazia
        }

        // CREATE - recebe os dados do formulário e salva
        // Rota: POST /Cliente/Criar
        [HttpPost]
        public IActionResult Criar(Cliente cliente)
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
        public IActionResult Editar(long id)
        {
            var cliente = _service.BuscarPorId(id);
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
        public IActionResult Deletar(long id)
        {
            _service.Deletar(id);
            return RedirectToAction("Index");
        }
    }
}