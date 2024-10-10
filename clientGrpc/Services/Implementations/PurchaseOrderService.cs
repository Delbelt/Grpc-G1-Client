
using clientGrpc.DTOs;
using clientGrpc.Handlers;
using orderItemProto;
using purchaseOrderProto;

namespace clientGrpc.Services.Implementations
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly PurchaseOrderGrpcService.PurchaseOrderGrpcServiceClient _PurchaseOrderGrpcService;

        public PurchaseOrderService(PurchaseOrderGrpcService.PurchaseOrderGrpcServiceClient purchaseOrderGrpcService)
        {
            _PurchaseOrderGrpcService = purchaseOrderGrpcService;
        }

        public async Task<PurchaseOrderDTO> GetPurchaseOrderById(int purchaseOrder_id)
        {
            var request = new GetByDispatchOrderRequest { IdPurchaseOrder = purchaseOrder_id };

            var response = await _PurchaseOrderGrpcService.GetPurchaseOrderGrpcAsync(request);

            var items = response.Items.Select(item => new OrderItemDTO
            {
                code = item.Code,
                color = item.Color,
                size = item.Size,
                quantity = item.Quantity
            }).ToList();

            PurchaseOrderDTO purchaseOrderDTO = new PurchaseOrderDTO
            {
                IdPurchaseOrder = purchaseOrder_id,
                Observations = response.Observations,
                RequestDate = TimestampGrpcConverter.ConvertToDateTime(response.RequestDate),
                State = response.State,
                OrderItems = items
            };

            return purchaseOrderDTO;
        }

        public async Task<PurchaseOrderGrpc> PostPurchaseOrderGrpc(PostPurchaseOrderRequestDto request)
        {
            var listaRange = new List<OrderItemGrpc>();
          
            listaRange.AddRange(request.Items);            

            var requestPost = new PostPurchaseOrderRequest
            {
                CodeStore = request.CodeStore,               
            };

            requestPost.Items.AddRange(listaRange);

            return await _PurchaseOrderGrpcService.PostPurchaseOrderGrpcAsync(requestPost);
        }

        public async Task<List<PurchaseOrderDTO>> GetAllPurchaseOrdersGrpc()
        {
            var emptyRequest = new EmptyAll();

            var response = await _PurchaseOrderGrpcService.GetAllPurchaseOrderGrpcAsync(emptyRequest);

            var purchaseOrderDTOs = response.Orders.Select(p => new PurchaseOrderDTO
            {
                IdPurchaseOrder = p.IdPurchaseOrder,
                State = p.State,
                Observations = p.Observations,
                RequestDate = TimestampGrpcConverter.ConvertToDateTime(p.RequestDate),
                OrderItems = p.Items.Select(item => new OrderItemDTO
                {
                    code = item.Code,
                    color = item.Color,
                    size = item.Size,
                    quantity = item.Quantity
                })
                .ToList(),
                
            })
             .ToList();

            return purchaseOrderDTOs;
        }

        public async Task<List<PurchaseOrderDTO>> GetAllPurchaseByStateOrdersGrpc(string state)
        {
            var stateRequest = new RequestAllByState{ State = state};

            var response = await _PurchaseOrderGrpcService.GetAllByStatePurchaseOrderGrpcAsync(stateRequest);

            var purchaseOrderDTOs = response.Orders.Select(p => new PurchaseOrderDTO
            {
                IdPurchaseOrder = p.IdPurchaseOrder,
                State = p.State,
                Observations = p.Observations,
                RequestDate = TimestampGrpcConverter.ConvertToDateTime(p.RequestDate),
                OrderItems = p.Items.Select(item => new OrderItemDTO
                {
                    code = item.Code,
                    color = item.Color,
                    size = item.Size,
                    quantity = item.Quantity
                })
                .ToList(),
            })
             .ToList();

            return purchaseOrderDTOs;
        }
    }
}
