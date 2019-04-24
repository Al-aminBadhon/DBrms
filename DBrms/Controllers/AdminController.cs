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
            if(Session["UserId"] == null)
            {
                return RedirectToAction("Index","Login");
            }
            return View();
        }

        public ActionResult Slider(int? page)
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
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


            String filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            String extension = Path.GetExtension(ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            slider.Image = "/Image/" + filename;
            filename = Path.Combine(Server.MapPath("/Image/"), filename);
            ImageFile.SaveAs(filename);
            
            
            if (ModelState.IsValid)
            {
              
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

            if(edit == null)
            {

                return HttpNotFound();
            }

                return View(edit);
        }

        [HttpPost]
        public ActionResult SliderEdit([Bind(Include = "SliderId,Name,Details,IsActive")] Slider slider, HttpPostedFileBase ImageFile)
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
        public ActionResult Newspanel(int? page)
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
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



        public ActionResult TradingRestaurant(int? page)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View(db.Restaurants.ToList().ToPagedList(page ?? 1, 3));
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



        public ActionResult Review(int? page)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
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

       
        public ActionResult TopReview(int? page)
        {
            return View(db.Reviews.ToList().ToPagedList(page ?? 1,5));
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



        public ActionResult Magazine(int? page)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Magazines.ToList().ToPagedList(page ?? 1, 3));
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
        public ActionResult MagazineAdd([Bind(Include = "Name,Image,Details")] Magazine magazine, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null)
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
            return View();
        }

        public ActionResult ManageRestaurant(int? page, string search)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (search != null)
            {
                return View(db.Restaurants.Where(x => x.Name.StartsWith(search)).ToList().ToPagedList(page ?? 1, 5));
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
            return RedirectToAction("ManageRsetaurant");
        }



        [HttpGet]

        public ActionResult RestaurantAdd()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RestaurantAdd([Bind(Include = "RestaurantId,Name,Address,Phone,picture,LocationId,CostPerOrder,Cuisine,Username,Password")] Restaurant restaurant, HttpPostedFileBase ImageFile, int? LocationId)
        {
            //int er = 0;
            //if (LocationId == null)
            //{
            //    er++;

            //}
            //if (er > 0)
            //{
            //    return View("Index");
            //}
            String fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            String extension = Path.GetExtension(ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            restaurant.Picture = "/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("/Image/"), fileName);
            ImageFile.SaveAs(fileName);

            if (ModelState.IsValid)
            {
                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("ManageRsetaurant");

            }
            return View();
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
        public ActionResult ManageRestaurantEdit([Bind(Include = "Name,Address,Phone,Picture,LocationId,PopularMenu,CostPerOrder,Time,Cuisine,Extra,Discount,Username,Password,IsActive")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                //Restaurant res = db.Restaurants.FirstOrDefault(x=> x.RestaurantId == restaurant.RestaurantId);
                //res.IsActive = restaurant.IsActive;
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageRestaurant");
            }
            return View();
        }

        public ActionResult ManageCustomer(int? page)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Customers.ToList().ToPagedList(page ?? 1, 3));
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
        public ActionResult CustomerAdd([Bind(Include = "Name,Address,Image,Phone,Username,Password")] Customer customer, HttpPostedFileBase ImageFile)
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
                return RedirectToAction("ManageCustomer");
            }
            return View();
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
        public ActionResult CustomerEdit([Bind(Include = "CustomerId,Name,Address,Image,Phone,Username,Password")] Customer customer, HttpPostedFileBase ImageFile)
        {
            String fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            String extension = Path.GetExtension(ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            customer.Image = "/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("/Image/"), fileName);
            ImageFile.SaveAs(fileName);

            if (ModelState.IsValid)
            {
                //Restaurant res = db.Restaurants.FirstOrDefault(x=> x.RestaurantId == restaurant.RestaurantId);
                //res.IsActive = restaurant.IsActive;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageCustomer");
            }
            return View();
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
            Restaurant restaurant = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurant);
            db.SaveChanges();
            return RedirectToAction("ManageCustomer");
        }

        public ActionResult OrderList(int? page)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View(db.Carts.ToList().ToPagedList(page ?? 1,5));
        }

        public ActionResult OrderListDetails(int? id, int? page)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var foodlist = db.FoodCarts.Where(x => x.CartId == id).ToList().ToPagedList(page ?? 1, 5);
            return View(foodlist);
        }
       
    }
}