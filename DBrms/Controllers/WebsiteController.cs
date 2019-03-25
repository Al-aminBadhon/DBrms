using DBrms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            

            List<Magazine> magazines = db.Magazines.Where(x => x.IsActive == true).ToList();
            ViewBag.Magazines = magazines;

            List<Restaurant> restaurants = db.Restaurants.Where(x => x.IsActive == true).ToList();
            ViewBag.Restaurants = restaurants;

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

        public ActionResult Restaurants()
        {
            List<Restaurant> restaurants = db.Restaurants.ToList();
            ViewBag.Restaurants = restaurants;

            return View();
        }

        public ActionResult RestaurantProfile(int? id)
        {
            

            List<Food> foods = db.Foods.Where(x => x.RestaurantId == id).ToList();
            ViewBag.Foods = foods;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);

        }

        public ActionResult FoodSingle(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Food food  = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }
    }
}