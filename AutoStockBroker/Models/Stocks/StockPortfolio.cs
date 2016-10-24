using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoStockBroker.Models
{
    public class StockPortfolio
    {
        public string StockCatalogueName { get; set; }
        public int TotalAmountOwned { get; set; }
        public double TotalValue { get; set; }
        public double PortfolioVolatility { get; set; }
        //public Stock[] Stocks { get; set; }
        public List<Stock> Stocks { get; set; }
        public double VolatilityPortfolioContribution { get; internal set; }

        public StockPortfolio(string stockCatalogueName)
        {
            StockCatalogueName = stockCatalogueName;
            TotalValue = 0;
        }
    }
}