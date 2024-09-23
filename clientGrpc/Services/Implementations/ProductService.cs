using productProto;

namespace clientGrpc.Services.Implementations
{
    public class ProductService : IProductService
    {

        private readonly ProductGrpcService.ProductGrpcServiceClient _ProductGrpcService;
        public ProductService(ProductGrpcService.ProductGrpcServiceClient ProductGrpcService)
        {
            _ProductGrpcService = ProductGrpcService;
        }
        public async Task <ProductGrpc> GetProductGrpc(string code) 
        {
            var fetchByCode = new RequestId { Code = code};

            var response = await _ProductGrpcService.GetProductByCodeAsync(fetchByCode);

            return response;
        }

    }
}
