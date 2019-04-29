using DBrms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Data.Entity;
using PagedList;
using PagedList.Mvc;

namespace DBrms.Controllers
{
    public class AdminController : Controller
    {
        dbrmsEntities db = new dbrmsEntities();

        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }


            List<Review> reviews = db.Reviews.ToList();
            ViewBag.ReivewAll = reviews.Count();

            List<ReviewFood> reviewFoods = db.ReviewFoods.ToList();
            ViewBag.ReivewFoodAll = reviewFoods.Count();

            List<FoodCart> foodCarts = db.FoodCarts.ToList();
            ViewBag.FoodCarts = foodCarts.Count();

            List<FoodCart> foodDelivered = db.FoodCarts.Where(x => x.PaidAmount != null).ToList();
            ViewBag.FoodDelivered = foodDelivered.Count();

            List<Customer> customers = db.Customers.ToList();
            ViewBag.TotalCustomers = customers.Count();

            List<Restaurant> restaurants = db.Restaurants.ToList();
            ViewBag.TotalRestaurants = restaurants.Count();

            List<Magazine> magazines = db.Magazines.ToList();
            ViewBag.MagazinesAll = magazines.Count();



            List<Review> reviewstoday = new List<Review>();
            foreach (var item in db.Reviews)
            {
                var datetoday = item.Date;
                DateTime daydate = Convert.ToDateTime(datetoday);
                string day = daydate.Date.ToString("yyyy-MM-dd");
                if (day == DateTime.Now.Date.ToString("yyyy-MM-dd"))
                {
                    reviewstoday.Add(item);
                }
            }
            ViewBag.ReivewToday = reviewstoday.Count();


            List<ReviewFood> reviewFoodsToday = new List<ReviewFood>();
            foreach (var item in db.ReviewFoods)
            {
                var datetoday = item.Date;
                DateTime daydate = Convert.ToDateTime(datetoday);
                string day = daydate.Date.ToString("yyyy-MM-dd");
                if (day == DateTime.Now.Date.ToString("yyyy-MM-dd"))
                {
                    reviewFoodsToday.Add(item);
                }
            }
            ViewBag.ReivewFoodsToday = reviewFoodsToday.Count();



            List<FoodCart> foodCartsToday = new List<FoodCart>();
            foreach (var item1 in db.FoodCarts)
            {
                var datetoday = item1.Cart.Date;
                DateTime daydate = Convert.ToDateTime(datetoday);
                string day = daydate.Date.ToString("yyyy-MM-dd");
                if (day == DateTime.Now.Date.ToString("yyyy-MM-dd"))
                {
                    foodCartsToday.Add(item1);
                }
            }
            ViewBag.FoodCartsToday = foodCartsToday.Count();


            List<FoodCart> foodDeliveredToday = new List<FoodCart>();
            foreach (var item in db.FoodCarts.Where(x => x.PaidAmount != null))
            {
                var datetoday = item.Cart.Date;
                DateTime daydate = Convert.ToDateTime(datetoday);
                string day = daydate.Date.ToString("yyyy-MM-dd");
                if (day == DateTime.Now.Date.ToString("yyyy-MM-dd"))
                {
                    foodDeliveredToday.Add(item);
                }
            }
            ViewBag.FoodDeliveredToday = foodDeliveredToday.Count();


            return View();
        }

        public ActionResult Slider(int? page, string search)
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (search != null)
            {
                return View(db.Sliders.Where(x => x.Name.Contains(search)).ToList().ToPagedList(page ?? 1, 5));
            }

            return View(db.Sliders.ToList().ToPagedList(page ?? 1, 3));
        }

        [HttpGet]
        public ActionResult SliderAdd()
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SliderAdd([Bind(Include = "Name,Details,IsActive")] Slider slider, HttpPostedFileBase ImageFile)

        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (ImageFile != null)
            {
                String filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                String extension = Path.GetExtension(ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                slider.Image = "/Image/" + filename;
                filename = Path.Combine(Server.MapPath("/Image/"), filename);
                ImageFile.SaveAs(filename);




                db.Sliders.Add(slider);
                db.SaveChanges();

                ModelState.Clear();
                return RedirectToAction("Slider");


            }
            return View();
        }



        [HttpGet]
        public ActionResult SliderEdit(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var edit = db.Sliders.Find(id);

            if (edit == null)
            {

                return HttpNotFound();
            }

            return View(edit);
        }

        [HttpPost]
        public ActionResult SliderEdit([Bind(Include = "SliderId,Name,Image,Details,IsActive")] Slider slider, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {

                db.Entry(slider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Slider");
            }
            return View();

        }



        [HttpGet]
        public ActionResult SliderDelete(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        public ActionResult SliderDelete(int id)
        {
            Slider slider = db.Sliders.Find(id);
            db.Sliders.Remove(slider);
            db.SaveChanges();
            return RedirectToAction("Slider");
        }



        public ActionResult CategoryBuffet()
        {


            return View();
        }
        public ActionResult Newspanel(int? page, string search)
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (search != null)
            {
                return View(db.Magazines.Where(x => x.Name.Contains(search)).ToList().ToPagedList(page ?? 1, 5));
            }

            return View(db.Magazines.ToList().ToPagedList(page ?? 1, 3));
        }


        [HttpGet]
        public ActionResult NewspanelEdit(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Magazine newspanelEdit = db.Magazines.Find(id);
            if (newspanelEdit == null)
            {
                return HttpNotFound();
            }
            return View(newspanelEdit);

        }
        [HttpPost]
        public ActionResult NewspanelEdit([Bind(Include = "MagazineId,Name,Image,Details,IsActive")] Magazine newspanel)
        {
            if (ModelState.IsValid)
            {

                db.Entry(newspanel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Newspanel");
            }
            return View();
        }



        public ActionResult TradingRestaurant(int? page, string search)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (search != null)
            {
                return View(db.Restaurants.Where(x => x.Name.Contains(search)).ToList().ToPagedList(page ?? 1, 5));
            }
            return View(db.Restaurants.ToList().ToPagedList(page ?? 1, 5));


        }


        [HttpGet]
        public ActionResult TradingRestaurantEdit(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Restaurant tradingrestaurantEdit = db.Restaurants.Find(id);
            if (tradingrestaurantEdit == null)
            {
                return HttpNotFound();
            }
            return View(tradingrestaurantEdit);

        }
        [HttpPost]
        public ActionResult TradingRestaurantEdit([Bind(Include = "RestaurantId,Name,Address,Phone,Picture,LocationId,PopularMenu,CostPerOrder,Time,Cuisine,Extra,Discount,Username,Password,IsActive")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {

                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TradingRestaurant");
            }
            return View();
        }



        public ActionResult Review(int? page, string search)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (search != null)
            {
                return View(db.Reviews.Where(x => x.Customer.Name.StartsWith(search)).ToList().ToPagedList(page ?? 1, 5));
            }


            return View(db.Reviews.ToList().ToPagedList(page ?? 1, 5));
        }

        [HttpGet]
        public ActionResult ReviewEdit(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);

        }
        [HttpPost]
        public ActionResult ReviewEdit([Bind(Include = "RestaurantId,CustomerId,Description")] Review review)
        {
            if (ModelState.IsValid)
            {

                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Review");
            }
            return View();
        }
        [HttpGet]
        public ActionResult ReviewDelete(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        public ActionResult ReviewDelete(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Review");
        }


        public ActionResult TopReview(int? page, string search)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (search != null)
            {
                return View(db.Reviews.Where(x => x.Customer.Name.StartsWith(search)).ToList().ToPagedList(page ?? 1, 5));
            }
            return View(db.Reviews.ToList().ToPagedList(page ?? 1, 5));
        }
        [HttpGet]
        public ActionResult TopReviewEdit(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);

        }
        [HttpPost]
        public ActionResult TopReviewEdit([Bind(Include = "ReviewId,RestaurantsId,CustomerId,Description,Rating,IsActive")] Review review)
        {
            if (ModelState.IsValid)
            {

                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TopReview");
            }
            return View();
        }

        public ActionResult ReviewFood(int? page, string search)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (search != null)
            {
                return View(db.ReviewFoods.Where(x => x.Customer.Name.StartsWith(search)).ToList().ToPagedList(page ?? 1, 5));
            }


            return View(db.ReviewFoods.ToList().ToPagedList(page ?? 1, 5));
        }

        [HttpGet]
        public ActionResult ReviewFoodDelete(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ReviewFood reviewFood = db.ReviewFoods.Find(id);
            if (reviewFood == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        public ActionResult ReviewFoodDelete(int id)
        {
            ReviewFood reviewFood = db.ReviewFoods.Find(id);
            db.ReviewFoods.Remove(reviewFood);
            db.SaveChanges();
            return RedirectToAction("ReviewFood");
        }

        public ActionResult TopReviewFood(int? page, string search)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (search != null)
            {
                return View(db.ReviewFoods.Where(x => x.Customer.Name.StartsWith(search)).ToList().ToPagedList(page ?? 1, 5));
            }
            return View(db.ReviewFoods.ToList().ToPagedList(page ?? 1, 5));
        }
        [HttpGet]
        public ActionResult TopReviewFoodEdit(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            ReviewFood reviewFood = db.ReviewFoods.Find(id);
            if (reviewFood == null)
            {
                return HttpNotFound();
            }
            return View(reviewFood);

        }
        [HttpPost]
        public ActionResult TopReviewFoodEdit([Bind(Include = "ReviewFoodId,FoodId,CustomerId,Description,RatingFood,IsActive")] ReviewFood reviewFood)
        {
            if (ModelState.IsValid)
            {

                db.Entry(reviewFood).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TopReviewFood");
            }
            return View();
        }

        public ActionResult Magazine(int? page, string search)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (search != null)
            {
                return View(db.Magazines.Where(x => x.Name.Contains(search)).ToList().ToPagedList(page ?? 1, 5));
            }
            return View(db.Magazines.ToList().ToPagedList(page ?? 1, 5));
        }

        [HttpGet]
        public ActionResult MagazineAdd()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult MagazineAdd([Bind(Include = "MagazineId,Name,Image,Details")] Magazine magazine, HttpPostedFileBase ImageFile)
        {
           
                String fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                String extension = Path.GetExtension(ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                magazine.Image = "/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("/Image/"), fileName);
                ImageFile.SaveAs(fileName);

                db.Magazines.Add(magazine);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Magazine");
         
        }


        [HttpGet]
        public ActionResult MagazineEdit(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Magazine magazine = db.Magazines.Find(id);
            if (magazine == null)
            {
                return HttpNotFound();
            }
            return View(magazine);
        }

        [HttpPost]
        public ActionResult MagazineEdit([Bind(Include = "MagazineId,Name,Image,Details")] Magazine magazine)
        {
            db.Entry(magazine).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Magazine");
        }


        [HttpGet]
        public ActionResult MagazineDelete(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Magazine magazine = db.Magazines.Find(id);
            if (magazine == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        public ActionResult MagazineDelete(int id)
        {
            Magazine magazine = db.Magazines.Find(id);
            db.Magazines.Remove(magazine);
            db.SaveChanges();
            return RedirectToAction("Magazine");
        }

        
        public ActionResult ManageRestaurant(int? page, string search)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (search != null)
            {
                return View(db.Restaurants.Where(x => x.Name.Contains(search)).ToList().ToPagedList(page ?? 1, 5));
            }
            return View(db.Restaurants.ToList().ToPagedList(page ?? 1, 5));
        }

        [HttpGet]
        public ActionResult RestaurantDelete(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        public ActionResult RestaurantDelete(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurant);
            db.SaveChanges();
            return RedirectToAction("ManageRestaurant");
        }



        [HttpGet]

        public ActionResult RestaurantAdd()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Name");
            List<Location> locations = db.Locations.ToList();
            ViewBag.Locations = locations;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RestaurantAdd([Bind(Include = "RestaurantId,Name,Address,Phone,picture,LocationId,CostPerOrder,Cuisine,Username,Password")] Restaurant restaurant, HttpPostedFileBase ImageFile, int? LocationId)
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
            return RedirectToAction("ManageRestaurant");
        }


        [HttpGet]
        public ActionResult ManageRestaurantEdit(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Name");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Restaurant managerestaurantEdit = db.Restaurants.Find(id);
            if (managerestaurantEdit == null)
            {
                return HttpNotFound();
            }
            return View(managerestaurantEdit);


        }
        [HttpPost]
        public ActionResult ManageRestaurantEdit([Bind(Include = "RestaurantId,Name,Address,Phone,Picture,LocationId,PopularMenu,CostPerOrder,Time,Cuisine,Extra,Discount,Username,Password")] Restaurant restaurant, string IsActive, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {

                if (IsActive=="on")
                {
                    restaurant.IsActive = true;
                }
                else
                {
                    restaurant.IsActive = false;
                }
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageRestaurant");
            }
            return View();
        }

        public ActionResult ManageCustomer(int? page, string search)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (search != null)
            {
                return View(db.Customers.Where(x => x.Name.Contains(search)).ToList().ToPagedList(page ?? 1, 5));
            }
            return View(db.Customers.ToList().ToPagedList(page ?? 1, 5));
        }


        [HttpGet]
        public ActionResult CustomerAdd()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CustomerAdd([Bind(Include = "CustomerId,Name,Address,Image,Phone,Username,Password")] Customer customer, HttpPostedFileBase ImageFile)
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
                return RedirectToAction("ManageCustomer");
        }

        [HttpGet]
        public ActionResult CustomerEdit(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public ActionResult CustomerEdit([Bind(Include = "CustomerId,Name,Address,Image,Phone,Username,Password")] Customer customer)
        {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageCustomer");
           
        }

        [HttpGet]
        public ActionResult CustomerDelete(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        public ActionResult CustomerDelete(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("ManageCustomer");
        }

        public ActionResult OrderList(int? page)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View(db.Carts.ToList().ToPagedList(page ?? 1, 5));
        }
        [HttpGet]
        public ActionResult OrderListDetails(int? id, int? page)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var foodlist = db.FoodCarts.Where(x => x.CartId == id).ToList().ToPagedList(page ?? 1, 5);
            return View(foodlist);
        }

        [HttpPost]
        public ActionResult OrderListDetails([Bind(Include = "FoodCartId,FoodId,CartId,Quantity,Price,PaidAmount,IsConfirm")] FoodCart foodCart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodCart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("OrderListDetails");
            }
            return View();
        }

    }
}