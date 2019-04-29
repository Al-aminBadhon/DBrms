using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DBrms.Models;
using PagedList;
using PagedList.Mvc;

namespace DBrms.Controllers
{
    public class RestaurantsController : Controller
    {
        dbrmsEntities db = new dbrmsEntities();

        // GET: Restaurants
        public ActionResult Index(int? id)
        {
            if (Session["RestaurantsId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            id = Convert.ToInt32(Session["RestaurantsId"]);

            List<Review> reviews = db.Reviews.Where(x => x.RestaurantsId == id).ToList();
            ViewBag.ReivewAll = reviews.Count();

            List<ReviewFood> reviewFoods = db.ReviewFoods.Where(x => x.Food.RestaurantId == id).ToList();
            ViewBag.ReivewFoodAll = reviewFoods.Count();

            List<FoodCart> foodCarts = db.FoodCarts.Where(x => x.Food.RestaurantId == id).ToList();
            ViewBag.FoodCarts = foodCarts.Count();

            List<FoodCart> foodDelivered = db.FoodCarts.Where(x => x.Food.RestaurantId == id && x.PaidAmount != null).ToList();
            ViewBag.FoodDelivered = foodDelivered.Count();

            List<Food> foods = db.Foods.Where(x => x.RestaurantId == id).ToList();
            ViewBag.FoddsTotal = foods.Count();



            List<Review> reviewstoday = new List<Review>();
            foreach (var item in db.Reviews.Where(x => x.RestaurantsId == id))
            {
                var datetoday = item.Date;
                DateTime daydate = Convert.ToDateTime(datetoday);
                string day = daydate.Date.ToString("yyyy-MM-dd");
                if(day==DateTime.Now.Date.ToString("yyyy-MM-dd"))
                {
                    reviewstoday.Add(item);
                }
            }
            ViewBag.ReivewToday = reviewstoday.Count();


            List<ReviewFood> reviewFoodsToday = new List<ReviewFood>();
            foreach (var item in db.ReviewFoods.Where(x => x.Food.RestaurantId == id))
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
            foreach (var item1 in db.FoodCarts.Where(x=> x.Food.RestaurantId== id ))
            {
                var datetoday = item1.Cart.Date;
                DateTime daydate = Convert.ToDateTime(datetoday);
                string day = daydate.Date.ToString("yyyy-MM-dd");
                if(day==DateTime.Now.Date.ToString("yyyy-MM-dd"))
                {
                    foodCartsToday.Add(item1);
                }
            }
            ViewBag.FoodCartsToday = foodCartsToday.Count();


            List<FoodCart> foodDeliveredToday = new List<FoodCart>();
            foreach (var item in db.FoodCarts.Where(x=> x.Food.RestaurantId== id && x.PaidAmount != null))
            {
                var datetoday = item.Cart.Date;
                DateTime daydate = Convert.ToDateTime(datetoday);
                string day = daydate.Date.ToString("yyyy-MM-dd");
                if(day==DateTime.Now.Date.ToString("yyyy-MM-dd"))
                {
                    foodDeliveredToday.Add(item);
                }
            }
            ViewBag.FoodDeliveredToday = foodDeliveredToday.Count();

            
            
            return View();
        }



        public ActionResult Menu(int? id, int? page, string search)
        {


            id = Convert.ToInt32(Session["RestaurantsId"]);
            List<Restaurant> restaurants = db.Restaurants.Where(X => X.RestaurantId == id).ToList();
            ViewBag.Restaurants = restaurants;

            if (search != null)
            {
                return View(db.Foods.Where(x => x.Name.StartsWith(search)).ToList().ToPagedList(page ?? 1, 3));
            }



            return View(db.Foods.Where(x => x.RestaurantId == id).ToList().ToPagedList(page ?? 1, 3));


        }

        [HttpGet]
        public ActionResult MenuAdd()
        {
            if (Session["RestaurantsId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int id = Convert.ToInt32(Session["RestaurantsId"]);
            return View();
        }
        [HttpPost]
        public ActionResult MenuAdd([Bind(Include = "Name,Image,Price,Details")] Food food, HttpPostedFileBase ImageFile)
        {


            if (ImageFile != null)
            {

                String filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                String extension = Path.GetExtension(ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                food.Image = "/Image/" + filename;
                filename = Path.Combine(Server.MapPath("/Image/"), filename);
                ImageFile.SaveAs(filename);
                int id = Convert.ToInt32(Session["RestaurantsId"]);
                food.RestaurantId = id;
                db.Foods.Add(food);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Menu");

            }
            return View();
        }
        [HttpGet]
        public ActionResult MenuEdit(int? id)
        {
            if (Session["RestaurantsId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            var menuedit = db.Foods.Find(id);
            if (menuedit == null)
            {
                return HttpNotFound();
            }
            return View(menuedit);
        }
        [HttpPost]
        public ActionResult MenuEdit([Bind(Include = "Name,Image,Price,Details")]Food food, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Menu");
            }

            return View();
        }



        public ActionResult PopularMenu()
        {
            return View(db.Foods.ToList());
        }

        [HttpGet]
        public ActionResult PopularMenuAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PopularMenuAdd([Bind(Include = "Name,Image,Price,Details")] Food food, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null)
            {
                String filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                String extension = Path.GetExtension(ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                food.Image = "/Image/" + filename;
                filename = Path.Combine(Server.MapPath("/Image/"), filename);
                ImageFile.SaveAs(filename);

                db.Foods.Add(food);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Newspanel");

            }
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (Session["RestaurantsId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            id = Convert.ToInt32(Session["RestaurantsId"]);

            return View(db.Restaurants.Where(x => x.RestaurantId == id).FirstOrDefault());
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (Session["RestaurantsId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Name");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Restaurant edit = db.Restaurants.Find(id);
            if (edit == null)
            {
                return HttpNotFound();
            }
            return View(edit);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "RestaurantId,Name,Address,Phone,Picture,LocationId,PopularMenu,CostPerOrder,Time,Cuisine,Extra,Discount,UserName,Password")] Restaurant restaurant)
        {
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
        }

        public ActionResult RestaurantReviewList(int? id, int? page)
        {
            if (Session["RestaurantsId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            id = Convert.ToInt32(Session["RestaurantsId"]);

            return View(db.Reviews.Where(x => x.RestaurantsId == id).ToList().ToPagedList(page ?? 1, 5));

        }
        public ActionResult FoodReviewList(int? id, int? page)
        {
            if (Session["RestaurantsId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            id = Convert.ToInt32(Session["RestaurantsId"]);

            return View(db.ReviewFoods.Where(x => x.Food.RestaurantId == id).ToList().ToPagedList(page ?? 1, 5));

        }

        public ActionResult RestaurantOrderList(int? id, int? page)
        {
            if (Session["RestaurantsId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            id = Convert.ToInt32(Session["RestaurantsId"]);

            return View(db.FoodCarts.Where(x=> x.Food.RestaurantId == id && x.IsConfirm == null).ToList().ToPagedList(page ??1,5));
        }

        [HttpGet]
        public ActionResult RestaurantOrderListEdit(int? id, int? page)
        {
            if (Session["RestaurantsId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            FoodCart foodCart = db.FoodCarts.Find(id);
            if (foodCart == null)
            {
                return HttpNotFound();
            }

            return View(foodCart);
        }

        [HttpPost]
        public ActionResult RestaurantOrderListEdit([Bind(Include ="FoodCartId,FoodId,CartId,Quantity,Price,IsConfirm")] FoodCart foodCart)
        {
         if(ModelState.IsValid)
            {
                db.Entry(foodCart).State = EntityState.Modified;    
                db.SaveChanges();
                return RedirectToAction("RestaurantOrderList");
            }

            return View();
        }
        [HttpGet]
        public ActionResult RestaurantCashOnDelivary(int? id, int? page)
        {
            if (Session["RestaurantsId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            id = Convert.ToInt32(Session["RestaurantsId"]);
            var foodlist = db.FoodCarts.Where(x => x.Food.RestaurantId == id && x.IsConfirm == true).ToList().ToPagedList(page ?? 1,5);

            return View(foodlist);

        }

        [HttpPost]
        public ActionResult RestaurantCashOnDelivary([Bind(Include = "FoodCartId,CartId,FoodId,Quantity,PaidAmount,Price,IsConfirm")]FoodCart foodCart, float PaidAmount, int CartId, int RestaurantId)
        {
           
            Transaction transaction = new Transaction();
           
               
                foodCart.PaidAmount = PaidAmount;
           
          
                db.FoodCarts.Add(foodCart);
                db.Entry(foodCart).State = EntityState.Modified;
                db.SaveChanges();
                transaction.RestaurantId = RestaurantId;
                transaction.PaidAmount = PaidAmount;
                transaction.Date = DateTime.Now;
                transaction.CartId = CartId;
                db.Transactions.Add(transaction);
                db.SaveChanges();
           



            return RedirectToAction("RestaurantCashOnDelivary");
        }


        public ActionResult RestaurantRejectedOrderList(int? id, int? page)
        {
            if (Session["RestaurantsId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            id = Convert.ToInt32(Session["RestaurantsId"]);
            var foodlist = db.FoodCarts.Where(x => x.Food.RestaurantId == id && x.IsConfirm == false).ToList().ToPagedList(page ?? 1,5);

            return View(foodlist);

        }


    }
}
