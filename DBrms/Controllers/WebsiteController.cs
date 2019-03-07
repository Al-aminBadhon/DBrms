using DBrms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBrms.Controllers
{
    public class WebsiteController : Controller
    {
        dbrmsEntities1 db = new dbrmsEntities1();
        // GET: Website
        public ActionResult Index()
        {
            List<Slider> sliders = db.Sliders.ToList();
            ViewBag.Sliders = sliders;

            List<Newspanel> newspanels = db.Newspanels.ToList();
            ViewBag.Newspanels = newspanels;

            List<TradingRestaurant> tradingRestaurants = db.TradingRestaurants.ToList();
            ViewBag.TradingRestaurants = tradingRestaurants;

            return View();
        }
        public ActionResult Magazine()
        {
            

            List<Magazine> magazines = db.Magazines.ToList();
            ViewBag.Magazines = magazines;

            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }
    }
}