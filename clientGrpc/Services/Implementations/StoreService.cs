using storeProto;

namespace clientGrpc.Services.Implementations
{
    public class StoreService : IStoreService
    {
        private readonly StoreGrpcService.StoreGrpcServiceClient _client;

        public StoreService(StoreGrpcService.StoreGrpcServiceClient client)
        {
            _client = client;
        }

        public async Task<StoreGrpc> GetStoreByCode(string code)
        {
            var request = new RequestCode { Code = code };
            return await _client.getStoreGrpcAsync(request);
        }


    }
}
