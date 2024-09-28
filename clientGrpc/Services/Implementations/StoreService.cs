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
            var response = await _client.getStoreGrpcAsync(request);
            return response;
        }

        public async Task<List<StoreGrpc>> GetStoresByState(bool active)
        {
            var request = new StoreStateRequest { Active = active };
            var response = await _client.GetStoresByStateAsync(request);

            return response.Stores.ToList();
        }

    }
}
