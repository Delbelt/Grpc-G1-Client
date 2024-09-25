using storeProto;

namespace clientGrpc.Services
{
    public interface IStoreService
    {
        Task<StoreGrpc> GetStoreByCode(string code);
    }
}
