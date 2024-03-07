
namespace Application.Implementations
{
    public class PedidoPagamentoMessageHandler : Interfaces.RabbitMQ.IPedidoPagamentoMessageHandler
    {
        Application.Interfaces.RabbitMQ.IPedidoStatusMessage _pedidoStatus;

        public PedidoPagamentoMessageHandler(Application.Interfaces.RabbitMQ.IPedidoStatusMessage pedidoStatus)
        {
            this._pedidoStatus = pedidoStatus;
        }

        public void HandleMessage(string message)
        {
            var paymentStatus = System.Text.Json.JsonSerializer.Deserialize<DTOs.External.PagamentoService.PedidoPagamentoStatusMessage>(message);
            if (paymentStatus.PagamentoStatus == Enums.PagamentoStatus.Aprovado)
                this._pedidoStatus.SendToQueue(new DTOs.External.StatusService.PedidoStatusRequest
                {
                    PedidoId = paymentStatus.PedidoId,
                    Status = Enums.PedidoStatus.Recebido
                });
        }
    }
}
