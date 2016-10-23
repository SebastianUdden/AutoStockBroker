using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoStockBroker.Models
{
    public static class Calculator
    {
        public static StockPortfolio SetStockParameters(StockPortfolio stockPortfolio)
        {
            for (int i = 0; i < stockPortfolio.Stocks.Count; i++)
            {
                stockPortfolio.Stocks[i].ValueOwned = stockPortfolio.Stocks[i].DoubleValue * stockPortfolio.Stocks[i].AmountOwned;
                stockPortfolio.TotalValue += stockPortfolio.Stocks[i].ValueOwned;
                stockPortfolio.TotalAmountOwned += stockPortfolio.Stocks[i].AmountOwned;
                stockPortfolio.PortfolioVolatility += stockPortfolio.Stocks[i].PortfolioVolatility;
            }

            for (int i = 0; i < stockPortfolio.Stocks.Count; i++)
            {
                stockPortfolio.Stocks[i].Weight = 100 * (stockPortfolio.Stocks[i].ValueOwned / stockPortfolio.TotalValue);
            }

            return stockPortfolio;
        }

    }
}