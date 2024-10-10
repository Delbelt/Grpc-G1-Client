using clientGrpc.DTOs;
using clientGrpc.Handlers;
using clientGrpc.Services;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace clientGrpc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DispatchOrderController : ControllerBase
    {
        private readonly IDispatchOrderService _dispatchOrderService;
        private readonly ILogger<IDispatchOrderService> _logger;

        public DispatchOrderController(IDispatchOrderService dispatchOrderService, ILogger<IDispatchOrderService> logger)
        {
            _dispatchOrderService = dispatchOrderService;
            _logger = logger;
        }

        [HttpGet("{dispatchOrder_id}")]
        public async Task<IActionResult> GetPurchaseByDispatchOrder(int dispatchOrder_id)
        {
            try
            {
                var response = await _dispatchOrderService.GetDispatchById(dispatchOrder_id);

                _logger.LogInformation("[DispatchOrderController][GetPurchaseByDispatchOrder]: {message}", response.ToString());

                DispatchOrderDTO dispatchOrderDTO = new DispatchOrderDTO
                {
                    dispatchOrder = response.DispatchOrder,
                    idPurchaseOrder = response.IdPurchaseOrder,
                    estimatedDate = TimestampGrpcConverter.ToFormattedString(TimestampGrpcConverter.ToDateTime(response.EstimatedDate)),
                };

                var responseDTO = new mainDTO
                {
                    Content = dispatchOrderDTO,
                };

                return Ok(responseDTO);
            }

            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[UserController][GetUserById]: {error}", ex.Message);

                return GrpcExceptionHandler.HandleGrpcException(ex, responseDTO);
            }
        }
    }
}
