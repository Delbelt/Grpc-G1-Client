using clientGrpc.DTOs;
using clientGrpc.Handlers;
using clientGrpc.Services;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace clientGrpc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        private readonly ILogger<IOrderItemService> _logger;

        public OrderItemController(IOrderItemService orderItemService, ILogger<IOrderItemService> logger)
        {
            _orderItemService = orderItemService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItemById(int id)
        {
            try
            {
                var response = await _orderItemService.GetOrderItemByCode(id);

                _logger.LogInformation("[OrderItemController][GetOrderItemByCode]: {message}", response.ToString());

                var responseDTO = new mainDTO
                {
                    Content = response,
                };

                return Ok(responseDTO);
            }

            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[OrderItemController][GetOrderItemByCode]: {error}", ex.Message);

                return GrpcExceptionHandler.HandleGrpcException(ex, responseDTO);
            }
        }

    }
}
