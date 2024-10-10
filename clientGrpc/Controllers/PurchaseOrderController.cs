using clientGrpc.DTOs;
using clientGrpc.Handlers;
using clientGrpc.Services;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace clientGrpc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly ILogger<IPurchaseOrderService> _logger;

        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService, ILogger<IPurchaseOrderService> logger)
        {
            _purchaseOrderService = purchaseOrderService;
            _logger = logger;
        }

        [HttpGet("{purchaseOrder_id}")]
        public async Task<IActionResult> GetPurchaseOrderById(int purchaseOrder_id)
        {
            try
            {
                var response = await _purchaseOrderService.GetPurchaseOrderById(purchaseOrder_id);

                _logger.LogInformation("[PurchaseOrderController][GetPurchaseOrderById]: {message}", response.ToString());                

                var responseDTO = new mainDTO
                {
                    Content = response,
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

        [HttpPost]
        public async Task<IActionResult> PostPurchaseOrder([FromBody] PostPurchaseOrderRequestDto request)
        {
            try
            {
                var response = await _purchaseOrderService.PostPurchaseOrderGrpc(request);

                _logger.LogInformation("[PurchaseOrderController][PostPurchaseOrder]: {message}", response.ToString());

                PurchaseOrderDTO purchaseOrderDTO = new PurchaseOrderDTO
                {
                    IdPurchaseOrder = response.IdPurchaseOrder,
                    Observations = response.Observations,
                    State = response.State,
                    RequestDate = TimestampGrpcConverter.ConvertToDateTime(response.RequestDate),
                };

                var responseDTO = new mainDTO
                {
                    Content = purchaseOrderDTO,
                };

                return Ok(responseDTO);
            }

            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[PurchaseOrderController][PostPurchaseOrder]: {error}", ex.Message);

                return GrpcExceptionHandler.HandleGrpcException(ex, responseDTO);
            }
        }

        [HttpGet("/GetAllPurchaseOrders")]
        public async Task<IActionResult> GetAllPurchaseOrders()
        {
            try
            {
                var response = await _purchaseOrderService.GetAllPurchaseOrdersGrpc();

                _logger.LogInformation("[PurchaseOrderController][GetAllPurchaseOrders]: {message}", response.ToString());

                var responseDTO = new mainDTO { Content = response };

                return Ok(responseDTO);
            }
            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[ProductController][GetAllProducts]: {error}", ex.Message);

                if (ex.StatusCode.Equals(Grpc.Core.StatusCode.Unavailable))
                {
                    return StatusCode(500, responseDTO);
                }

                return NotFound(responseDTO);
            }
        }

        [HttpGet("/GetAllPurchaseOrders/{state}")]
        public async Task<IActionResult> GetAllByStatePurchaseOrders(string state)
        {
            try
            {
                var response = await _purchaseOrderService.GetAllPurchaseByStateOrdersGrpc(state);

                _logger.LogInformation("[PurchaseOrderController][GetAllByStatePurchaseOrders]: {message}", response.ToString());

                var responseDTO = new mainDTO { Content = response };

                return Ok(responseDTO);
            }
            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[ProductController][GetAllProducts]: {error}", ex.Message);

                if (ex.StatusCode.Equals(Grpc.Core.StatusCode.Unavailable))
                {
                    return StatusCode(500, responseDTO);
                }

                return NotFound(responseDTO);
            }
        }
    }
}
