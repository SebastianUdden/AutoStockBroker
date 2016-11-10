namespace AutoStockBroker.Models
{
    public class Stock
    {
        public string Name { get; set; }
        public string StockCatalogueName { get; set; }
        public string Href { get; set; }
        public string Industry { get; set; }
        public string Currency { get; set; }
        public string ValueString { get; set; }
        public double ValueDouble { get; set; }
        public string MarketCap { get; set; }
        public int MarketCapInt { get; set; }
        public string Dividend { get; set; }
        public double DividendDouble { get; set; }
        public double Volatility { get; set; }
        public string Beta { get; set; }
        public double BetaDouble { get; set; }
        public string PriceEarnings { get; set; }
        public double PriceEarningsDouble { get; set; }
        public string PriceSales { get; set; }
        public double PriceSalesDouble { get; set; }
        public string Consensus { get; set; }
        public double ConsensusDouble { get; set; }
        public int AmountOwned { get; set; }
        public double ValueOwned { get; set; }
        public double PortfolioVolatility { get; set; }
        public double Weight { get; set; }

        public double Today { get; set; }
        public double OneMonth { get; set; }
        public double ThreeMonths { get; set; }
        public double OneYear { get; set; }
        public double ThreeYears { get; set; }
        public Stock()
        {
            Name = "";
        }
    }
}