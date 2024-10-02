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

        // Crear una nueva tienda
        [HttpPost]
        public async Task<IActionResult> CreateStore([FromBody] StoreGrpc store)
        {
            try
            {
                var response = await _storeService.CreateStore(store);
                _logger.LogInformation("[StoreController][CreateStore]: Store created with code {code}", store.Code);

                var responseDTO = new mainDTO
                {
                    Content = response,
                };

                return CreatedAtAction(nameof(GetStoreByCode), new { code = response.Code }, responseDTO);
            }
            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[StoreController][CreateStore]: {error}", ex.Message);

                return GrpcExceptionHandler.HandleGrpcException(ex, responseDTO);
            }
        }


        // Habilitar o deshabilitar una tienda
        [HttpPut("{code}/state")]
        public async Task<IActionResult> ChangeStoreState(string code, [FromBody] bool active)
        {
            try
            {
                var response = await _storeService.ChangeStoreState(code, active);
                _logger.LogInformation("[StoreController][ChangeStoreState]: Store state changed for code {code}", code);

                var responseDTO = new mainDTO
                {
                    Content = response,
                };

                return Ok(responseDTO);
            }
            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[StoreController][ChangeStoreState]: {error}", ex.Message);

                return GrpcExceptionHandler.HandleGrpcException(ex, responseDTO);
            }
        }
    }
}

