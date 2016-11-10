namespace AutoStockBroker.Models
{
    public static class Calculator
    {
        public static StockPortfolio SetStockParameters(StockPortfolio stockPortfolio)
        {
            for (int i = 0; i < stockPortfolio.Stocks.Count; i++)
            {
                stockPortfolio.Stocks[i].Industry = stockPortfolio.Stocks[i].ValueDouble < 500 && stockPortfolio.Stocks[i].ValueDouble > 200 ? "Dagligvaror" : stockPortfolio.Stocks[i].Industry;
                stockPortfolio.Stocks[i].Industry = stockPortfolio.Stocks[i].ValueDouble < 700 && stockPortfolio.Stocks[i].ValueDouble > 300 ? "Hälsovård" : stockPortfolio.Stocks[i].Industry;
                stockPortfolio.Stocks[i].ValueOwned = stockPortfolio.Stocks[i].ValueDouble * stockPortfolio.Stocks[i].AmountOwned;
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