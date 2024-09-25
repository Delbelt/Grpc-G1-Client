namespace clientGrpc.DTOs
{
    public class AuthDTO
    {
        public required string Token {  get; set; }
        public required string UserName { get; set; }
        public required List<string> Roles { get; set; }
    }
}
