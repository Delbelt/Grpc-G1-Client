﻿using stockProto;

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

    }
}
