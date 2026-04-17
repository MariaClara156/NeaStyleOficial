using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Repositories;

namespace NeaStyleOficial.Services
{
    public class ProdutoService
    {
        private ProdutoRepository _repo = new ProdutoRepository();

        public void CadastrarProduto(Produto produto)
        {
            // Regra: preço não pode ser zero ou negativo
            if (produto.Preco <= 0)
                throw new Exception("Preço deve ser maior que zero!");

            _repo.Criar(produto);
        }

        public List<Produto> BuscarTodos() => _repo.BuscarTodos();

        public List<Produto> BuscarPorCategoria(Produto.CategoriaProduto categoria)
            => _repo.BuscarPorCategoria(categoria);

        public Produto BuscarPorId(long id) 
        {
            using (var context = new NeaStyleContext())
            {
                var produto = context.Produtos.Find(id);
                if (produto == null)
                    throw new Exception("Produto não encontrado!");
                return produto;
            }
        }

        public void Atualizar(Produto produto) => _repo.Atualizar(produto);

        public void Deletar(long id) => _repo.Deletar(id);
    }
}