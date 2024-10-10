using orderItemProto;

namespace clientGrpc.DTOs
{
    public class PurchaseOrderDTO
    {
        public required int IdPurchaseOrder { get; set; }
        public required string State { get; set; }
        public required string Observations { get; set; }
        public required DateTime RequestDate { get; set; }
        public List<OrderItemDTO>? OrderItems { get; set; }
    }
    public class PostPurchaseOrderRequestDto
    {
        public required string CodeStore { get; set; }
        public required List<OrderItemGrpc> Items { get; set; }
    }
}
