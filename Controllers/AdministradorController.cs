using Microsoft.AspNetCore.Mvc;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Models.Sales;

namespace NeaStyleOficial.Controllers
{
    public class AdministradorController : Controller
    {
        // Dashboard - página inicial do ADM após login
        public IActionResult Index()
        {
            return View(); // só abre o painel por enquanto
        }
    }
}