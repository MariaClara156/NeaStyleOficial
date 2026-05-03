using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Models.Catalog;

namespace NeaStyleOficial.Models.Catalog
{
    public class ProdutoVariacao
    {
        public long ProdutoVariacaoId { get; set; }
        public TamanhoProduto Tamanho { get; set; }
        public CorProduto Cor { get; set; }
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
            Tamanho = tamanho;
            Cor = cor;
            Estoque = estoque;
        }
    }
}