using productProto;

namespace clientGrpc.Services
{
    public interface IProductService
    {
        Task<ProductGrpc> GetProductGrpc(string code); 
    }
}
