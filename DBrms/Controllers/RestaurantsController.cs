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
            id = Convert.ToInt32(Session["RestaurantsId"]);

            List<Restaurant> restaurants = db.Restaurants.Where(x=> x.RestaurantId ==id).ToList();
            ViewBag.Restaurants = restaurants;

            return View(db.Restaurants.ToList());
        }

      
        
        public ActionResult Menu (int? id, int ? page, string search)
        {
            id = Convert.ToInt32(Session["RestaurantsId"]);
            List<Restaurant> restaurants = db.Restaurants.Where(X => X.RestaurantId == id).ToList();
            ViewBag.Restaurants = restaurants;

            if (search != null)
            {
                return View(db.Foods.Where(x=> x.Name.StartsWith(search)).ToList().ToPagedList(page ?? 1,3));
            }
           


            return View(db.Foods.Where(x => x.RestaurantId == id).ToList().ToPagedList(page ?? 1,3));

            
        }

        [HttpGet]
        public ActionResult MenuAdd()
        {
           int id = Convert.ToInt32(Session["RestaurantsId"]);
            return View();
        }
        [HttpPost]
        public ActionResult MenuAdd([Bind(Include ="Name,Image,Price,Details")] Food food , HttpPostedFileBase ImageFile)
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
        public ActionResult MenuEdit (int? id)
        {
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
            id = Convert.ToInt32(Session["RestaurantsId"]);
            
            return View(db.Restaurants.Where(x => x.RestaurantId == id).FirstOrDefault());
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Restaurant edit = db.Restaurants.Find(id);
            if(edit == null)
            {
                return HttpNotFound();
            }
            return View(edit);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "RestaurantId,Name,Address,Phone,Picture,Location,PopularMenu,CostPerOrder,Time,Cuisine,Extra,Discount,UserName,Password")] Restaurant restaurant, HttpPostedFileBase ImageFile)
            {
            //String filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            //String extension = Path.GetExtension(ImageFile.FileName);
            //filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            //restaurant.Picture = "/Image/" + filename;
            //filename = Path.Combine(Server.MapPath("/Image/"), filename);
            //ImageFile.SaveAs(filename);
            if (ModelState.IsValid)
            {
                
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            return View();
        }




    }
}
