using OrderProcessingProto;

namespace clientGrpc.Services
{
    public interface IOrderProcessingService
    {
        Task<ProcessingResponse> RunProcess();
    }
}
