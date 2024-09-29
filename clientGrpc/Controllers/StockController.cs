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

    }
}
