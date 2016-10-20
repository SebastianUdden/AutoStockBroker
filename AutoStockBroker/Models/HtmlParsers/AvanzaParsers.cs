using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using AutoStockBroker.Models;

namespace AutoStockBroker.Models
{
    public static class AvanzaParsers
    {
        public static StockPortfolio ParseAvanzaOldListOverview(string website, StockPortfolio stockPortfolio)
        {
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(new WebClient().DownloadString(website));
            HtmlNode root = html.DocumentNode;
            var contentTable = root.Descendants().Where(n => n.GetAttributeValue("id", "").Equals("contentTable"));
            var contentTableData = contentTable.First();
            var tbody = contentTableData.Descendants("tbody").First();
            var trs = tbody.Descendants("tr");

            for (int i = 0; i < trs.Count(); i++)
            {
                string value = trs.ElementAt(i).Descendants("td").ElementAt(6).InnerText;
                //var tempValue = value.Replace(',', '.');
                //if (value.Length > 6)
                //{
                //    value = value.Remove(value.Length - 8, 2);
                //}

                //double doubleValue = 0;
                //bool doubleParseResult = double.TryParse(value, out doubleValue);
                string marketCap = CharHandling.RemoveSpecialCharacters(trs.ElementAt(i).Descendants("td").ElementAt(2).InnerText);
                stockPortfolio.Stocks[i].MarketCap = marketCap;
                stockPortfolio.Stocks[i].Dividend = trs.ElementAt(i).Descendants("td").ElementAt(3).InnerText;
                stockPortfolio.Stocks[i].Volatility = trs.ElementAt(i).Descendants("td").ElementAt(4).InnerText;
                stockPortfolio.Stocks[i].Beta = trs.ElementAt(i).Descendants("td").ElementAt(5).InnerText;
                stockPortfolio.Stocks[i].PriceEarnings = trs.ElementAt(i).Descendants("td").ElementAt(6).InnerText;
                stockPortfolio.Stocks[i].PriceSales = trs.ElementAt(i).Descendants("td").ElementAt(7).InnerText;
                stockPortfolio.Stocks[i].Consensus = trs.ElementAt(i).Descendants("td").ElementAt(8).InnerText;

                //string aTag = trs.ElementAt(i).OuterHtml.ToString();
                //int pFrom = aTag.IndexOf("href=\"/") + "href=\"/".Length;
                //int pTo = aTag.LastIndexOf("/nyckeltal");
                //string href = aTag.Substring(pFrom, pTo - pFrom);

                //stockPortfolio.Stocks.Add(new Stock
                //{
                //    Name = descendants.ElementAt(i).InnerText.Trim().Replace(".", ""),
                //    Href = href,
                //    AmountOwned = 10
                //});
            };

            return stockPortfolio;
        }

    }
}