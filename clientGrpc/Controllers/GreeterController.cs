using clientGrpc.DTOs;
using clientGrpc.Services;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace clientGrpc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GreeterController : ControllerBase
    {
        private readonly IGreeterService _greeterService;
        private readonly ILogger<IGreeterService> _logger;

        public GreeterController(IGreeterService greeterService, ILogger<IGreeterService> logger)
        {
            _greeterService = greeterService;
            _logger = logger;
        }

        [HttpGet("hello")]
        public async Task<IActionResult> SayHello(string message)
        {
            try
            {
                var response = await _greeterService.SayHello(message);

                _logger.LogInformation("[GreeterController][SayHello]: {message}", message);

                var responseDTO = new mainDTO { Content = response };

                return Ok(responseDTO);
            }

            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[UserController][GetUserById]: {error}", ex.Message);

                return StatusCode(500, responseDTO);
            }
        }
    }
}
