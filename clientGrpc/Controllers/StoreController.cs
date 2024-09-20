using clientGrpc.Services;
using Microsoft.AspNetCore.Mvc;


using System.Threading.Tasks;

namespace clientGrpc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetStoreByCode(string code)
        {
            var store = await _storeService.GetStoreByCode(code);
            if (store == null)
            {
                return NotFound();
            }
            return Ok(store);
        }
    }
}

