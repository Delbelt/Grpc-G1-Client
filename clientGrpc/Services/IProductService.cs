using clientGrpc.DTOs;
using productProto;
using System.Drawing;
using System.Xml.Linq;

namespace clientGrpc.Services
{
    public interface IProductService
    {
        Task<ProductGrpc> GetProductGrpc(string code);
        Task<ProductList> GetAllProductsGrpc();
        Task<ProductList> GetAllProductsActive(Boolean active);
        Task<string> CreateProductGrpc(ProductDTO productDTO);
        Task<ProductList> GetProductsByFilter(string code, string name, string size, string color);
        Task<string> DeleteProductGrpc(string code);
        Task<string> UpdateProductGrpc(ProductDTO productDTO);
        Task<string> ModifyProductActiveGrpc(string code);

    }
}
