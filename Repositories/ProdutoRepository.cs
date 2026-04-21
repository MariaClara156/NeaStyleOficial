using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Catalog;

namespace NeaStyleOficial.Repositories
{
    public class ProdutoRepository
    {
        private readonly NeaStyleContext context;
        // CREATE
        public void Criar(Produto produto)
        {
            {
                context.Produtos.Add(produto);
                context.SaveChanges();
            }
        }

        // READ - busca todos
        public List<Produto> BuscarTodos()
        {
            {
                return context.Produtos.ToList();
            }
        }

        // READ - busca por categoria
        public List<Produto> BuscarPorCategoria(CategoriaProduto categoria)
        {
            {
                // Filtra os produtos pela categoria (Masculino, Feminino, Infantil)
                return context.Produtos
                    .Where(p => p.CategoriaProduto == categoria)
                    .ToList();
            }
        }

        public Produto BuscarPorId(long ProdutoId)
        {
            {
                return context.Produtos.Find(ProdutoId);
            }
        }

        // UPDATE
        public void Atualizar(Produto produto)
        {
            {
                context.Produtos.Update(produto);
                context.SaveChanges();
            }
        }

        // DELETE
        public void Deletar(long ProdutoId)
        {
            {
                var produto = context.Produtos.Find(ProdutoId);
                if (produto != null)
                {
                    context.Produtos.Remove(produto);
                    context.SaveChanges();
                }
            }
        }
    }
}