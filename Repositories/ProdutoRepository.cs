using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Catalog;

namespace NeaStyleOficial.Repositories
{
    public class ProdutoRepository
    {
        // CREATE
        public void Criar(Produto produto)
        {
            using (var context = new NeaStyleContext())
            {
                context.Produtos.Add(produto);
                context.SaveChanges();
            }
        }

        // READ - busca todos
        public List<Produto> BuscarTodos()
        {
            using (var context = new NeaStyleContext())
            {
                return context.Produtos.ToList();
            }
        }

        // READ - busca por categoria
        public List<Produto> BuscarPorCategoria(Produto.CategoriaProduto categoria)
        {
            using (var context = new NeaStyleContext())
            {
                // Filtra os produtos pela categoria (Masculino, Feminino, Infantil)
                return context.Produtos
                    .Where(p => p.Categoria == categoria)
                    .ToList();
            }
        }

        public Produto BuscarPorId(long id)
        {
            using (var context = new NeaStyleContext())
            {
                return context.Produtos.Find(id);
            }
        }

        // UPDATE
        public void Atualizar(Produto produto)
        {
            using (var context = new NeaStyleContext())
            {
                context.Produtos.Update(produto);
                context.SaveChanges();
            }
        }

        // DELETE
        public void Deletar(long id)
        {
            using (var context = new NeaStyleContext())
            {
                var produto = context.Produtos.Find(id);
                if (produto != null)
                {
                    context.Produtos.Remove(produto);
                    context.SaveChanges();
                }
            }
        }
    }
}