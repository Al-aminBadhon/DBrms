using DBrms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace DBrms.Controllers
{
    public class WebsiteController : Controller
    {
        dbrmsEntities db = new dbrmsEntities();
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
        public ActionResult Magazine(int ? page)
        {

            var magazines = db.Magazines.ToList().ToPagedList(page ?? 1, 9);
            ViewBag.Magazines = magazines;
           

            return View(magazines);
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        ////  public ActionResult Restaurants()
        //  {
        //     var restaurants = db.Restaurants.ToList();
        //      ViewBag.Restaurants = restaurants;

        //      return View(restaurants);
        //  }

           // [ActionName("Restaurant")]
        public ActionResult Restaurants(int? page, string search)
        {
            if(search != null)
            {
                return View(db.Restaurants.Where(x => x.Name.StartsWith(search)).ToList().ToPagedList(page ?? 1, 3));
            }

            var restaurants = db.Restaurants.ToList().ToPagedList(page ?? 1,3);
            ViewBag.Restaurants = restaurants;

            return View(restaurants);
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

        public ActionResult Checkout(/*int? id*/)
        {
            
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //Food food = db.Foods.Find(id);
            //if (food == null)
            //{
            //    return HttpNotFound();
            //}
            //Session["FoodId"] = db.Foods.ToList();
            //Session["CustomerId"] = db.Customers.ToList();
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

    }
}