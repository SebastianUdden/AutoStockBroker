using AutoStockBroker.Models.Stocks;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("WelcomePage", "Home");
        }

        public ActionResult WelcomePage()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "AutoStockBroker is a website/tool created to help Tim and Sebastian make predictions in the stock market, managing their portfolios and brokering deals through the web.";

            return View();
        }

        public ActionResult MasterPlan()
        {
            ViewBag.Message = "This is where we do goalsetting, create milestones and weigh pros and cons in order to keep the project moving forward on the right track.";

            return View();
        }

        public ActionResult GreatLinks()
        {
            ViewBag.Message = "Here we gather links that are of interest when building the back-end REST-API, the web front page and all financial information of interest.";

            return View();
        }

        public ActionResult FinancialSyntax()
        {
            ViewBag.Message = "Information about different syntax and financial methods will be added here.";

            return View();
        }
        public ActionResult ProgrammingSyntax()
        {
            ViewBag.Message = "Information about different syntax and programming methods will be added here.";

            return View();
        }

        //public ActionResult PrototypePage()
        //{
        //    StockPortfolio listOfStocks = new StockPortfolio();
        //    listOfStocks.StockCatalogueName = "Börsdata";
        //    listOfStocks.TotalValue = 0;
        //    listOfStocks.Stocks = new List<Stock>
        //    {
        //        new Stock
        //        {
        //            Name = "2e-group",
        //            AmountOwned = 5
        //        },
        //        new Stock
        //        {
        //            Name = "a-city-media",
        //            AmountOwned = 1
        //        },
        //        new Stock
        //        {
        //            Name = "ap-moller-maersk",
        //            AmountOwned = 3
        //        },
        //        new Stock
        //        {
        //            Name = "a1m-pharma",
        //            AmountOwned = 5
        //        },
        //        new Stock
        //        {
        //            Name = "aalborg-bolspilklub",
        //            AmountOwned = 10
        //        },
        //        new Stock
        //        {
        //            Name = "aarhus-elite",
        //            AmountOwned = 15
        //        },
        //        new Stock
        //        {
        //            Name = "aarhuskarlshamn",
        //            AmountOwned = 7
        //        },
        //        new Stock
        //        {
        //            Name = "aasen-sparebank",
        //            AmountOwned = 30
        //        },
        //        new Stock
        //        {
        //            Name = "abb",
        //            AmountOwned = 36
        //        }
        //    };

        //    foreach (var stock in listOfStocks.Stocks)
        //    {
        //        stock.Value = Convert.ToDecimal(ParseHTML("https://borsdata.se/" + stock.Name + "/nyckeltal"));
        //        stock.ValueOwned = stock.Value * stock.AmountOwned;
        //        listOfStocks.TotalValue += stock.ValueOwned;
        //    }

        //    foreach (var stock in listOfStocks.Stocks)
        //    {
        //        stock.Weight = listOfStocks.TotalValue / stock.ValueOwned;
        //    }

        //    ViewBag.Message = "These are the stocks currently added.";
        //    return View(listOfStocks);
        //}

        //private string ParseHTML(string website)
        //{
        //    var html = new HtmlDocument();
        //    html.LoadHtml(new WebClient().DownloadString(website));
        //    var root = html.DocumentNode;
        //    var topbox = root.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("RightTopBoxBig"));
        //    var table = topbox.Single()
        //        .Descendants("table").Single();
        //    var content = table.Descendants("tr").ElementAt(1).Descendants("td").Single().InnerHtml;
        //    var stockValue = Regex.Match(content, @"\d+(\,\d{1,2})?").Value;

        //    return stockValue;

        //    #region Not used
        //    //var html = new HtmlDocument();
        //    //html.LoadHtml(new WebClient().DownloadString("http://forums.asp.net/members/Mikesdotnetting.aspx"));
        //    //var root = html.DocumentNode;
        //    //var p = root.Descendants()
        //    //    .Where(n => n.GetAttributeValue("class", "").Equals("module-profile-recognition"))
        //    //    .Single()
        //    //    .Descendants("p")
        //    //    .Single();
        //    //var content = p.InnerText;

        //    //HttpClient http = new HttpClient();
        //    //var response = await http.GetByteArrayAsync(website);
        //    //String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
        //    //source = WebUtility.HtmlDecode(source);
        //    //HtmlDocument resultat = new HtmlDocument();
        //    //resultat.LoadHtml(source);

        //    //List<HtmlNode> toftitle = resultat.DocumentNode.Descendants().Where(
        //    //    x => (x.Name == "div" && x.Attributes["class"] != null &&
        //    //    x.Attributes["class"].Value.Contains("block_content"))).ToList();

        //    //return source;
        //    #endregion
        //}
    }
}