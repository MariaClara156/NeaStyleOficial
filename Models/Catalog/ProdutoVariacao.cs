using NeaStyleOficial.Models.Collections;

namespace NeaStyleOficial.Models.Catalog
{
    public class ProdutoVariacao
    {
        public long ProdutoVariacaoId { get; set; }

        public TamanhoProduto TamanhoProduto { get; set; }
        public CorProduto CorProduto { get; set; }

        public int Estoque { get; set; }

        // FK
        public long ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public List<ItemConjunto> ItensCarrinho { get; set; } = new();
        public List<ItemConjunto> ItensFavorito { get; set; } = new();
        public List<ItemConjunto> ItensConjunto { get; set; } = new();

        public ProdutoVariacao() { }
        public ProdutoVariacao(TamanhoProduto tamanho, CorProduto cor, int estoque)
        {
            TamanhoProduto = tamanho;
            CorProduto = cor;
            Estoque = estoque;
        }
    }
}