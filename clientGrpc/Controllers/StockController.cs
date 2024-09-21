using clientGrpc.Services;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace clientGrpc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetStockByCode(string code)
        {
            var stock = await _stockService.GetStockByCode(code);

            if(stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }
    }
}
