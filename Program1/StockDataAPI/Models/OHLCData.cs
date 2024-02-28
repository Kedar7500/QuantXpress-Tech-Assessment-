using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace StockDataAPI.Models
{
    public class OHLCData
    {
        [Name("symbol")]
        public string Symbol { get; set; }
        [Name("date")]
        public DateTime Date { get; set; }
        [Name("open")]
        public float Open { get; set; }
        [Name("high")]
        public float High { get; set; }
        [Name("low")]
        public float Low { get; set; }
        [Name("close")]
        public float Close { get; set; }
        [Name("volume")]
        public int Volume { get; set; }
        [Name("oi")]
        public int OI { get; set; }
    }
}
