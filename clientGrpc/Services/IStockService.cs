using stockProto;

namespace clientGrpc.Services
{
    public interface IStockService
    {
        Task<StockGrpc> GetStockByCode(string code);
        Task<StockList> GetAllStocks();
    }
}
