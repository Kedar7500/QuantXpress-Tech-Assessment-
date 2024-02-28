using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockDataAPI.Services;

namespace StockDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockDataController : ControllerBase
    {
        private readonly StockDataReader _stockDataReader;
        private readonly ILogger<StockDataController> _logger;

        public StockDataController(StockDataReader stockDataReader, ILogger<StockDataController> logger)
        {
            _stockDataReader = stockDataReader;
            _logger = logger;
        }

        [HttpGet("{symbol}")]
        public IActionResult GetLatestPrice([FromRoute] string symbol)
        {
             symbol = symbol.Trim();
            _logger.LogInformation("Received symbol: {symbol}", symbol);

            //symbol = "BANKNIFTY28JUL2236800CE";

            var data = _stockDataReader.GetLatestStockData(symbol);
            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                _logger.LogWarning("Symbol not found: {Symbol}", symbol);
                return NotFound("Symbol not found");
            }
        }
    }
}

