//Enum para padronizar status de pedido, pagamento, reembolso e metodos de pagamento
namespace NeaStyleOficial.Models.Sales
{
    public enum MetodoPagamento
    {
            CartaoCredito,
            CartaoDebito,
            Boleto,
            Pix
    }
    public enum StatusPagamento
    {
        Pendente,
        Processando,
        Aprovado,
        Recusado,
        Estornado
    }
    public enum StatusPedido
    {
        Pendente,
        Processando,
        Enviado,
        Entregue,
        Cancelado,
        Estornado
    }
    public enum StatusReembolso
    {
        Solicitado,
        EmAnalise,
        Aprovado,
        Recusado
    }
}