using clientGrpc.DTOs;
using clientGrpc.Handlers;
using clientGrpc.Services;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using stockProto;

namespace clientGrpc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly ILogger<IStockService> _logger;

        public StockController(IStockService stockService, ILogger<IStockService> logger)
        {
            _stockService = stockService;
            _logger = logger;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetStockByCode(string code)
        {
            try
            {
                var stock = await _stockService.GetStockByCode(code);

                if (stock == null)
                {
                    return NotFound();
                }

                _logger.LogInformation("[StockController][GetStockByCode]: {message}", stock.ToString());

                var responseDTO = new mainDTO
                {
                    Content = stock,
                };

                return Ok(responseDTO);
            }
            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[StockController][GetStockByCode]: {error}", ex.Message);

                return GrpcExceptionHandler.HandleGrpcException(ex, responseDTO);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllStocks()
        {
            try
            {
                var stockList = await _stockService.GetAllStocks();

                if (stockList.Stocks.Count == 0)
                {
                    return NotFound("No stocks available.");
                }

                _logger.LogInformation("[StockController][GetAllStocks]: {count} stocks found", stockList.Stocks.Count);

                var responseDTO = new mainDTO
                {
                    Content = stockList,
                };

                return Ok(responseDTO);
            }
            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[StockController][GetAllStocks]: {error}", ex.Message);

                return GrpcExceptionHandler.HandleGrpcException(ex, responseDTO);
            }
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableStocks()
        {
            try
            {
                var availableStockList = await _stockService.GetAvailableStocks();

                if (availableStockList.Stocks.Count == 0)
                {
                    return NotFound("No available stocks.");
                }

                _logger.LogInformation("[StockController][GetAvailableStocks]: {count} available stocks found", availableStockList.Stocks.Count);

                var responseDTO = new mainDTO
                {
                    Content = availableStockList,
                };

                return Ok(responseDTO);
            }
            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[StockController][GetAvailableStocks]: {error}", ex.Message);

                return GrpcExceptionHandler.HandleGrpcException(ex, responseDTO);
            }
        }

        [HttpGet("unavailable")] // Nuevo endpoint para obtener stocks no disponibles
        public async Task<IActionResult> GetUnavailableStocks()
        {
            try
            {
                var unavailableStockList = await _stockService.GetUnavailableStocks();

                if (unavailableStockList.Stocks.Count == 0)
                {
                    return NotFound("No unavailable stocks.");
                }

                _logger.LogInformation("[StockController][GetUnavailableStocks]: {count} unavailable stocks found", unavailableStockList.Stocks.Count);

                var responseDTO = new mainDTO
                {
                    Content = unavailableStockList,
                };

                return Ok(responseDTO);
            }
            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[StockController][GetUnavailableStocks]: {error}", ex.Message);

                return GrpcExceptionHandler.HandleGrpcException(ex, responseDTO);
            }
        }
        [HttpGet("byproduct/{productCode}")]
        public async Task<IActionResult> GetStockByProduct(string productCode)
        {
            try
            {
                var stockList = await _stockService.GetStockByProduct(productCode);

                if (stockList.Stocks.Count == 0)
                {
                    return NotFound("No stocks found for this product.");
                }

                _logger.LogInformation("[StockController][GetStockByProduct]: {count} stocks found", stockList.Stocks.Count);

                var responseDTO = new mainDTO
                {
                    Content = stockList,
                };

                return Ok(responseDTO);
            }
            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[StockController][GetStockByProduct]: {error}", ex.Message);

                return GrpcExceptionHandler.HandleGrpcException(ex, responseDTO);
            }
        }
        [HttpGet("store/{storeCode}")] // Endpoint para obtener stock por tienda
        public async Task<IActionResult> GetStockByStore(string storeCode)
        {
            try
            {
                var stockList = await _stockService.GetStockByStore(storeCode);

                if (stockList.Stocks.Count == 0)
                {
                    return NotFound("No stocks available for this store.");
                }

                _logger.LogInformation("[StockController][GetStockByStore]: {count} stocks found", stockList.Stocks.Count);

                var responseDTO = new mainDTO
                {
                    Content = stockList,
                };

                return Ok(responseDTO);
            }
            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[StockController][GetStockByStore]: {error}", ex.Message);

                return GrpcExceptionHandler.HandleGrpcException(ex, responseDTO);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequest createStockRequest)
        {
            try
            {
                // Asegúrate de que el request no sea nulo
                if (createStockRequest == null)
                {
                    return BadRequest("Invalid stock creation request.");
                }

                // Llamar al servicio para crear el stock
                var response = await _stockService.CreateStock(createStockRequest.StoreCode, createStockRequest.ProductCode, createStockRequest.Quantity);

                // Regresar una respuesta 201 Created con el nuevo código de stock
                return CreatedAtAction(nameof(GetStockByCode), new { code = response.Code }, response);
            }
            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[StockController][CreateStock]: {error}", ex.Message);

                return GrpcExceptionHandler.HandleGrpcException(ex, responseDTO);
            }
            catch (Exception ex) // Manejo adicional de excepciones generales si es necesario
            {
                var responseDTO = new mainDTO { Content = "An unexpected error occurred." };

                _logger.LogError("[StockController][CreateStock]: {error}", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, responseDTO);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddStock([FromBody] AddStockRequest addStockRequest)
        {
            try
            {
                if (addStockRequest == null)
                {
                    return BadRequest("Invalid stock addition request.");
                }

                var stock = await _stockService.AddStock(addStockRequest.StockCode, addStockRequest.Quantity);

                return Ok(stock);
            }
            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };
                _logger.LogError("[StockController][AddStock]: {error}", ex.Message);
                return GrpcExceptionHandler.HandleGrpcException(ex, responseDTO);
            }
        }

        [HttpPost("subtract")]
        public async Task<IActionResult> SubtractStock([FromBody] SubtractStockRequest subtractStockRequest)
        {
            try
            {
                if (subtractStockRequest == null)
                {
                    return BadRequest("Invalid stock subtraction request.");
                }

                var stock = await _stockService.SubtractStock(subtractStockRequest.StockCode, subtractStockRequest.Quantity);

                return Ok(stock);
            }
            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };
                _logger.LogError("[StockController][SubtractStock]: {error}", ex.Message);
                return GrpcExceptionHandler.HandleGrpcException(ex, responseDTO);
            }
        }



    }
}
