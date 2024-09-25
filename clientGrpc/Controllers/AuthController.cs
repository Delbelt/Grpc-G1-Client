using authProto;
using clientGrpc.DTOs;
using clientGrpc.Handlers;
using clientGrpc.Security;
using clientGrpc.Services;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace clientGrpc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<IAuthService> _logger;
        private readonly AuthInterceptor _authInterceptor;

        public AuthController(IAuthService authService, ILogger<IAuthService> logger, AuthInterceptor authInterceptor)
        {
            _authService = authService;
            _logger = logger;
            _authInterceptor = authInterceptor;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _authService.Login(request.Username, request.Password);

                string token = response.Token;

                _authInterceptor.SetToken(token);

                _logger.LogInformation("[AuthController][Login]: {token}", token);

                AuthDTO auth = new AuthDTO
                {
                    Token = response.Token,
                    UserName = response.UserName,
                    Roles = response.Roles
                };

                var responseDTO = new mainDTO { Content = auth };

                return CreatedAtAction(nameof(Login), new { id = response.UserName }, responseDTO);
            }

            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[AuthController][Login]: {error}", ex.Message);

                return GrpcExceptionHandler.HandleGrpcException(ex, responseDTO);
            }
        }
    }
}