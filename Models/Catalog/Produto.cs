using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Models.Catalog;

namespace NeaStyleOficial.Models.Catalog
{
    public class Produto
    {
        public long ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        // Categoria e Tipo são enums para facilitar a classificação dos produtos
        public CategoriaProduto Categoria { get; set; }
        public TipoProduto Tipo { get; set; }
        // Relacionamentos: um produto pode ter várias variações e estar em vários pedidos (=new() para inicializar a lista vazia)
        public List<ProdutoVariacao> Variacoes { get; set; } = new();
        public List<Pedido> Pedidos { get; set; } = new();

        protected Produto() { }
        public Produto(string nome, string Descricao, CategoriaProduto categoria, TipoProduto tipo)
        {
            Nome = nome;
            Descricao = Descricao;
            Categoria = categoria;
            Tipo = tipo;
        }
    }
}

