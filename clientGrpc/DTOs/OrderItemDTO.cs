namespace clientGrpc.DTOs
{
    public class OrderItemDTO
    {
        public required string code { get; set; }
        public required string color { get; set; }
        public required string size { get; set; }
        public required int quantity { get; set; } 
    }
}
