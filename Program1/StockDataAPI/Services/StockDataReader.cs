
using CsvHelper;
using StockDataAPI.Models;
using System.Linq;
using System.IO;

namespace StockDataAPI.Services
{
    public class StockDataReader : BackgroundService
    {

        private readonly ILogger<StockDataReader> _logger;
        private readonly Dictionary<string, (DateTime, float, float, float, float, int, int)> _stockData;

        public StockDataReader(ILogger<StockDataReader> logger)
        {
            _logger = logger;
            _stockData = new Dictionary<string, (DateTime, float, float, float, float, int, int)>();

            _logger.LogInformation("In constructor");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("StockDataReader background service is starting");

            var filePath = "Data/MDServerOHLCData_25072022_085347.csv";

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var reader = new StreamReader(filePath);

                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        var parts = line.Split(',');
                        var symbol = parts[0];
                        var date = DateTime.Parse(parts[1] + " " + parts[2]);
                        var open = float.Parse(parts[3]);
                        var high = float.Parse(parts[4]);
                        var low = float.Parse(parts[5]);
                        var close = float.Parse(parts[6]);
                        var volume = int.Parse(parts[7]);
                        var oi = int.Parse(parts[8]);

                        _stockData[symbol] = (date, open, high, low, close, volume, oi);


                        //_logger.LogInformation($"Read and stored symbol: {symbol}");

                        //_logger.LogInformation("Read data: symbol={symbol}, Date={Date}, Open={Open}, High={High}, Low={Low}, Close={Close}, Volume={Volume}, OI={OI}",
                        //symbol, date, open, high, low, close, volume, oi);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error reading from CSV file");
                }

                await Task.Delay(10, stoppingToken);
            }
        }

        public (DateTime, float, float, float, float, int, int)? GetLatestStockData(string symbol)
        {
            _logger.LogInformation($"Retrieving data for symbol: {symbol}, Type: {symbol.GetType()}");

            if (_stockData.TryGetValue(symbol, out var data))
            {
                _logger.LogInformation($"Stock data: {data}");
                return data;
            }
            else
            {
                _logger.LogInformation("Symbol not found, return null");
                return null; // Symbol not found, return null
            }
        }
    }
}

