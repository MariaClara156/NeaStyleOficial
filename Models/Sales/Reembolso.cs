using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Collections;

namespace NeaStyleOficial.Models.Sales
{
    public class Reembolso
    {
        public long ReembolsoId { get; set; }
        public long PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public string Motivo { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public decimal ValorReembolso { get; set; }
        public StatusReembolso Status { get; set; }

        public Reembolso() { }
        public Reembolso(long pedidoId, string motivo, DateTime dataSolicitacao, decimal valorReembolso, StatusReembolso status)
        {
            PedidoId = pedidoId;
            Motivo = motivo;
            DataSolicitacao = dataSolicitacao;
            ValorReembolso = valorReembolso;
            Status = status;
        }
    }
}
