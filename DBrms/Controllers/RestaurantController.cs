using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace DBrms.Controllers
{
    public class RestaurantController : Controller
    {
        
        // GET: Restaurant
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Menu ()
        {
            return View();
        }
        public ActionResult PopularMenu()
        {
            return View();
        }
        public ActionResult Address ()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

    }
}