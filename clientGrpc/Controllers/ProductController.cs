using clientGrpc.DTOs;
using clientGrpc.Services;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace clientGrpc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<IProductService> _logger;

        public ProductController(IProductService productService , ILogger<IProductService> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetProductById(string code)
        {
            try
            {
                var response = await _productService.GetProductGrpc(code);

                _logger.LogInformation("[ProductController][GetProductByCode]: {message}", response.ToString());

                var responseDTO = new mainDTO { Content = response };

                return Ok(responseDTO);
            }

            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[ProductController][GetProductByCode]: {error}", ex.Message);

                if (ex.StatusCode.Equals(Grpc.Core.StatusCode.Unavailable))
                {
                    return StatusCode(500, responseDTO);
                }

                return NotFound(responseDTO);
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var response = await _productService.GetAllProductsGrpc();

                _logger.LogInformation("[ProductController][GetAllProducts]: {message}", response.ToString());

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
