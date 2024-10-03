using clientGrpc.DTOs;
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
        public async Task<ProductGrpc> GetProductGrpc(string code)
        {
            var fetchByCode = new RequestId { Code = code };

            var response = await _ProductGrpcService.GetProductByCodeAsync(fetchByCode);

            return response;
        }
        public async Task<ProductList> GetAllProductsGrpc()
        {
            var emptyRequest = new Empty(); // Solicitud vacía para obtener todos los productos
            var response = await _ProductGrpcService.GetAllProductsAsync(emptyRequest);
            return response;
        }

        public async Task<string> CreateProductGrpc(ProductDTO productDTO)
        {
            var newProduct = new ProductGrpc
            {
                Code = productDTO.Code,
                Name = productDTO.Name,
                Size = productDTO.Size,
                Photo = Google.Protobuf.ByteString.CopyFrom(productDTO.Photo),
                Color = productDTO.Color,
                Active = productDTO.Active,
                
            };

            var response = await _ProductGrpcService.CreateProductAsync(newProduct);
            return response.Message;
        }

        public async Task<ProductList> GetProductsByFilter(string code, string name, string size, string color)
        {
            var filterRequest = new ProductFilterRequest
            {
                Code = code ?? "",  // Si alguno de los parámetros es nulo, envía una cadena vacía
                Name = name ?? "",
                Size = size ?? "",
                Color = color ?? ""
            };

            var response = await _ProductGrpcService.GetProductsByFilterAsync(filterRequest);

            return response;
        }

    }
}
