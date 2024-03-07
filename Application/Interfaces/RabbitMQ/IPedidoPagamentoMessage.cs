namespace Application.Interfaces.RabbitMQ
{
    public interface IPedidoPagamentoMessage
    {
        public void SendToQueue(DTOs.External.PagamentoService.PedidoPagamentoRequest pedidoPagamento);
    }
}
