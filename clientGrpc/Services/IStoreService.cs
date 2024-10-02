using storeProto;

namespace clientGrpc.Services
{
    public interface IStoreService
    {
        Task<StoreGrpc> GetStoreByCode(string code);

        Task<List<StoreGrpc>> GetStoresByState(bool active);

        Task<StoreGrpc> CreateStore(StoreGrpc store);

        Task<StoreGrpc> ChangeStoreState(string code, bool active);

    }
}
