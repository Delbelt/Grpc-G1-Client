
using dispatchOrderProto;

namespace clientGrpc.Services
{
    public interface IDispatchOrderService
    {
      Task<DispatchOrderGrpc> GetDispatchById(int dispatchOrder_id);
    }
}
