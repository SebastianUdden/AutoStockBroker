using AutoStockBroker.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AutoStockBroker.Controllers
{
    public class PrototypeController : Controller
    {
        public ActionResult StockCatalogues()
        {
            #region NoInternetConnection
            StockPortfolio largeCapPortfolio = new StockPortfolio("Large Cap");
            StockPortfolio avanzaLargeCapStockPortfolio = AvanzaParsers.ParseAvanzaOldList("https://www.avanza.se/aktier/gamla-aktielistan.html?countryCode=SE&marketPlaceOrList=LIST_LargeCap.SE&sortField=NAME&sortOrder=ASCENDING&activeTab=quote", largeCapPortfolio);
            //StockPortfolio viewAvanzaLargeCapStockPortfolio = Calculator.SetStockParameters(avanzaLargeCapStockPortfolio);
            StockPortfolio avanzaLargeCapStockPortfolioHistory = AvanzaParsers.ParseAvanzaOldListHistory("https://www.avanza.se/aktier/gamla-aktielistan.html?countryCode=SE&marketPlaceOrList=LIST_LargeCap.SE&sectorId=ALL&page=1&sortField=NAME&sortOrder=ASCENDING&activeTab=history", avanzaLargeCapStockPortfolio);
            StockPortfolio avanzaLargeCapStockPortfolioOverview = AvanzaParsers.ParseAvanzaOldListOverview("https://www.avanza.se/aktier/gamla-aktielistan.html?countryCode=SE&marketPlaceOrList=LIST_LargeCap.SE&sectorId=ALL&page=1&sortField=NAME&sortOrder=ASCENDING&activeTab=overview", avanzaLargeCapStockPortfolioHistory);
            StockPortfolio viewAvanzaLargeCapStockPortfolioOverview = Calculator.SetStockParameters(avanzaLargeCapStockPortfolioOverview);

            StockPortfolio midCapPortfolio = new StockPortfolio("Mid Cap");
            StockPortfolio avanzaMidCapStockPortfolio = AvanzaParsers.ParseAvanzaOldList("https://www.avanza.se/aktier/gamla-aktielistan.html?countryCode=SE&marketPlaceOrList=LIST_MidCap.SE&sortField=NAME&sortOrder=ASCENDING&activeTab=quote", midCapPortfolio);
            //StockPortfolio viewAvanzaMidCapStockPortfolio = Calculator.SetStockParameters(avanzaMidCapStockPortfolio);
            StockPortfolio avanzaMidCapStockPortfolioHistory = AvanzaParsers.ParseAvanzaOldListHistory("https://www.avanza.se/aktier/gamla-aktielistan.html?countryCode=SE&marketPlaceOrList=LIST_LargeCap.SE&sectorId=ALL&page=1&sortField=NAME&sortOrder=ASCENDING&activeTab=history", avanzaMidCapStockPortfolio);
            StockPortfolio avanzaMidCapStockPortfolioOverview = AvanzaParsers.ParseAvanzaOldListOverview("https://www.avanza.se/aktier/gamla-aktielistan.html?countryCode=SE&marketPlaceOrList=LIST_MidCap.SE&sortField=NAME&sortOrder=ASCENDING&activeTab=overview", avanzaMidCapStockPortfolioHistory);
            StockPortfolio viewAvanzaMidCapStockPortfolioOverview = Calculator.SetStockParameters(avanzaMidCapStockPortfolioOverview);


            StockPortfolio smallCapPortfolio = new StockPortfolio("Small Cap");
            StockPortfolio avanzaSmallCapStockPortfolio = AvanzaParsers.ParseAvanzaOldList("https://www.avanza.se/aktier/gamla-aktielistan.html?countryCode=SE&marketPlaceOrList=LIST_SmallCap.SE&sortField=NAME&sortOrder=ASCENDING&activeTab=quote", smallCapPortfolio);
            //StockPortfolio viewAvanzaSmallCapStockPortfolio = Calculator.SetStockParameters(avanzaSmallCapStockPortfolio);
            StockPortfolio avanzaSmallCapStockPortfolioHistory = AvanzaParsers.ParseAvanzaOldListHistory("https://www.avanza.se/aktier/gamla-aktielistan.html?countryCode=SE&marketPlaceOrList=LIST_LargeCap.SE&sectorId=ALL&page=1&sortField=NAME&sortOrder=ASCENDING&activeTab=history", avanzaSmallCapStockPortfolio);
            StockPortfolio avanzaSmallCapStockPortfolioOverview = AvanzaParsers.ParseAvanzaOldListOverview("https://www.avanza.se/aktier/gamla-aktielistan.html?countryCode=SE&marketPlaceOrList=LIST_SmallCap.SE&sortField=NAME&sortOrder=ASCENDING&activeTab=overview", avanzaSmallCapStockPortfolioHistory);
            StockPortfolio viewAvanzaSmallCapStockPortfolioOverview = Calculator.SetStockParameters(avanzaSmallCapStockPortfolioOverview);

            StockPortfolio allAvanzaStocks = new StockPortfolio("Avanza Stocks");
            allAvanzaStocks.Stocks = AddAllStocks(viewAvanzaLargeCapStockPortfolioOverview.Stocks, viewAvanzaMidCapStockPortfolioOverview.Stocks, viewAvanzaSmallCapStockPortfolioOverview.Stocks);
            allAvanzaStocks.Stocks = allAvanzaStocks.Stocks.OrderBy(x => x.Name).ToList();
            StockPortfolio allAvanzaStocksView = Calculator.SetStockParameters(allAvanzaStocks);

            string allAvanzaStocksJson = JsonCreator.CreateJson(allAvanzaStocksView);
            JsonCreator.SaveToRoot(allAvanzaStocksJson);
            #endregion
            ViewBag.Message = "These are the stocks currently added.";
            return View(allAvanzaStocksView);
        }

        public ActionResult MyStockPortfolio()
        {
            //JsonCreator.SaveToRoot();
            return View();
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