namespace Application.DTOs.External.PagamentoService
{
    public class PedidoPagamentoStatusMessage
    {
        public int PedidoId { get; set; }
        public Enums.PagamentoStatus PagamentoStatus { get; set; }
    }
}
