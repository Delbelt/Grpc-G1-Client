namespace clientGrpc.DTOs
{
    public class DispatchOrderDTO
    {
        public required int dispatchOrder {  get; set; }
        public required int idPurchaseOrder {  get; set; }
        public required string estimatedDate { get; set; }
    }
}
