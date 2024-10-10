using orderItemProto;

namespace clientGrpc.Services.Implementations
{
    public class OrderItemService : IOrderItemService
    {
        private readonly OrderItemGrpcService.OrderItemGrpcServiceClient _OrderItemGrpcService;

        public OrderItemService(OrderItemGrpcService.OrderItemGrpcServiceClient orderItemGrpcService)
        {
            _OrderItemGrpcService = orderItemGrpcService;
        }
        public async Task<OrderItemGrpc> GetOrderItemByCode(int id)
        {
            var request = new GetByIdRequest { Id = id };

            var response = await _OrderItemGrpcService.GetPurcharseOrderGrpcAsync(request);

            return response;
        }
    }
}
