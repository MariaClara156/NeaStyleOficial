using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Collections;

namespace NeaStyleOficial.Models.Sales
{
    public class Reembolso
    {
        public long ReembolsoId { get; set; }
        // Um reembolso pertence a um pedido
        public long PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public string Motivo { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public decimal ValorReembolso { get; set; }
        // ENUM para status do reembolso
        public StatusReembolso Status { get; set; }

        protected Reembolso() { }
        public Reembolso(long pedidoId, string motivo, DateTime dataSolicitacao, decimal valorReembolso, StatusReembolso status)
        {
            if (valorReembolso < 0)
                throw new ArgumentException("Valor de reembolso não pode ser negativo");
            PedidoId = pedidoId;
            Motivo = motivo;
            DataSolicitacao = dataSolicitacao;
            ValorReembolso = valorReembolso;
            Status = status;
        }
    }
}
