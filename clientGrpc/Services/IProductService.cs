using clientGrpc.DTOs;
using productProto;

namespace clientGrpc.Services
{
    public interface IProductService
    {
        Task<ProductGrpc> GetProductGrpc(string code);
        Task<ProductList> GetAllProductsGrpc();
        Task<string> CreateProductGrpc(ProductDTO productDTO);
    }
}
