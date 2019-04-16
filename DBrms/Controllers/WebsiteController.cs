using DBrms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.IO;

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

        public ActionResult MagazineSingle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magazine magazine = db.Magazines.Find(id);
            ViewBag.Magazines = magazine;
            if (magazine == null)
            {
                return HttpNotFound();
            }
            return View(magazine);
        }

        public ActionResult ContactUs()
        {
            return View();
        }


        public ActionResult Restaurants(int? page, string search, string range, string discount, string id)
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

            //Logic for Discount
            //Logic for Discount

            if (discount == "5%" && range == null)
            {
                var discount1 = db.Restaurants.Where(x => x.Discount.StartsWith("5")).ToList().ToPagedList(page ?? 1, 3);
                return View(discount1);
            }
            if (discount == "10%" && range == null)
            {
                var discount2 = db.Restaurants.Where(x => x.Discount.Contains("10")).ToList().ToPagedList(page ?? 1, 3);
                return View(discount2);
            }
            if (discount == "15%" && range == null)
            {
                var discount3 = db.Restaurants.Where(x => x.Discount.Contains("15")).ToList().ToPagedList(page ?? 1, 3);
                return View(discount3);
            }

            //logic for Loaction
            //logic for Loaction



            var restaurants = db.Restaurants.ToList().ToPagedList(page ?? 1, 3);
            ViewBag.Restaurants = restaurants;

            return View(restaurants);
        }

        public ActionResult RestaurantsLocation(string id, int? page)
        {
            if (id == "Uttara")
            {
                var location1 = db.Restaurants.Where(x => x.LocationId == 1).ToList().ToPagedList(page ?? 1, 3);
                return View(location1);
            }
            if (id == "Mirpur")
            {
                var location2 = db.Restaurants.Where(x => x.LocationId == 2).ToList().ToPagedList(page ?? 1, 3);
                return View(location2);
            }
            if (id == "Dhanmondi")
            {
                var location3 = db.Restaurants.Where(x => x.LocationId == 3).ToList().ToPagedList(page ?? 1, 3);
                return View(location3);
            }
            if (id == "300 Feet")
            {
                var location4 = db.Restaurants.Where(x => x.LocationId == 4).ToList().ToPagedList(page ?? 1, 3);
                return View(location4);
            }
            if (id == "Banani")
            {
                var location5 = db.Restaurants.Where(x => x.LocationId == 5).ToList().ToPagedList(page ?? 1, 3);
                return View(location5);
            }
            if (id == "Basundhara R Area")
            {
                var location6 = db.Restaurants.Where(x => x.LocationId == 6).ToList().ToPagedList(page ?? 1, 3);
                return View(location6);
            }
            if (id == "Mohammad Pur")
            {
                var location7 = db.Restaurants.Where(x => x.LocationId == 7).ToList().ToPagedList(page ?? 1, 3);
                return View(location7);
            }
            if (id == "Gulsan-1")
            {
                var location8 = db.Restaurants.Where(x => x.LocationId == 8).ToList().ToPagedList(page ?? 1, 3);
                return View(location8);
            }
            if (id == "Gulsan-2")
            {
                var location9 = db.Restaurants.Where(x => x.LocationId == 9).ToList().ToPagedList(page ?? 1, 3);
                return View(location9);
            }
            if (id == "Jamuna Future Park")
            {
                var location10 = db.Restaurants.Where(x => x.LocationId == 10).ToList().ToPagedList(page ?? 1, 3);
                return View(location10);
            }
            if (id == "Basundhara City")
            {
                var location11 = db.Restaurants.Where(x => x.LocationId == 11).ToList().ToPagedList(page ?? 1, 3);
                return View(location11);
            }
            var restaurants = db.Restaurants.ToList().ToPagedList(page ?? 1, 3);
            ViewBag.Restaurants = restaurants;
            return View(restaurants);
        }

        [HttpGet]
        public ActionResult RestaurantProfile(int? id)
        {
            List<Review> reviews = db.Reviews.Where(x => x.RestautanstId == id).ToList();
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
        [HttpPost]
        public ActionResult RestaurantProfile(int RestaurantId, string Description)
        {
            Review review = new Review();
            if (Session["CustomerId"] != null)
            {
                int id = Convert.ToInt32(Session["CustomerId"]);
                review.CustomerId = id;

                review.RestautanstId = RestaurantId;
                review.Description = Description;

                if (ModelState.IsValid)
                {
                    db.Reviews.Add(review);
                    db.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("RestaurantProfile");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
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

        [HttpGet]
        public ActionResult CustomerRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CustomerRegistration([Bind(Include = "Name,Address,Image,Phone,Username,Password")] Customer customer, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null)
            {
                String fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                String extension = Path.GetExtension(ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                customer.Image = "/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("/Image/"), fileName);
                ImageFile.SaveAs(fileName);

                db.Customers.Add(customer);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("Index", "Login");
            }
            return View();
        }


        [HttpGet]
        public ActionResult RestaurantRegistration()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult RestaurantRegistration([Bind(Include = "Name,Address,Phone,picture,LocationId,CostPerOrder,Cuisine")] Restaurant restaurant, HttpPostedFileBase ImageFile, int? LocationId)
        {
            int er = 0;
            if (LocationId == null)
            {
                er++;
            }
            if (er > 0)
            {
                return View("Index");
            }

            if (ImageFile != null)
            {
                String fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                String extension = Path.GetExtension(ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                restaurant.Picture = "/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("/Image/"), fileName);
                ImageFile.SaveAs(fileName);

                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Index", "Login");
            }
            return View();
        }


    }
}