using clientProto;

namespace clientGrpc.Services
{
    public interface IStockService
    {
        Task<StockGrpc> GetStockByCode(string code);
    }
}
