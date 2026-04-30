using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Catalog;

namespace NeaStyleOficial.Repositories
{
    public class ProdutoRepository
    {
        private readonly NeaStyleContext _context;
        public ProdutoRepository(NeaStyleContext context)
        {
            _context = context;
        }
        // CREATE
        public void Criar(Produto produto)
        {
            
                _context.Produtos.Add(produto);
                _context.SaveChanges();
            
        }

        // READ - busca todos
        public List<Produto> BuscarTodos()
        {
            return _context.Produtos.ToList();
        }

        // READ - busca por categoria
        public List<Produto> BuscarPorCategoria(CategoriaProduto categoria)
        {
            
                // Filtra os produtos pela categoria (Masculino, Feminino, Infantil)
                return _context.Produtos
                    .Where(p => p.CategoriaProduto == categoria)
                    .ToList();
            
        }

        public Produto BuscarPorId(long ProdutoId)
        {
            
                return _context.Produtos.Find(ProdutoId);
            
        }

        // UPDATE
        public void Atualizar(Produto produto)
        {
            
                _context.Produtos.Update(produto);
                _context.SaveChanges();
            
        }

        // DELETE
        public void Deletar(long ProdutoId)
        {
                var produto = _context.Produtos.Find(ProdutoId);
                if (produto != null)
                {
                    _context.Produtos.Remove(produto);
                    _context.SaveChanges();   
            }
        }
    }
}