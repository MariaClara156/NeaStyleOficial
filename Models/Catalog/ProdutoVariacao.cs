using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Models.Catalog;

namespace NeaStyleOficial.Models.Catalog
{
    public class ProdutoVariacao
    {
        public long ProdutoVariacaoId { get; set; }
        // Tamanho e Cor são enums para padronizar as variações dos produtos
        public TamanhoProduto Tamanho { get; set; }
        public CorProduto Cor { get; set; }
        public int Estoque { get; set; }
        // ImagemUrl é opcional, pois nem todas as variações precisam de uma imagem específica
        public string? ImagemUrl { get; set; }
        public decimal Preco { get; set; }
        public decimal PrecoCusto { get; set; }
        // Relacionamento: cada variação pertence a um produto
        public long ProdutoId { get; set; }
        public Produto Produto { get; set; }
        // Relacionamentos: uma variação pode estar em vários itens de carrinho, favoritos e conjuntos
        public List<ItemConjunto> ItensCarrinho { get; set; } = new();
        public List<ItemConjunto> ItensFavorito { get; set; } = new();
        public List<ItemConjunto> ItensConjunto { get; set; } = new();

        protected ProdutoVariacao() { }
        public ProdutoVariacao(
        TamanhoProduto tamanho,
        CorProduto cor,
        int estoque,
        decimal preco,
        decimal precoCusto,
        string? imagemUrl = null)
    {
        // Validações básicas para garantir que os dados sejam consistentes
        if (estoque < 0)
            throw new ArgumentException("Estoque não pode ser negativo");

        if (preco <= 0)
            throw new ArgumentException("Preço deve ser maior que zero");

        if (precoCusto < 0)
            throw new ArgumentException("Preço de custo inválido");

        Tamanho = tamanho;
        Cor = cor;
        Estoque = estoque;
        Preco = preco;
        PrecoCusto = precoCusto;
        ImagemUrl = imagemUrl;
    }
    }
}