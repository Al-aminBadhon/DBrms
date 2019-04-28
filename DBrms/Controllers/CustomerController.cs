using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DBrms.Models;
using PagedList;
using PagedList.Mvc;

namespace DBrms.Controllers
{
    public class CustomerController : Controller
    {
        dbrmsEntities db = new dbrmsEntities();
        // GET: Customer
        public ActionResult Index( int? id)
        {
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            id = Convert.ToInt32(Session["CustomerId"]);

            List<Review> reviews = db.Reviews.Where(x => x.CustomerId == id).ToList();
            ViewBag.ReivewAll = reviews.Count();

            List<ReviewFood> reviewFoods = db.ReviewFoods.Where(x => x.CustomerId == id).ToList();
            ViewBag.ReivewFoodAll = reviewFoods.Count();

            List<FoodCart> foodCarts = db.FoodCarts.Where(x => x.Cart.CustomerId == id).ToList();
            ViewBag.FoodCarts = foodCarts.Count();

            List<FoodCart> foodDelivered = db.FoodCarts.Where(x => x.Cart.CustomerId == id && x.PaidAmount != null).ToList();
            ViewBag.FoodDelivered = foodDelivered.Count();



            List<Review> reviewstoday = new List<Review>();
            foreach (var item in db.Reviews.Where(x => x.CustomerId == id))
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
            foreach (var item in db.ReviewFoods.Where(x => x.CustomerId == id))
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
            foreach (var item1 in db.FoodCarts.Where(x => x.Cart.CustomerId == id))
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
            foreach (var item in db.FoodCarts.Where(x => x.Cart.CustomerId == id && x.PaidAmount != null))
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

        public ActionResult Details(int? id)
        {
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                id = Convert.ToInt32(Session["CustomerId"]);
                return View(db.Customers.Where(x => x.CustomerId == id).FirstOrDefault());
            }
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var edit = db.Customers.Find(id);
            if (edit == null)
            {
                return HttpNotFound();
            }
            return View(edit);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "")] Customer customer)
        {
            return View();
        }

        public ActionResult CustomerOrderList(int? id,int? page)
        {
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            id = Convert.ToInt32(Session["CustomerId"]);
            return View(db.Carts.Where(x=> x.CustomerId == id).ToList().ToPagedList(page ??1,5));

        }


        [HttpGet]
        public ActionResult OrderListDetails(int? id, int? page)
        {
            if (Session["CustomerId"] == null)
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


        public ActionResult CustomerReviewList(int? id, int? page)
        {
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            id = Convert.ToInt32(Session["CustomerId"]);

            return View(db.Reviews.Where(x => x.CustomerId == id).ToList().ToPagedList(page ?? 1, 5));

        }
        public ActionResult CustomerFoodReviewList(int? id, int? page)
        {
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            id = Convert.ToInt32(Session["CustomerId"]);

            return View(db.ReviewFoods.Where(x => x.CustomerId == id).ToList().ToPagedList(page ?? 1, 5));

        }

        public ActionResult OrderDelete()
        {


            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpGet]
        public ActionResult ReviewDelete(int? id)
        {
            if (Session["CustomerId"] == null)
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
            return RedirectToAction("CustomerReviewList");
        }
        [HttpGet]
        public ActionResult CustomerFoodReviewDelete(int? id)
        {
            if (Session["CustomerId"] == null)
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
        public ActionResult CustomerFoodReviewDelete(int id)
        {
            ReviewFood reviewFood = db.ReviewFoods.Find(id);
            db.ReviewFoods.Remove(reviewFood);
            db.SaveChanges();
            return RedirectToAction("CustomerFoodReviewList");
        }


    }
}