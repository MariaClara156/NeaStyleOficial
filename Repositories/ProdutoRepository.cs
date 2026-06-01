using Microsoft.EntityFrameworkCore;
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

        // CREATE — produto e variação são criados separadamente, já que um produto pode ter várias variações
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

        // READ — busca todos (Include carrega as variações junto, pois são entidades relacionadas)
        public List<Produto> BuscarTodos()
        {
            return _context.Produtos
                .Include(p => p.Variacoes)
                .ToList();
        }

        // READ — busca por ID do produto
        public Produto? BuscarPorId(long produtoId)
        {
            return _context.Produtos
                .Include(p => p.Variacoes)
                .FirstOrDefault(p => p.ProdutoId == produtoId);
        }

        // READ — busca por ID da variação
        public ProdutoVariacao? BuscarVariacaoPorId(long produtoVariacaoId)
        {
            return _context.ProdutoVariacoes
                .Include(v => v.Produto)
                .FirstOrDefault(v => v.ProdutoVariacaoId == produtoVariacaoId);
        }

        // READ — busca por nome
        public List<Produto> BuscarPorNome(string nome)
        {
            return _context.Produtos
                .Include(p => p.Variacoes)
                .Where(p => p.Nome.Contains(nome))
                .ToList();
        }

        // FILTROS — aplica cada filtro somente se fornecido (não nulo/vazio)
        // AsQueryable() permite construir a consulta dinamicamente
        public List<Produto> Filtrar(string? nome, TamanhoProduto? tamanho, CorProduto? cor, TipoProduto? tipo, CategoriaProduto? categoria)
        {
            var query = _context.Produtos
                .Include(p => p.Variacoes)
                .AsQueryable();

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(p => p.Nome.ToLower().Contains(nome));

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

        public void AtualizarVariacao(ProdutoVariacao produtoVariacao)
        {
            _context.ProdutoVariacoes.Update(produtoVariacao);
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
                // Para cada variação, remove os itens de carrinho primeiro
                var variacaoIds = produto.Variacoes.Select(v => v.ProdutoVariacaoId).ToList();

                var itensCarrinho = _context.ItensConjunto
                    .Where(i => variacaoIds.Contains(i.ProdutoVariacaoId))
                    .ToList();

                if (itensCarrinho.Any())
                    _context.ItensConjunto.RemoveRange(itensCarrinho);

                _context.Produtos.Remove(produto);
                _context.SaveChanges();
            }
        }

        public void DeletarVariacao(long produtoVariacaoId)
        {
            var variacao = _context.ProdutoVariacoes
                .FirstOrDefault(p => p.ProdutoVariacaoId == produtoVariacaoId);

            if (variacao != null)
            {
                // Remove itens de carrinho que referenciam essa variação
                var itensCarrinho = _context.ItensConjunto
                    .Where(i => i.ProdutoVariacaoId == produtoVariacaoId)
                    .ToList();

                if (itensCarrinho.Any())
                    _context.ItensConjunto.RemoveRange(itensCarrinho);

                _context.ProdutoVariacoes.Remove(variacao);
                _context.SaveChanges();
            }
        }
    }
}