using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoStockBroker.Models.Stocks
{
    public class Stock
    {
        public string Name { get; set; }
        public string Href { get; set; }
        public string Industry { get; set; }
        Branch privateObject;
        public double Value { get; set; }
        public int AmountOwned { get; set; }
        public double ValueOwned { get; set; }
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