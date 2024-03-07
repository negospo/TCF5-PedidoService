using Application.DTOs.External.PagamentoService;

namespace Infrastructure.RabbitMQ
{
    public class PedidoPagamento : Application.Interfaces.RabbitMQ.IPedidoPagamentoMessage
    {
        public void SendToQueue(PedidoPagamentoRequest pedidoPagamento)
        {
            RabbitMQ.Connection.SendMessage("order-payment", System.Text.Json.JsonSerializer.Serialize(pedidoPagamento));
        }
    }
}
