using clientGrpc.DTOs;
using clientGrpc.Handlers;
using clientGrpc.Services;
using Grpc.Core;
using kafkaProto;
using Microsoft.AspNetCore.Mvc;

namespace clientGrpc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KafkaController : ControllerBase
    {
        private readonly IKafkaService _kafkaService;
        private readonly ILogger<IKafkaService> _logger;
        public KafkaController(IKafkaService kafkaService, ILogger<IKafkaService> logger)
        {
            _kafkaService = kafkaService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
        {
            try
            {
                var response = await _kafkaService.SendMessage(request.Topic, request.Message);

                _logger.LogInformation("[KafkaController][SendMessage]: {response}", response);

                KafkaDTO kafkaDTO = new KafkaDTO
                {
                    Message = response.Message,
                    Success = response.Success
                };

                var responseDTO = new mainDTO { Content = kafkaDTO };

                return Ok(responseDTO);
            }

            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[KafkaController][SendMessage]: {error}", ex.Message);

                return GrpcExceptionHandler.HandleGrpcException(ex, responseDTO);
            }
        }
    }
}
