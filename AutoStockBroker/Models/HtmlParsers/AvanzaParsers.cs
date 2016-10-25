using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using AutoStockBroker.Models;
using System.Globalization;

namespace AutoStockBroker.Models
{
    public static class AvanzaParsers
    {
        public static StockPortfolio ParseAvanzaOldList(string website, StockPortfolio stockPortfolio)
        {
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(new WebClient().DownloadString(website));
            HtmlNode root = html.DocumentNode;
            var contentTable = root.Descendants().Where(n => n.GetAttributeValue("id", "").Equals("contentTable"));
            var contentTableData = contentTable.First();
            var tbody = contentTableData.Descendants("tbody").First();
            var trs = tbody.Descendants("tr");
            //var firstTr = trs.First();
            //var tds = firstTr.Descendants("td");
            //var firstTd = tds.ElementAt(1);
            ////var spans = firstTd.Descendants("span");
            ////var firstSpan = spans.First();
            //var aHref = firstTd.Descendants("a").First();
            //var name = aHref.InnerText.Trim();
            //var descendants = trs;

            //string tobesearched = "href=\\";
            //string code = myString.Substring(myString.IndexOf(tobesearched) + tobesearched.Length);
            //var href = aTag;
            stockPortfolio.Stocks = new List<Stock>();

            for (int i = 0; i < trs.Count(); i++)
            {
                string value = trs.ElementAt(i).Descendants("td").ElementAt(6).InnerText;
                //var tempValue = value.Replace(',', '.');
                if (value.Length > 6)
                {
                    value = value.Remove(value.Length - 8, 2);
                }

                value = value.Replace(",", ".");
                string stringValue = string.Format("{0:0,##}", value);
                double doubleValue = 0;
                bool doubleParseResult = double.TryParse(stringValue, NumberStyles.Number, CultureInfo.InvariantCulture, out doubleValue);

                stockPortfolio.Stocks.Add(new Stock
                {
                    Name = trs.ElementAt(i).Descendants("td").ElementAt(1).Descendants("a").First().InnerText.Trim(),
                    Currency = "SEK",
                    ValueDouble = doubleValue,
                    ValueString = stringValue
                    //StringValue = Regex.Match(tempValue, @"\d+(\,\d{1,2})?").Value
                });
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
                string marketCap = CharHandling.RemoveSpecialCharacters(trs.ElementAt(i).Descendants("td").ElementAt(2).InnerText);
                string marketCapCleansed = "N/A";
                if (marketCap.Length - 4 > 3)
                {
                    marketCapCleansed = marketCap.Remove(marketCap.Length - 4, 4);
                }
                int marketCapInt = 0;
                if (int.TryParse(marketCapCleansed, out marketCapInt))
                {
                    marketCapCleansed = marketCapInt.ToString("# ##0");
                }
                stockPortfolio.Stocks[i].MarketCap = marketCapCleansed;
                stockPortfolio.Stocks[i].MarketCapInt = marketCapInt;

                string dividend = trs.ElementAt(i).Descendants("td").ElementAt(3).InnerText;
                stockPortfolio.Stocks[i].Dividend = dividend.Replace(",", ".");
                double dividendDouble = 0;
                bool doubleParseDividendResult = double.TryParse(dividend, out dividendDouble);
                stockPortfolio.Stocks[i].DividendDouble = dividendDouble;

                string volatility = trs.ElementAt(i).Descendants("td").ElementAt(4).InnerText;
                volatility = volatility.Replace(",", ".");
                double volatilityDouble = 0;
                bool doubleParseVolatilityResult = double.TryParse(volatility, NumberStyles.Number, CultureInfo.InvariantCulture, out volatilityDouble);
                stockPortfolio.Stocks[i].Volatility = volatilityDouble;
                stockPortfolio.Stocks[i].PortfolioVolatility = volatilityDouble * stockPortfolio.Stocks[i].Weight / 100;

                string beta = trs.ElementAt(i).Descendants("td").ElementAt(5).InnerText;
                stockPortfolio.Stocks[i].Beta = beta.Replace(",", ".");
                double betaDouble = 0;
                bool doubleParseBetaResult = double.TryParse(beta, out betaDouble);
                stockPortfolio.Stocks[i].BetaDouble = betaDouble;

                string priceEarnings = trs.ElementAt(i).Descendants("td").ElementAt(6).InnerText;
                stockPortfolio.Stocks[i].PriceEarnings = priceEarnings.Replace(",", ".");
                double priceEarningsDouble = 0;
                bool doubleParsePriceEarningsResult = double.TryParse(priceEarnings, out priceEarningsDouble);
                stockPortfolio.Stocks[i].PriceEarningsDouble = priceEarningsDouble;

                string priceSales = trs.ElementAt(i).Descendants("td").ElementAt(7).InnerText;
                stockPortfolio.Stocks[i].PriceSales = priceSales.Replace(",", ".");
                double priceSalesDouble = 0;
                bool doubleParsePriceSalesResult = double.TryParse(priceSales, out priceSalesDouble);
                stockPortfolio.Stocks[i].PriceSalesDouble = priceSalesDouble;

                string consensus = trs.ElementAt(i).Descendants("td").ElementAt(8).InnerText;
                stockPortfolio.Stocks[i].Consensus = consensus;
                double consensusDouble = 0;
                bool doubleParseConsensusResult = double.TryParse(consensus.Substring(0, consensus.Length - 1), out consensusDouble);
                stockPortfolio.Stocks[i].ConsensusDouble = consensusDouble;

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
        public static StockPortfolio ParseStockList(string website, StockPortfolio stockPortfolio)
        {
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(new WebClient().DownloadString(website));
            HtmlNode root = html.DocumentNode;
            var compListDataEnumerable = root.Descendants().Where(n => n.GetAttributeValue("id", "").Equals("CompListData"));
            var compListData = compListDataEnumerable.First();
            var childNodes = compListData.ChildNodes;
            var descendants = childNodes.Descendants("a");

            //string tobesearched = "href=\\";
            //string code = myString.Substring(myString.IndexOf(tobesearched) + tobesearched.Length);
            //var href = aTag;
            stockPortfolio.Stocks = new List<Stock>();

            for (int i = 0; i < 10/*descendants.Count()*/; i++)
            {
                string aTag = descendants.ElementAt(i).OuterHtml.ToString();
                int pFrom = aTag.IndexOf("href=\"/") + "href=\"/".Length;
                int pTo = aTag.LastIndexOf("/nyckeltal");
                string href = aTag.Substring(pFrom, pTo - pFrom);

                stockPortfolio.Stocks.Add(new Stock
                {
                    Name = descendants.ElementAt(i).InnerText.Trim().Replace(".", ""),
                    Href = href,
                    AmountOwned = 10
                });
            }
            #region NotUsed
            //    if (descendants.ElementAt(i).InnerText.Trim().ToString().Length > 3)
            //    {
            //        if (descendants.ElementAt(i)
            //                .InnerText.Trim()
            //                .ToString()
            //                .Substring(descendants.ElementAt(i)
            //                    .InnerText.Trim()
            //                    .ToString()
            //                    .Length - 4, 4) == "Pref" || descendants.ElementAt(i)
            //                .InnerText.Trim()
            //                .ToString()
            //                .Substring(descendants.ElementAt(i)
            //                    .InnerText.Trim()
            //                    .ToString()
            //                    .Length - 1, 1) == "B")
            //        {
            //            continue;
            //        }
            //    }

            //var mCSB_1 = root.Descendants().Where(n => n.GetAttributeValue("id", "").Equals("mCSB_1"));

            //string stockTitle = mCSB_1.ElementAt(0).Descendants("a").Where(s => s.GetAttributeValue("title", "").Equals("2E Group")).ToString();

            //HtmlNode pTag = mCSB_container.Descendants("p").First();
            //string aTagContent = pTag.Descendants("a").First().InnerHtml;
            #endregion

            return stockPortfolio;
        }
    }
}