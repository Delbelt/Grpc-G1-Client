using authProto;
using clientGrpc.DTOs;
using clientGrpc.Services;

public class AuthService : IAuthService
{
    private readonly AuthGrpcService.AuthGrpcServiceClient _AuthGrpcService;
    public AuthService(AuthGrpcService.AuthGrpcServiceClient authGrpcService)
    {
        _AuthGrpcService = authGrpcService;
    }
    public async Task<AuthDTO> Login(string username, string password)
    {
        var loginRequest = new LoginRequest { Username = username, Password = password };

        var response = await _AuthGrpcService.LoginGrpcAsync(loginRequest);

        AuthDTO auth = new AuthDTO
        {
            Token = response.Token,
            UserName = response.Username,
            Roles = [.. response.Roles],
        };

        return auth;
    }
}
