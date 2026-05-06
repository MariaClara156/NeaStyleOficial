using NeaStyleOficial.Data;
using NeaStyleOficial.Models.Catalog;
using Microsoft.EntityFrameworkCore;

namespace NeaStyleOficial.Repositories
{
    public class ProdutoRepository
    {
        private readonly NeaStyleContext _context;

        public ProdutoRepository(NeaStyleContext context)
        {
            _context = context;
        }

        // CREATE - Produto e variação são criados separadamente, já que um produto pode ter várias variações (tamanho, cor, etc.)
        public void Criar(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }
        public void CriarVariacao(ProdutoVariacao variacao)
        {
            _context.ProdutoVariacoes.Add(variacao);
            _context.SaveChanges();
        }

        // READ - busca todos
        public List<Produto> BuscarTodos()
        {
            return _context.Produtos
            //Include é necessário pra carregar as variações junto com o produto, já que são entidades relacionadas
                .Include(p => p.Variacoes)
                .ToList();
        }
        // READ - busca por ID da variação
        public ProdutoVariacao? BuscarVariacaoPorId(long produtoVariacaoId)
        {
            return _context.ProdutoVariacoes
                .Include(v => v.Produto)
                .FirstOrDefault(v => v.ProdutoVariacaoId == produtoVariacaoId);
        }
        // READ - busca por ID do produto
        public Produto? BuscarPorId(long produtoId) 
        {
            return _context.Produtos
                .Include(p => p.Variacoes) 
                .FirstOrDefault(p => p.ProdutoId == produtoId);
        }
        // READ - busca por nome
        public List<Produto> BuscarPorNome(string nome)
        {
            return _context.Produtos
                .Include(p => p.Variacoes)
                .Where(p => p.Nome.Contains(nome))
                .ToList();
        }
        //READ - Calcular estoque total do produto
        public int CalcularEstoqueTotal(long produtoId)
        {
            var produto = _context.Produtos
                .Include(p => p.Variacoes)
                .FirstOrDefault(p => p.ProdutoId == produtoId);
            // Se o produto não for encontrado, retorna 0 como estoque total
            if (produto == null)
                return 0;
            return produto.Variacoes.Sum(v => v.Estoque);
        }
        //------------------------FILTROS------------------------//
        public List<Produto> Filtrar(string? nome, TamanhoProduto? tamanho, CorProduto? cor, TipoProduto? tipo, CategoriaProduto? categoria)
        {
            // Começa com a consulta base, incluindo as variações pra poder filtrar por atributos delas
            var query = _context.Produtos
                .Include(p => p.Variacoes)
                // AsQueryable() é importante pra permitir construir a consulta dinamicamente com os filtros opcionais
                .AsQueryable();
            // Aplica cada filtro somente se ele tiver sido fornecido (não for nulo ou vazio)
            if (!string.IsNullOrEmpty(nome))
                query = query.Where(p => p.Nome.ToLower().Contains(nomeFiltro));
            if (tamanho.HasValue)
                query = query.Where(p => p.Variacoes.Any(v => v.Tamanho == tamanho.Value));
            if (cor.HasValue)
                query = query.Where(p => p.Variacoes.Any(v => v.Cor == cor.Value));
            if (tipo.HasValue)
                query = query.Where(p => p.Tipo == tipo.Value);
            if (categoria.HasValue)
                query = query.Where(p => p.Categoria == categoria.Value);
            // Executa a consulta e retorna os resultados como uma lista
            return query.ToList();
        }
        // UPDATE
        public void Atualizar(Produto produto)
        {
            _context.Produtos.Update(produto);
            _context.SaveChanges();
        }
        // DELETE
        public void Deletar(long produtoId)
        {
            var produto = _context.Produtos
            .Include(p => p.Variacoes)
            .FirstOrDefault(p => p.ProdutoId == produtoId);

            if (produto != null)
            {
                _context.Produtos.Remove(produto);
            }
        }
    }
}
