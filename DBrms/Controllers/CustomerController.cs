using System;
using System.Collections.Generic;
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
        public ActionResult Index()
        {
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }

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

        public ActionResult OrderListDetails(int? id, int? page)
        {
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var foodlist = db.FoodCarts.Where(x => x.CartId == id).ToList().ToPagedList(page ?? 1, 5);
            return View(foodlist);
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