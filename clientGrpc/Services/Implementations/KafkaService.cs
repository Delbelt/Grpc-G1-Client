using authProto;
using clientGrpc.DTOs;
using kafkaProto;

namespace clientGrpc.Services.Implementations
{
    public class KafkaService : IKafkaService
    {
        private readonly KafkaGrpcService.KafkaGrpcServiceClient _KafkaGrpcService;

        public KafkaService(KafkaGrpcService.KafkaGrpcServiceClient kafkaGrpcService)
        {
            _KafkaGrpcService = kafkaGrpcService;
        }

        public async Task<KafkaDTO> SendMessage(string topic, string message)
        {
            var SendMessageRequest = new SendMessageRequest { Topic = topic, Message = message };

            var response = await _KafkaGrpcService.SendMessageAsync(SendMessageRequest);

            KafkaDTO kafkaDTO = new KafkaDTO
            {
                Success = response.Success,
                Message = response.Message,
            };

            return kafkaDTO;
        }
    }
}
