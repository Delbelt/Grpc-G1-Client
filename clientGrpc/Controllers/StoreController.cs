using clientGrpc.DTOs;
using clientGrpc.Handlers;
using clientGrpc.Services;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

using storeProto;


namespace clientGrpc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly ILogger<IUserService> _logger;

        public StoreController(IStoreService storeService, ILogger<IUserService> logger)
        {
            _storeService = storeService;
            _logger = logger;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetStoreByCode(string code)
        {
            try
            {
                var response = await _storeService.GetStoreByCode(code);
                _logger.LogInformation("[StoreController][GetStoreByCode]: {message}", response.ToString());

                var responseDTO = new mainDTO
                {
                    Content = response,
                };

                return Ok(responseDTO);
            }
            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[StoreController][GetStoreByCode]: {error}", ex.Message);

                return GrpcExceptionHandler.HandleGrpcException(ex, responseDTO);
            }
        }

        [HttpGet("state/{active}")]
        public async Task<IActionResult> GetStoresByState(bool active)
        {
            try
            {
                List<StoreGrpc> response = await _storeService.GetStoresByState(active);
                _logger.LogInformation("[StoreController][GetStoresByState]: {message}", response.ToString());

                var responseDTO = new mainDTO
                {
                    Content = response,
                };

                return Ok(responseDTO);
            }
            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[StoreController][GetStoresByState]: {error}", ex.Message);

                return GrpcExceptionHandler.HandleGrpcException(ex, responseDTO);
            }
        }
    }
}

