using clientGrpc.DTOs;

namespace clientGrpc.Services
{
    public interface IKafkaService
    {
        Task<KafkaDTO> SendMessage(string topic, string message);        
    }
}
