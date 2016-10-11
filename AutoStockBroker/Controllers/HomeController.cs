using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult PrototypePage()
        {
            ViewBag.Message = "This is the first iteration of the website. We will put up new versions of this page continually until the product reaches it's final completed form.";

            return View();
        }
    }
}