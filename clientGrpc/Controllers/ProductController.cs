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

        public ProductController(IProductService productService, ILogger<IProductService> logger)
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

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO productDTO)
        {
            try
            {
                var response = await _productService.CreateProductGrpc(productDTO);

                _logger.LogInformation("[ProductController][CreateProduct]: {message}", response.ToString());

                var responseDTO = new mainDTO { Content = response };

                return Ok(responseDTO);
            }
            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[ProductController][CreateProduct]: {error}", ex.Message);

                if (ex.StatusCode.Equals(Grpc.Core.StatusCode.Unavailable))
                {
                    return StatusCode(500, responseDTO);
                }

                return NotFound(responseDTO);
            }

        }

        [HttpGet("Filter")]
        public async Task<IActionResult> GetProductsByFilter(
            [FromQuery] string code = "",
            [FromQuery] string name = "",
            [FromQuery] string size = "",
            [FromQuery] string color = "")
        {
            try
            {
                var response = await _productService.GetProductsByFilter(code, name, size, color);

                _logger.LogInformation("[ProductController][GetProductsByFilter]: {message}", response.ToString());

                var responseDTO = new mainDTO { Content = response };

                return Ok(responseDTO);
            }
            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[ProductController][GetProductsByFilter]: {error}", ex.Message);

                if (ex.StatusCode.Equals(Grpc.Core.StatusCode.Unavailable))
                {
                    return StatusCode(500, responseDTO);
                }

                return NotFound(responseDTO);
            }
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteProduct(string code)
        {
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    _logger.LogWarning("[ProductController][DeleteProduct]: Product code is required.");
                    return BadRequest(new mainDTO { Content = "Product code is required." });
                }

                var result = await _productService.DeleteProductGrpc(code);

                _logger.LogInformation("[ProductController][DeleteProduct]: Successfully deleted product with code {code}.", code);

                var responseDTO = new mainDTO { Content = result };
                return Ok(responseDTO);
            }
            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[ProductController][DeleteProduct]: Error occurred while deleting product with code {code}. Error: {error}", code, ex.Message);

                if (ex.StatusCode.Equals(Grpc.Core.StatusCode.Unavailable))
                {
                    return StatusCode(500, responseDTO);
                }

                return NotFound(responseDTO);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO productDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(productDTO.Code))
                {
                    _logger.LogWarning("[ProductController][UpdateProduct]: Product code is required.");
                    return BadRequest(new mainDTO { Content = "Product code is required." });
                }

                // Llamar al servicio para actualizar el producto
                var response = await _productService.UpdateProductGrpc(productDTO);

                _logger.LogInformation("[ProductController][UpdateProduct]: {message}", response.ToString());

                var responseDTO = new mainDTO { Content = response };

                return Ok(responseDTO);
            }
            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[ProductController][UpdateProduct]: {error}", ex.Message);

                if (ex.StatusCode.Equals(Grpc.Core.StatusCode.Unavailable))
                {
                    return StatusCode(500, responseDTO);
                }

                return NotFound(responseDTO);
            }


        }
    }
}
