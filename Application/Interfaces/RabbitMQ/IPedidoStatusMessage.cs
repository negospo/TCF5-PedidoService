namespace Application.Interfaces.RabbitMQ
{
    public interface IPedidoStatusMessage
    {
        public void SendToQueue(DTOs.External.StatusService.PedidoStatusRequest pedidoStatus);
    }
}
