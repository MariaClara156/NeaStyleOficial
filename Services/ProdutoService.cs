using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Repositories;

namespace NeaStyleOficial.Services
{
    public class ProdutoService
    {
        private readonly ProdutoRepository _repo;

        public ProdutoService(ProdutoRepository repo)
        {
            _repo = repo;
        }

        public void CadastrarProduto(Produto produto)
        {
            // Regra: preço não pode ser zero ou negativo
            if (produto.Preco <= 0)
                throw new Exception("Preço deve ser maior que zero!");

            _repo.Criar(produto);
        }

        public List<Produto> BuscarTodos() => _repo.BuscarTodos();

        public List<Produto> BuscarPorCategoria(CategoriaProduto categoria)=> _repo.BuscarPorCategoria(categoria);

        public Produto BuscarPorId(long ProdutoId) 
        {
            var produto = _repo.BuscarPorId(ProdutoId);
            if (produto == null)
                throw new Exception("Produto não encontrado!");
            return produto;
            
        }

        public void Atualizar(Produto produto) => _repo.Atualizar(produto);

        public void Deletar(long ProdutoId) => _repo.Deletar(ProdutoId);
    }
}
