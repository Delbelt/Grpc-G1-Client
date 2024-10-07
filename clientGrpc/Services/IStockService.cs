using productProto;
using stockProto;

namespace clientGrpc.Services
{
    public interface IStockService
    {
        Task<StockGrpc> GetStockByCode(string code);
        Task<StockList> GetAllStocks();
        Task<StockList> GetAvailableStocks();
        Task<StockList> GetUnavailableStocks();
        Task<StockList> GetStockByProduct(string productCode);
        Task<StockList> GetStockByStore(string storeCode);
        Task<CreateStockResponse> CreateStock(string storeCode, string productCode, int quantity);
        Task<StockGrpc> AddStock(string stockCode, int quantity);
        Task<StockGrpc> SubtractStock(string stockCode, int quantity);
    }
}
