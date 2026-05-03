using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Models.Catalog;

namespace NeaStyleOficial.Models.Catalog
{
    public class Produto
    {
        public long ProdutoId { get; set; }

        public string Nome { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public string ImagemUrl { get; set; }

        public CategoriaProduto Categoria { get; set; }
        public TipoProduto Tipo { get; set; }
        public List<ProdutoVariacao> Variacoes { get; set; } = new();
        public List<Pedido> Pedidos { get; set; } = new();

        public Produto() { }
        
        public Produto(string nome, decimal precoCusto,decimal preco, string Descricao, string ImagemUrl, CategoriaProduto categoria, TipoProduto tipo)
        {
            Nome = nome;
            PrecoCusto = precoCusto;
            Preco = preco;
            Descricao = Descricao;
            ImagemUrl = ImagemUrl;
            Categoria = categoria;
            Tipo = tipo;
        }
    }
}

