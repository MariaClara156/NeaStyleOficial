using NeaStyleOficial.Models.Collections;

namespace NeaStyleOficial.Models.Catalog
{
    public class ProdutoVariacao
    {
        public long    ProdutoVariacaoId { get; set; }

        // Tamanho e Cor são enums para padronizar as variações dos produtos
        public TamanhoProduto Tamanho    { get; set; }
        public CorProduto     Cor        { get; set; }

        public int     Estoque           { get; set; }
        // Opcional: nem todas as variações precisam de imagem específica
        public string? ImagemUrl         { get; set; }
        public decimal Preco             { get; set; }
        public decimal PrecoCusto        { get; set; }

        // Relacionamento: cada variação pertence a um produto
        public long    ProdutoId         { get; set; }
        public Produto Produto           { get; set; }

        // Relacionamentos: uma variação pode estar em vários itens de conjunto
        public List<ItemConjunto> Itens  { get; set; } = new();

        public ProdutoVariacao() { }

        public ProdutoVariacao(
            TamanhoProduto tamanho,
            CorProduto     cor,
            int            estoque,
            decimal        preco,
            decimal        precoCusto,
            string?        imagemUrl = null)
        {
            // Validações básicas para garantir que os dados sejam consistentes
            if (estoque < 0)
                throw new ArgumentException("Estoque não pode ser negativo");

            if (preco <= 0)
                throw new ArgumentException("Preço deve ser maior que zero");

            if (precoCusto < 0)
                throw new ArgumentException("Preço de custo inválido");

            Tamanho    = tamanho;
            Cor        = cor;
            Estoque    = estoque;
            Preco      = preco;
            PrecoCusto = precoCusto;
            ImagemUrl  = imagemUrl;
        }
    }
}