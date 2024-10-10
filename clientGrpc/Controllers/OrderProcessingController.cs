using clientGrpc.DTOs;
using clientGrpc.Services;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace clientGrpc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderProcessingController: ControllerBase
    {
        private readonly IOrderProcessingService OrderProcessingService;
        private readonly ILogger<IOrderProcessingService> _logger;

        public OrderProcessingController(IOrderProcessingService orderProcessingService, ILogger<IOrderProcessingService> logger)
        {
            OrderProcessingService = orderProcessingService;
            _logger = logger;
        }

        [HttpGet("run")]
        public async Task<IActionResult> Run()
        {
            try
            {
                var response = await OrderProcessingService.RunProcess();

                _logger.LogInformation("[OrderProcessingController][Run]: {message}", response);

                var responseDTO = new mainDTO { Content = response };

                return Ok(responseDTO);
            }

            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[OrderProcessingController][Run]: {error}", ex.Message);

                return StatusCode(500, responseDTO);
            }
        }
    }
}
