using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Repositories;
using NeaStyleOficial.ViewModels.Catalog;

// ALTERADO: removidos usings de NeaStyleOficial.Data — não utilizado diretamente nesta classe
namespace NeaStyleOficial.Services
{
    public class ProdutoService
    {
        private readonly ProdutoRepository _repo;

        public ProdutoService(ProdutoRepository repo)
        {
            _repo = repo;
        }

        // CREATE
        public void CadastrarProduto(Produto produto)   => _repo.Criar(produto);
        public void CadastrarVariacao(ProdutoVariacao v) => _repo.CriarVariacao(v);

        // READ
        public List<Produto> BuscarTodos()                    => _repo.BuscarTodos();
        public List<Produto> BuscarPorNome(string nome)       => _repo.BuscarPorNome(nome);

        public Produto BuscarPorId(long produtoId)
        {
            var produto = _repo.BuscarPorId(produtoId);
            if (produto == null)
                throw new Exception("Produto não encontrado!");
            return produto;
        }

        public ProdutoVariacao BuscarVariacaoPorId(long produtoVariacaoId)
        {
            var variacao = _repo.BuscarVariacaoPorId(produtoVariacaoId);
            if (variacao == null)
                throw new Exception("Variação de produto não encontrada!");
            return variacao;
        }

        public List<CatalogoProdutoViewModel> ObterCatalogo()
        {
            var produtos = _repo.BuscarTodos();

            return produtos.Select(p => new CatalogoProdutoViewModel
            {
                ProdutoId   = p.ProdutoId,
                Nome        = p.Nome,
                Descricao   = p.Descricao,
                Categoria  = p.Categoria,
                MenorPreco  = p.Variacoes
                                .Where(v => v.Estoque > 0)
                                .OrderBy(v => v.Preco)
                                .FirstOrDefault()?.Preco ?? 0,
                // Primeira variação com imagem; se não houver, usa a primeira variação disponível
                ImagemUrl   = p.Variacoes.FirstOrDefault(v => !string.IsNullOrEmpty(v.ImagemUrl))?.ImagemUrl
                                ?? p.Variacoes.FirstOrDefault()?.ImagemUrl,
                Variacoes   = p.Variacoes.ToList()
            }).ToList();
        }

        public int CalcularEstoqueTotal(long produtoId)
        {
            var produto = _repo.BuscarPorId(produtoId);
            if (produto == null)
                throw new Exception("Produto não encontrado!");
            return produto.Variacoes.Sum(v => v.Estoque);
        }

        // FILTRAR
        public List<Produto> Filtrar(string? nome, TamanhoProduto? tamanho, CorProduto? cor, TipoProduto? tipo, CategoriaProduto? categoria)
            => _repo.Filtrar(nome, tamanho, cor, tipo, categoria);

        // UPDATE
        public void Atualizar(Produto produto)                    => _repo.Atualizar(produto);
        public void AtualizarVariacao(ProdutoVariacao variacao)   => _repo.AtualizarVariacao(variacao);

        // DELETE
        public void Deletar(long produtoId)                => _repo.Deletar(produtoId);
        public void DeletarVariacao(long produtoVariacaoId) => _repo.DeletarVariacao(produtoVariacaoId);
    }
}