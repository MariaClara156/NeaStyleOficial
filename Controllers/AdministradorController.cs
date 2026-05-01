using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Services;
using Microsoft.AspNetCore.Authorization;

namespace NeaStyleOficial.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdministradorController : Controller
    {
        private readonly ProdutoService _produtoService;
        private readonly ClienteService _clienteService;
        private readonly PedidoService _pedidoService;

        public AdministradorController(ProdutoService produtoService, ClienteService clienteService, PedidoService pedidoService)
        {
            _produtoService = produtoService;
            _clienteService = clienteService;
            _pedidoService = pedidoService;
        }

        // Dashboard
        public IActionResult Index()
        {
            return View();
        }
        /* CRUD Produtos */
        public IActionResult ListarProdutos()
        {
            var produtos = _produtoService.BuscarTodos();
            return View(produtos);
        }   
        
        public IActionResult GerenciarProdutos()
        {
            ViewBag.Categoria = Enum.GetValues(typeof(CategoriaProduto))
                .Cast<CategoriaProduto>()
                .Select(e => new SelectListItem { 
                    Value = ((int)e).ToString(), 
                    Text = e.ToString() 
                }).ToList();
            
            ViewBag.Tipo = Enum.GetValues(typeof(TipoProduto))
                .Cast<TipoProduto>()
                .Select(e => new SelectListItem { 
                    Value = ((int)e).ToString(), 
                    Text = e.ToString() 
                }).ToList();

            ViewBag.Tamanho = Enum.GetValues(typeof(TamanhoProduto))
                .Cast<TamanhoProduto>()
                .Select(e => new SelectListItem { 
                    Value = ((int)e).ToString(), 
                    Text = e.ToString() 
                }).ToList();
            return View();
        }

        [HttpPost]
        // 1. Adicionado async Task para permitir o upload de arquivo
        public async Task<IActionResult> GerenciarProdutos(Produto produto, IFormFile fotoArquivo) 
        {
            try
            {
                if (fotoArquivo != null && fotoArquivo.Length > 0)
                {
                    string nomeArquivo = Guid.NewGuid().ToString() + "_" + fotoArquivo.FileName;
                    
                    // Directory.GetCurrentDirectory() pega a raiz do projeto
                    string caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "produtos");
                    
                    // Garante que a pasta existe, se não existir, o C# cria
                    if (!Directory.Exists(caminhoPasta)) Directory.CreateDirectory(caminhoPasta);

                    string caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

                    using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                    {
                        // Aqui o 'await' precisa do 'async' lá em cima na assinatura do método
                        await fotoArquivo.CopyToAsync(stream);
                    }

                    produto.ImagemUrl = nomeArquivo;
                }

                _produtoService.CadastrarProduto(produto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Se der erro, precisamos recarregar os Enums para a View não quebrar
                CarregarEnumsNoViewBag();
                ModelState.AddModelError("", ex.Message);
                return View(produto);
            }
        }

        // Método auxiliar para não repetir código de Enums
        private void CarregarEnumsNoViewBag()
        {
            ViewBag.Categorias = Enum.GetValues(typeof(CategoriaProduto))
                                    .Cast<CategoriaProduto>()
                                    .Select(e => new SelectListItem { Value = e.ToString(), Text = e.ToString() });
            ViewBag.Tipo = Enum.GetValues(typeof(TipoProduto))
                                    .Cast<TipoProduto>()
                                    .Select(e => new SelectListItem { Value = e.ToString(), Text = e.ToString() });
            ViewBag.Tamanho = Enum.GetValues(typeof(TamanhoProduto))
                                   .Cast<TamanhoProduto>()
                                   .Select(e => new SelectListItem { Value = e.ToString(), Text = e.ToString() });

        }

        public IActionResult EditarProduto(long UsuarioId)
        {
            var produto = _produtoService.BuscarPorId(UsuarioId);
            if (produto == null)
                return NotFound();
            return View(produto);
        }
        [HttpPost]
        public IActionResult EditarProduto(Produto produto)
        {
            _produtoService.Atualizar(produto);
            return RedirectToAction("Index");
        }

        public IActionResult DeletarProduto(long ProdutoId)
        {
            var produto = _produtoService.BuscarPorId(ProdutoId);
            if (produto == null)
                return NotFound();
            _produtoService.Deletar(ProdutoId);
            return RedirectToAction("Index");
        }

        /* Read Pedidos */
        public IActionResult GerenciarPedidos()
        {
            var pedidos = _pedidoService.VerTodosPedidos();
            return View(pedidos);
        }
        // Relatório de vendas
        public IActionResult Relatorio()
        {
            var pedidos = _pedidoService.VerTodosPedidos();
            return View(pedidos);
        }

        /* Read, Delete Clientes */
        public IActionResult GerenciarClientes()
        {
            var clientes = _clienteService.BuscarTodos();
            return View(clientes);
        }
        public IActionResult DeletarCliente(long UsuarioId)
        {
            var cliente = _clienteService.BuscarPorId(UsuarioId);
            if (cliente == null)
                return NotFound();
            _clienteService.Deletar(UsuarioId);
            return RedirectToAction("GerenciarClientes");
        }
    }
}