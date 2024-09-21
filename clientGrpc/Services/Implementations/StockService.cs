using clientProto;

namespace clientGrpc.Services.Implementations
{
    public class StockService : IStockService
    {
        private readonly StockGrpcService.StockGrpcServiceClient _client;

        public StockService(StockGrpcService.StockGrpcServiceClient client)
        {
            _client = client;
        }

        public async Task<StockGrpc> GetStockByCode(string code)
        {
            var request = new GetStockByIdRequest { Code = code };
            return await _client.getStockByIdAsync(request);
        }
    }
}
