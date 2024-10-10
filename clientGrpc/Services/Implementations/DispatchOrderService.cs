using dispatchOrderProto;

namespace clientGrpc.Services.Implementations
{
    public class DispatchOrderService : IDispatchOrderService
    {
        private readonly DispatchOrderGrpcService.DispatchOrderGrpcServiceClient _DispatchOrderGrpcService;

        public DispatchOrderService(DispatchOrderGrpcService.DispatchOrderGrpcServiceClient DispatchOrderGrpcService)
        {
           _DispatchOrderGrpcService = DispatchOrderGrpcService;
        }

        public async Task<DispatchOrderGrpc> GetDispatchById(int dispatchOrder_id)
        {
            var request = new GetByDispatchOrderRequest { DispatchOrder = dispatchOrder_id };

            var response = await _DispatchOrderGrpcService.GetDispatchOrderGrpcAsync(request);

            return response;
        }
    }
}
