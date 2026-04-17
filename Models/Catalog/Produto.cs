using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Models.Sales;

namespace NeaStyleOficial.Models.Catalog
{
    public class Produto
    {
        public long ProdutoID { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public decimal PrecoCusto { get; set; }
        //colocar propriedade para Imagem do produto depois
        public enum TamanhoProduto
        {
            P,
            M,
            G,
            GG
        }
        public TamanhoProduto Tamanho { get; set; }
        public decimal Preco { get; set; }
        public CategoriaProduto Categoria { get; set; }
        public enum CategoriaProduto
        {
            Masculino,
            Feminino,
            Infantil
        }
        public List<ItemConjunto> ItensConjunto { get; set; } = new();
        public List<ItemConjunto> ItensFavorito { get; set; } = new();
        public List<Pedido> Pedidos { get; set; } = new();

        public Produto(string nome, string tipo,decimal precoCusto, TamanhoProduto tamanho, decimal preco, CategoriaProduto categoria)
        {
            Nome = nome;
            Tipo = tipo;
            PrecoCusto = precoCusto;
            Tamanho = tamanho;
            Preco = preco;
            Categoria = categoria;
        }
    }
}

