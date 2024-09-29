namespace clientGrpc.DTOs
{
    public class ProductDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public byte[] Photo { get; set; }
        public string Color { get; set; }
        public bool Active { get; set; }
    }
}
