using AutoStockBroker.Models.Stocks;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AutoStockBroker.Controllers
{
    public class PrototypeController : Controller
    {
        public ActionResult StockCatalogues()
        {
            //StockPortfolio stockPortfolio = CreateListOfStocks("Börsdata");
            StockPortfolio stockPortfolio = CreateListOfStocks("Avanza Old List");

            StockPortfolio newStockPortfolio = ParseStockList("https://borsdata.se", stockPortfolio);

            StockPortfolio avanzaStockPortfolio = ParseAvanzaOldList("https://www.avanza.se/aktier/gamla-aktielistan.html", stockPortfolio);

            #region NotUsed
            //stockPortfolio.Stocks = new []{
            //    new Stock
            //    {
            //        Name = "2E Group",
            //        AmountOwned = 5
            //    },
            //    new Stock
            //    {
            //        Name = "A City Media",
            //        AmountOwned = 1
            //    },
            //    new Stock
            //    {
            //        Name = "Ap Moller Maersk",
            //        AmountOwned = 3
            //    },
            //    new Stock
            //    {
            //        Name = "A1m Pharma",
            //        AmountOwned = 5
            //    },
            //    new Stock
            //    {
            //        Name = "Aalborg Bolspilklub",
            //        AmountOwned = 10
            //    },
            //    new Stock
            //    {
            //        Name = "Aarhus Elite",
            //        AmountOwned = 15
            //    },
            //    new Stock
            //    {
            //        Name = "Aarhuskarlshamn",
            //        AmountOwned = 7
            //    },
            //    new Stock
            //    {
            //        Name = "Aasen Sparebank",
            //        AmountOwned = 30
            //    },
            //    new Stock
            //    {
            //        Name = "ABB",
            //        AmountOwned = 36
            //    }
            //};
            #endregion

            //StockPortfolio viewStockPortfolio = SetStockParameters("https://borsdata.se", newStockPortfolio);
            ViewBag.Message = "These are the stocks currently added.";
            return View(avanzaStockPortfolio);
        }

        private StockPortfolio ParseAvanzaOldList(string website, StockPortfolio stockPortfolio)
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
                stockPortfolio.Stocks.Add(new Stock {
                    Name = trs.ElementAt(i).Descendants("td").ElementAt(1).Descendants("a").First().InnerText.Trim(),
                    Value = Convert.ToDouble(trs.ElementAt(i).Descendants("td").ElementAt(6).InnerText)
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

        private StockPortfolio SetStockParameters(string website, StockPortfolio stockPortfolio)
        {
            for (int i = 0; i < stockPortfolio.Stocks.Count; i++)
            {
                stockPortfolio.Stocks[i].Value = Convert.ToDouble(ParseIndividualHTML(website + "/" + stockPortfolio.Stocks[i].Href + "/nyckeltal"));
                stockPortfolio.Stocks[i].ValueOwned = stockPortfolio.Stocks[i].Value * stockPortfolio.Stocks[i].AmountOwned;
                stockPortfolio.TotalValue += stockPortfolio.Stocks[i].ValueOwned;
                stockPortfolio.TotalAmountOwned += stockPortfolio.Stocks[i].AmountOwned;
            }

            for (int i = 0; i < stockPortfolio.Stocks.Count; i++)
            {
                stockPortfolio.Stocks[i].Weight = 100 * (stockPortfolio.Stocks[i].ValueOwned / stockPortfolio.TotalValue);
            }

            return stockPortfolio;
        }

        private StockPortfolio CreateListOfStocks(string stockCatalogueName)
        {
            StockPortfolio listOfStocks = new StockPortfolio();
            listOfStocks.StockCatalogueName = stockCatalogueName;
            listOfStocks.TotalValue = 0;
            return listOfStocks;
        }

        private StockPortfolio ParseStockList(string website, StockPortfolio stockPortfolio)
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

        private string ParseIndividualHTML(string website)
        {
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(new WebClient().DownloadString(website));
            HtmlNode root = html.DocumentNode;
            HtmlNode topbox = root.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("RightTopBoxBig")).Single();
            HtmlNode topBoxtable = topbox.Descendants("table").Single();
            string content = topBoxtable.Descendants("tr").ElementAt(1).Descendants("td").Single().InnerHtml;
            string stockValue = Regex.Match(content, @"\d+(\,\d{1,2})?").Value;

            return stockValue;

            #region Not used
            //var html = new HtmlDocument();
            //html.LoadHtml(new WebClient().DownloadString("http://forums.asp.net/members/Mikesdotnetting.aspx"));
            //var root = html.DocumentNode;
            //var p = root.Descendants()
            //    .Where(n => n.GetAttributeValue("class", "").Equals("module-profile-recognition"))
            //    .Single()
            //    .Descendants("p")
            //    .Single();
            //var content = p.InnerText;

            //HttpClient http = new HttpClient();
            //var response = await http.GetByteArrayAsync(website);
            //String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
            //source = WebUtility.HtmlDecode(source);
            //HtmlDocument resultat = new HtmlDocument();
            //resultat.LoadHtml(source);

            //List<HtmlNode> toftitle = resultat.DocumentNode.Descendants().Where(
            //    x => (x.Name == "div" && x.Attributes["class"] != null &&
            //    x.Attributes["class"].Value.Contains("block_content"))).ToList();

            //return source;
            #endregion
        }

        public string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
    }
}