using Application.DTOs.External.StatusService;

namespace Infrastructure.RabbitMQ
{
    public class PedidoStatus : Application.Interfaces.RabbitMQ.IPedidoStatusMessage
    {
        public void SendToQueue(PedidoStatusRequest pedidoStatus)
        {
            RabbitMQ.Connection.SendMessage("order-status", System.Text.Json.JsonSerializer.Serialize(pedidoStatus));
        }
    }
}
