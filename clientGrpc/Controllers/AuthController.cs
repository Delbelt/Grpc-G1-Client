using authProto;
using clientGrpc.DTOs;
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
        private readonly AuthService _authService;
        private readonly ILogger<IGreeterService> _logger;
        private readonly AuthInterceptor _authInterceptor;

        public AuthController(AuthService authService, ILogger<IGreeterService> logger, AuthInterceptor authInterceptor)
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
                var token = await _authService.Login(request.Username, request.Password);

                _authInterceptor.SetToken(token);

                _logger.LogInformation("[AuthController][Login]: {token}", token);

                // TODO: DTO token
                var Token = new {Token = token};

                var responseDTO = new mainDTO { Content = Token };

                return Ok(responseDTO);
            }

            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[AuthController][Login]: {error}", ex.Message);

                if (ex.StatusCode.Equals(Grpc.Core.StatusCode.Unavailable))
                {
                    return StatusCode(500, responseDTO);
                }

                return Unauthorized(responseDTO);
            }
        }
    }
}