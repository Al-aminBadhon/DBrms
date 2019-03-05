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
        dbrmsEntities db = new dbrmsEntities();

        // GET: Restaurants
        public ActionResult Index()
        {
            var restaurants = db.Restaurants.Include(r => r.FoodOrder);
            return View(restaurants.ToList());
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

        //public ActionResult Details(int? id)
        //{
        //    id = Convert.ToInt32(Session["RestaurantsId"]);
        //    return View(db.Restaurants.Where(x => x.Username == username && x.Password == password).FirstOrDefault());
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
