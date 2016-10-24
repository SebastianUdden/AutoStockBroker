using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoStockBroker.Models
{
    public class Stock
    {
        public string Name { get; set; }
        public string Href { get; set; }
        public string Industry { get; set; }
        public string Currency { get; set; }
        public string MarketCap { get; set; }
        public string Dividend { get; set; }
        public string Volatility { get; set; }
        public string Beta { get; set; }
        public string PriceEarnings { get; set; }
        public string PriceSales { get; set; }
        public string Consensus { get; set; }
        public double DoubleValue { get; set; }
        public string StringValue { get; set; }
        public int AmountOwned { get; set; }
        public double ValueOwned { get; set; }
        public double Weight { get; set; }
        public Stock()
        {
            Name = "";
        }
    }
}