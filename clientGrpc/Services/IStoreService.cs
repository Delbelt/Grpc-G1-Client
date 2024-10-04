using storeProto;

namespace clientGrpc.Services
{
    public interface IStoreService
    {
        Task<StoreGrpc> GetStoreByCode(string code);

        Task<List<StoreGrpc>> GetStoresByState(bool active);

        Task<StoreGrpc> CreateStore(StoreGrpc store);

        Task<StoreGrpc> ChangeStoreState(string code, bool active);

        Task<StockGrpc> AssignProductToStore(string storeCode, string productCode);

        Task<StoreGrpc> AssignUserToStore(string storeCode, int userId);

        Task<RemoveProductResponse> RemoveProductFromStore(string storeCode, string productCode);

        Task<RemoveUserResponse> RemoveUserFromStore(string storeCode, int userId);


    }
}
