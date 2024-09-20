using clientProto;

namespace clientGrpc.Services
{
    public interface IStoreService
    {
        Task<StoreGrpc> GetStoreByCode(string code);
    }
}
