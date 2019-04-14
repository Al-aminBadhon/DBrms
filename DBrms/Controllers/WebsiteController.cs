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
        public ActionResult Magazine(int? page)
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
        public ActionResult Restaurants(int? page, string search, string range,string discount)
        {
            if (search != null && range == null && discount == null)
            {
                return View(db.Restaurants.Where(x => x.Name.StartsWith(search)).ToList().ToPagedList(page ?? 1, 3));
            }
            if (range == "400-499")
            {

                var range1 = db.Restaurants.Where(x => x.CostPerOrder.Contains("400")).ToList().ToPagedList(page ?? 1, 3);
                return View(range1);
            }
            if (range == "300-399")
            {
                
                var range2 = db.Restaurants.Where(x => x.CostPerOrder.Contains("300")).ToList().ToPagedList(page ?? 1, 3);
                return View(range2);
            }
            if (range == "200-299")
            {
                
                var range3 = db.Restaurants.Where(x => x.CostPerOrder.Contains("200")).ToList().ToPagedList(page ?? 1, 3);
                return View(range3);
            }
            if (range == "100-199")
            {
                var range4 = db.Restaurants.Where(x => x.CostPerOrder.Contains("100")).ToList().ToPagedList(page ?? 1, 3);
                return View(range4);
            }


            if (discount == "5%")
            {
                var discount1 = db.Restaurants.Where(x => x.Discount.StartsWith("5")).ToList().ToPagedList(page ?? 1, 3);
                return View(discount1);
            }
            if (discount == "10%")
            {
                var discount2 = db.Restaurants.Where(x => x.Discount.Contains("10")).ToList().ToPagedList(page ?? 1, 3);
                return View(discount2);
            }
            if (discount == "15%")
            {
                var discount3 = db.Restaurants.Where(x => x.Discount.Contains("15")).ToList().ToPagedList(page ?? 1, 3);
                return View(discount3);
            }
            var restaurants = db.Restaurants.ToList().ToPagedList(page ?? 1, 3);
            ViewBag.Restaurants = restaurants;

            return View(restaurants);
        }

        public ActionResult RestaurantProfile(int? id)
        {
            List<Review> reviews = db.Reviews.Where(x => x.RestautantId == id).ToList();
            ViewBag.Reviews = reviews;

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

            Food food = db.Foods.Find(id);
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

        public ActionResult PriceRange()
        {
            return View();
        }

    }
}