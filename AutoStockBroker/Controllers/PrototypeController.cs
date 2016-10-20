using AutoStockBroker.Models;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
            StockPortfolio largeCapPortfolio = new StockPortfolio("Large Cap");
            StockPortfolio avanzaLargeCapStockPortfolio = AvanzaParsers.ParseAvanzaOldList("https://www.avanza.se/aktier/gamla-aktielistan.html?countryCode=SE&marketPlaceOrList=LIST_LargeCap.SE&sortField=NAME&sortOrder=ASCENDING&activeTab=quote", largeCapPortfolio);
            StockPortfolio viewAvanzaLargeCapStockPortfolio = Calculator.SetStockParameters(avanzaLargeCapStockPortfolio);
            StockPortfolio avanzaLargeCapStockPortfolioOverview = AvanzaParsers.ParseAvanzaOldListOverview("https://www.avanza.se/aktier/gamla-aktielistan.html?countryCode=SE&marketPlaceOrList=LIST_LargeCap.SE&sectorId=ALL&page=1&sortField=NAME&sortOrder=ASCENDING&activeTab=overview", viewAvanzaLargeCapStockPortfolio);
            StockPortfolio viewAvanzaLargeCapStockPortfolioOverview = Calculator.SetStockParameters(avanzaLargeCapStockPortfolioOverview);

            StockPortfolio midCapPortfolio = new StockPortfolio("Mid Cap");
            StockPortfolio avanzaMidCapStockPortfolio = AvanzaParsers.ParseAvanzaOldList("https://www.avanza.se/aktier/gamla-aktielistan.html?countryCode=SE&marketPlaceOrList=LIST_MidCap.SE&sortField=NAME&sortOrder=ASCENDING&activeTab=quote", midCapPortfolio);
            StockPortfolio viewAvanzaMidCapStockPortfolio = Calculator.SetStockParameters(avanzaMidCapStockPortfolio);
            StockPortfolio avanzaMidCapStockPortfolioOverview = AvanzaParsers.ParseAvanzaOldListOverview("https://www.avanza.se/aktier/gamla-aktielistan.html?countryCode=SE&marketPlaceOrList=LIST_MidCap.SE&sortField=NAME&sortOrder=ASCENDING&activeTab=overview", viewAvanzaMidCapStockPortfolio);
            StockPortfolio viewAvanzaMidCapStockPortfolioOverview = Calculator.SetStockParameters(avanzaMidCapStockPortfolioOverview);


            StockPortfolio smallCapPortfolio = new StockPortfolio("Small Cap");
            StockPortfolio avanzaSmallCapStockPortfolio = AvanzaParsers.ParseAvanzaOldList("https://www.avanza.se/aktier/gamla-aktielistan.html?countryCode=SE&marketPlaceOrList=LIST_SmallCap.SE&sortField=NAME&sortOrder=ASCENDING&activeTab=quote", smallCapPortfolio);
            StockPortfolio viewAvanzaSmallCapStockPortfolio = Calculator.SetStockParameters(avanzaSmallCapStockPortfolio);
            StockPortfolio avanzaSmallCapStockPortfolioOverview = AvanzaParsers.ParseAvanzaOldListOverview("https://www.avanza.se/aktier/gamla-aktielistan.html?countryCode=SE&marketPlaceOrList=LIST_SmallCap.SE&sortField=NAME&sortOrder=ASCENDING&activeTab=overview", viewAvanzaSmallCapStockPortfolio);
            StockPortfolio viewAvanzaSmallCapStockPortfolioOverview = Calculator.SetStockParameters(avanzaSmallCapStockPortfolioOverview);

            //AvanzaPortfolio avanzaAllCapStockPortfolio = new AvanzaPortfolio()
            //{
            //    StockCatalogueName = "AvanzaAllCapStockPortfolio",
            //    LargeCapStocks = viewAvanzaLargeCapStockPortfolioOverview.Stocks,
            //    //LargeCapStocksOverview = viewAvanzaLargeCapStockPortfolioOverview.Stocks,
            //    MidCapStocks = viewAvanzaMidCapStockPortfolio.Stocks,
            //    SmallCapStocks = viewAvanzaSmallCapStockPortfolio.Stocks,
            //};

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
            StockPortfolio allAvanzaStocks = new StockPortfolio("Avanza Stocks");
            allAvanzaStocks.Stocks = AddAllStocks(viewAvanzaLargeCapStockPortfolioOverview.Stocks, viewAvanzaMidCapStockPortfolioOverview.Stocks, viewAvanzaSmallCapStockPortfolioOverview.Stocks);
            allAvanzaStocks.Stocks = allAvanzaStocks.Stocks.OrderBy(x => x.Name).ToList();
            StockPortfolio allAvanzaStocksView = Calculator.SetStockParameters(allAvanzaStocks);

            string allAvanzaStocksJson = JsonCreator.CreateJson(allAvanzaStocksView);
            JsonCreator.SaveToRoot(allAvanzaStocksJson);
            ViewBag.Message = "These are the stocks currently added.";
            return View(allAvanzaStocksView);
        }

        public static List<Stock> AddAllStocks(List<Stock> smallCap, List<Stock> midCap, List<Stock> largeCap)
        {
            List<Stock> allCaps = new List<Stock>();

            foreach (var stock in smallCap)
            {
                allCaps.Add(stock);
            }
            foreach (var stock in midCap)
            {
                allCaps.Add(stock);
            }
            foreach (var stock in largeCap)
            {
                allCaps.Add(stock);
            }

            return allCaps;
        }
    }
}