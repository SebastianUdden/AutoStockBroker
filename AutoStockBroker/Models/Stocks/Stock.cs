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
        Branch privateObject;
        public double DoubleValue { get; set; }
        public string StringValue { get; set; }
        public int AmountOwned { get; set; }
        public double ValueOwned { get; set; }
        public double PortfolioVolatility { get; set; }
        public double Weight { get; set; }
        public Stock()
        {
            Name = "";
        }

        public Branch EnumProperty
        {
            get
            {
                return privateObject;
            }
            set
            {
                privateObject = value;
            }
        }
    }
    public enum Branch
    {
        Bank = 1,
        Fastigheter = 3,
        Investmentbolag = 2,
        Produktion = 11,
        Byggprodukter = 12,
        Byggnation = 13,
        HandelOchDistribution = 14,
        TjänsterOchLeverans = 15,
        Bemanning = 16,
        BettingOchSpel = 23,
        Konsument = 24,
        KläderOchTextil = 25,
        HotelRestaurangOchNöje = 26,
        Media = 27,
        Återförsäljare = 28,
    }
}