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
            _repo.Criar(produto);
        }
        public void CadastrarVariacao(ProdutoVariacao variacao)
        {
            _repo.CriarVariacao(variacao);
        }
        // -------------------BUSCAS-------------------//
        public List<Produto> BuscarTodos() => _repo.BuscarTodos();
        public List<Produto> BuscarPorNome(string nome) => _repo.BuscarPorNome(nome);
        public ProdutoVariacao BuscarVariacaoPorId(long produtoVariacaoId)
        {
            var variacao = _repo.BuscarVariacaoPorId(produtoVariacaoId);
            if (variacao == null)
                throw new Exception("Variação de produto não encontrada!");
            return variacao;
        }
        public Produto BuscarPorId(long produtoId) 
        {
            var produto = _repo.BuscarPorId(produtoId);
            if (produto == null)
                throw new Exception("Produto não encontrado!");
            return produto;
        }

        public int CalcularEstoqueTotal(long produtoId)
        {
            var produto = _repo.BuscarPorId(produtoId);
            if (produto == null)
                throw new Exception("Produto não encontrado!");
            return produto.Variacoes.Sum(v => v.Estoque);
        }
        // -------------------FILTRAR-------------------//
        public List<Produto> Filtrar(string? nome, TamanhoProduto? tamanho, CorProduto? cor, TipoProduto? tipo, CategoriaProduto? categoria) => _repo.Filtrar(nome, tamanho, cor, tipo, categoria);
        public void Atualizar(Produto produto) => _repo.Atualizar(produto);
        public void Deletar(long produtoId) => _repo.Deletar(produtoId);
    }
}
