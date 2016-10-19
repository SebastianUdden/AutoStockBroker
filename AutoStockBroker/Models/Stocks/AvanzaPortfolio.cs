using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoStockBroker.Models.Stocks
{
    public class AvanzaPortfolio
    {
        public string StockCatalogueName { get; set; }
        public int TotalAmountOwned { get; set; }
        public double TotalValue { get; set; }
        //public Stock[] Stocks { get; set; }
        public List<Stock> LargeCapStocks { get; set; }
        public List<Stock> MidCapStocks { get; set; }
        public List<Stock> SmallCapStocks { get; set; }
        public List<Stock> LargeCapStocksOverview { get; internal set; }
    }
}