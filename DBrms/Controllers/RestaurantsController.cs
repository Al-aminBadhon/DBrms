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

namespace DBrms.Controllers
{
    public class RestaurantsController : Controller
    {
        private dbrmsEntities1 db = new dbrmsEntities1();

        // GET: Restaurants
        public ActionResult Index()
        {
            var restaurants = db.Restaurants.Include(r => r.FoodOrder);
            return View(restaurants.ToList());
        }

        // GET: Restaurants/Details/5
        public ActionResult Details(int? id)
        {
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

        // GET: Restaurants/Create
        public ActionResult Create()
        {
            ViewBag.RestaurantId = new SelectList(db.FoodOrders, "FoodOrderId", "FoodOrderId");
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RestaurantId,Name,Address,Phone,picture,Location,PopularMenu,CostPerOrder,Time,Cuisine,Extra,Discount,RatingUser,RatingNum")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RestaurantId = new SelectList(db.FoodOrders, "FoodOrderId", "FoodOrderId", restaurant.RestaurantId);
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            ViewBag.RestaurantId = new SelectList(db.FoodOrders, "FoodOrderId", "FoodOrderId", restaurant.RestaurantId);
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RestaurantId,Name,Address,Phone,picture,Location,PopularMenu,CostPerOrder,Time,Cuisine,Extra,Discount,RatingUser,RatingNum")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RestaurantId = new SelectList(db.FoodOrders, "FoodOrderId", "FoodOrderId", restaurant.RestaurantId);
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public ActionResult Delete(int? id)
        {
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

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Menu ()
        {
            return View(db.Foods.ToList());
        }

        [HttpGet]
        public ActionResult MenuAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MenuAdd([Bind(Include ="RestaurantId,Name,Image,Price,Details")] Food food , HttpPostedFileBase ImageFile)
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
                return RedirectToAction("Menu");

            }
            return View();
        }

        public ActionResult PopularMenu()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PopularMenuAdd()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult PopularMenuAdd([Bind(Include = "Name,Image,Price,Details")] Food food, HttpPostedFileBase ImageFile)
        //{
        //    return View();
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
