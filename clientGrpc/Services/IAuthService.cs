using clientGrpc.DTOs;

namespace clientGrpc.Services
{
    public interface IAuthService
    {
        Task<AuthDTO> Login(string username, string password);
    }
}
