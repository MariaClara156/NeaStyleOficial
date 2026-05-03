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

        // CREATE
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
                .Include(p => p.Variacoes) // ← aqui também!
                .ToList();
        }
        // READ - busca por ID da variação
        public ProdutoVariacao BuscarVariacaoPorId(long ProdutoVariacaoId)
        {
            return _context.ProdutoVariacoes
                .Include(v => v.Produto)
                .FirstOrDefault(v => v.ProdutoVariacaoId == ProdutoVariacaoId);
        }
        // READ - busca por ID do produto
        public Produto BuscarPorId(long produtoId) 
        {
            return _context.Produtos
                // 1. Você inclui a PROPRIEDADE DE NAVEGAÇÃO (a lista), não o ID!
                .Include(p => p.Variacoes) 
                // 2. Você busca pelo ID do Produto
                .FirstOrDefault(p => p.ProdutoId == produtoId);
        }
        // READ - busca por nome
        public List<Produto> BuscarPorNome(string nome)
        {
            return _context.Produtos
                .Include(p => p.Variacoes) // Inclui as variações para buscar por nome
                .Where(p => p.Nome.Contains(nome)) // Busca produtos cujo nome contenha a string fornecida
                .ToList();
        }
        //READ - Calcular estoque total do produto
        public int CalcularEstoqueTotal(long produtoId)
        {
            var produto = _context.Produtos
                .Include(p => p.Variacoes) // Inclui as variações para calcular o estoque total
                .FirstOrDefault(p => p.ProdutoId == produtoId);

            if (produto == null)
                return 0; // Produto não encontrado, retorna 0

            return produto.Variacoes.Sum(v => v.Estoque); // Soma o estoque de todas as variações
        }
        //------------------------FILTROS------------------------//
        public List<Produto> Filtrar(string? nome, TamanhoProduto? tamanho, CorProduto? cor, TipoProduto? tipo, CategoriaProduto? categoria)
        {
            var query = _context.Produtos
                .Include(p => p.Variacoes)
                .AsQueryable();

            if (!string.IsNullOrEmpty(nome))
            {
                var nomeFiltro = nome.ToLower();
                query = query.Where(p => p.Nome.ToLower().Contains(nomeFiltro));
            }
            if (tamanho.HasValue)
                query = query.Where(p => p.Variacoes.Any(v => v.Tamanho == tamanho.Value));
            if (cor.HasValue)
                query = query.Where(p => p.Variacoes.Any(v => v.Cor == cor.Value));
            if (tipo.HasValue)
                query = query.Where(p => p.Tipo == tipo.Value);
            if (categoria.HasValue)
                query = query.Where(p => p.Categoria == categoria.Value);

            return query.ToList();
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
            var produto = _context.Produtos
            .Include(p => p.Variacoes)
            .FirstOrDefault(p => p.ProdutoId == ProdutoId);

            if (produto != null)
            {
                _context.ProdutoVariacoes.RemoveRange(produto.Variacoes);
                _context.Produtos.Remove(produto);
                _context.SaveChanges();
            }
        }
    }
}
