using productProto;
using stockProto;

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

        public async Task<StockList> GetAllStocks()
        {
            var request = new Empty(); // Crear un objeto Empty para la solicitud
            return await _client.getAllStocksAsync(request); // Llamar al RPC
        }

        public async Task<StockList> GetAvailableStocks()
        {
            var request = new Empty(); // Solicitud vacía
            return await _client.getAvailableStocksAsync(request);
        }

        public async Task<StockList> GetUnavailableStocks()
        {
            var request = new Empty(); // Solicitud vacía
            return await _client.getUnavailableStocksAsync(request);
        }
        public async Task<StockList> GetStockByProduct(string productCode)
        {
            var request = new GetStockByProductRequest { ProductCode = productCode };
            return await _client.getStockByProductAsync(request);
        }
        public async Task<StockList> GetStockByStore(string storeCode)
        {
            var request = new GetStockByStoreRequest { StoreCode = storeCode };
            return await _client.getStockByStoreAsync(request);
        }
        public async Task<CreateStockResponse> CreateStock(string storeCode, string productCode, int quantity)
        {
            var request = new CreateStockRequest
            {
                StoreCode = storeCode,
                ProductCode = productCode, // Solo usamos el código del producto
                Quantity = quantity
            };

            return await _client.createStockAsync(request);
        }

        public async Task<StockGrpc> AddStock(string stockCode, int quantity)
        {
            var request = new AddStockRequest
            {
                StockCode = stockCode,
                Quantity = quantity
            };

            return await _client.addStockAsync(request);
        }

        public async Task<StockGrpc> SubtractStock(string stockCode, int quantity)
        {
            var request = new SubtractStockRequest
            {
                StockCode = stockCode,
                Quantity = quantity
            };

            return await _client.subtractStockAsync(request);
        }



    }
}
