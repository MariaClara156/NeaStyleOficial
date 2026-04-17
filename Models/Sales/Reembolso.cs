using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Models.Users;

namespace NeaStyleOficial.Models.Sales
{
    public class Reembolso
    {
        public long ReembolsoID { get; set; }
        public long PedidoID { get; set; }
        public Pedido Pedido { get; set; }
        public string Motivo { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public decimal ValorReembolso { get; set; }
        public StatusReembolso Status { get; set; }

        public Reembolso() { }
        public Reembolso(long pedidoID, string motivo, DateTime dataSolicitacao, decimal valorReembolso, StatusReembolso status)
        {
            PedidoID = pedidoID;
            Motivo = motivo;
            DataSolicitacao = dataSolicitacao;
            ValorReembolso = valorReembolso;
            Status = status;
        }
    }
}
