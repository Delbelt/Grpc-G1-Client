using OrderProcessingProto;

namespace clientGrpc.Services.Implementations
{
    public class OrderProcessingService : IOrderProcessingService
    {
        private readonly OrderProcessingGrpcService.OrderProcessingGrpcServiceClient _OrderProcessingGrpcService;

        public OrderProcessingService(OrderProcessingGrpcService.OrderProcessingGrpcServiceClient orderProcessingGrpcService)
        {
            _OrderProcessingGrpcService = orderProcessingGrpcService;
        }

        public async Task<ProcessingResponse> RunProcess()
        {
            var request = new EmptyProcessing();

            return await _OrderProcessingGrpcService.RunProcessAsync(request);
        }
    }
}
