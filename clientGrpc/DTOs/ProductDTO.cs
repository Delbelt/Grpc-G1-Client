namespace clientGrpc.DTOs
{
    public class ProductDTO
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Size { get; set; }
        public required string Photo { get; set; }
        public required string Color { get; set; }
        public required bool Active { get; set; }
    }
}
