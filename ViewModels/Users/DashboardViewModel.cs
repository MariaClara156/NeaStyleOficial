namespace NeaStyleOficial.ViewModels.Users
{
    public class DashboardViewModel
    {
        public int TotalPedidos { get; set; }
        public decimal TotalVendas { get; set; }
        public int TotalClientes { get; set; }
        public int TotalProdutos { get; set; }

        public List<ProdutoVendidoViewModel> ProdutosMaisVendidos { get; set; } = new();

        public List<VendasPeriodoViewModel> VendasPorMes { get; set; } = new();
    }

    public class ProdutoVendidoViewModel
    {
        public string NomeProduto { get; set; }
        public int QuantidadeVendida { get; set; }
        public decimal TotalGerado { get; set; }
    }

    public class VendasPeriodoViewModel
    {
        public string Mes { get; set; }
        public decimal Total { get; set; }
        public int Quantidade { get; set; }
    }
}