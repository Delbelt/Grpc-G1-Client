using clientGrpc.DTOs;
using purchaseOrderProto;

namespace clientGrpc.Services
{
    public interface IPurchaseOrderService
    {
        Task<PurchaseOrderDTO> GetPurchaseOrderById(int purchaseOrder_id); 
        Task<PurchaseOrderGrpc> PostPurchaseOrderGrpc(PostPurchaseOrderRequestDto request);
        Task<List<PurchaseOrderDTO>> GetAllPurchaseOrdersGrpc();
        Task<List<PurchaseOrderDTO>> GetAllPurchaseByStateOrdersGrpc(string state);
    }
}
