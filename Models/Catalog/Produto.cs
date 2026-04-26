using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Models.Sales;

namespace NeaStyleOficial.Models.Catalog
{
    public class Produto
    {
        public long ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal Preco { get; set; }
        public string ImagemUrl { get; set; }
        public TamanhoProduto TamanhoProduto { get; set; }
        public CategoriaProduto CategoriaProduto { get; set; }
        public TipoProduto TipoProduto { get; set; }
        public List<ItemConjunto> ItensConjunto { get; set; } = new();
        public List<ItemConjunto> ItensFavorito { get; set; } = new();
        public List<Pedido> Pedidos { get; set; } = new();

        public Produto() { }
        public Produto(string nome, decimal precoCusto,decimal preco,  TamanhoProduto tamanho, CategoriaProduto categoria, TipoProduto tipo)
        {
            Nome = nome;
            PrecoCusto = precoCusto;
            Preco = preco;
            TamanhoProduto = tamanho;
            CategoriaProduto = categoria;
            TipoProduto = tipo;
        }
    }
}

