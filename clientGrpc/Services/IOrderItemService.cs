using orderItemProto;

namespace clientGrpc.Services
{
    public interface IOrderItemService
    {
        Task<OrderItemGrpc> GetOrderItemByCode(int id);
    }
}
