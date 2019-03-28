using DBrms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Data.Entity;

namespace DBrms.Controllers
{
    public class AdminController : Controller
    {
        dbrmsEntities db = new dbrmsEntities();

        public ActionResult Index()
        {
            return View();
        }
         
        public ActionResult Slider()
        {
           

            return View(db.Sliders.ToList());
        }

        [HttpGet]
        public ActionResult SliderAdd()
        {
          

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SliderAdd([Bind(Include = "Name,Image,Details,IsActive")] Slider slider , HttpPostedFileBase ImageFile)

        {
            
                String filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                String extension = Path.GetExtension(ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                slider.Image = "/Image/" + filename;
                

               
            if (ModelState.IsValid)
            {
                 ImageFile.SaveAs(filename);
                db.Sliders.Add(slider);
                db.SaveChanges();
                filename = Path.Combine(Server.MapPath("/Image/"), filename);
                ModelState.Clear();
                return RedirectToAction("Slider");

            }
            return View();
        }




        public ActionResult CategoryBuffet()
        {
            

            return View();
        }
        public ActionResult Newspanel()
        {
            

            return View(db.Magazines.ToList());
        }


        //[HttpGet]
        //public ActionResult NewspanelAdd()
        //{
           

        //    return View();
        //}
        //[HttpPost]
        //public ActionResult NewspanelAdd([Bind(Include = "Name,Image,Details,IsActive")] Newspanel newspanel , HttpPostedFileBase ImageFile )
        //{
        //    if (ImageFile != null)
        //    {
        //        String filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
        //        String extension = Path.GetExtension(ImageFile.FileName);
        //        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
        //        newspanel.Image = "/Image/" + filename;
        //        filename = Path.Combine(Server.MapPath("/Image/"), filename);
        //        ImageFile.SaveAs(filename);

        //        db.Newspanels.Add(newspanel);
        //        db.SaveChanges();
        //        ModelState.Clear();
        //        return RedirectToAction("Newspanel");

        //    }

        //    return View();
        //}


        [HttpGet]
        public ActionResult NewspanelEdit(int? id)
        {
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
        public ActionResult NewspanelEdit([Bind(Include ="MagazineId,Name,Image,Details,IsActive")] Magazine newspanel)
        {
            if (ModelState.IsValid)
            {

                db.Entry(newspanel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Newspanel");
            }
            return View();
        }



        public ActionResult TradingRestaurant()
        {


            return View(db.Restaurants.ToList());
        }


        [HttpGet]
        public ActionResult TradingRestaurantEdit(int ? id)
        {
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
        public ActionResult TradingRestaurantEdit([Bind(Include = "RestaurantId,Name,Address,Phone,Picture,Location,PopularMenu,CostPerOrder,Time,Cuisine,Extra,Discount,Username,Password,IsActive,Visible")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {

                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TradingRestaurant");
            }
            return View();
        }



        //[HttpGet]
        //public ActionResult TradingRestaurantAdd()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult TradingRestaurantAdd([Bind(Include ="Name,Image,IsActive")] TradingRestaurant tradingRestaurant, HttpPostedFileBase Imagefile)
        //{
        //    if(Imagefile != null)
        //    {
        //        String filename = Path.GetFileNameWithoutExtension(Imagefile.FileName);
        //        String extension = Path.GetExtension(Imagefile.FileName);
        //        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
        //        tradingRestaurant.Image = "/Image/" + filename;
        //        filename = Path.Combine(Server.MapPath("/Image/"), filename);
        //        Imagefile.SaveAs(filename);

        //        db.TradingRestaurants.Add(tradingRestaurant);
        //        db.SaveChanges();
        //        ModelState.Clear();
        //        return RedirectToAction("TradingRestaurant");

        //    }
        //    return View();
        //}
        public ActionResult Review()
        {


            return View(db.Reviews.ToList());
        }

        public ActionResult Magazine()
        {
            return View(db.Magazines.ToList());
        }

        [HttpGet]
        public ActionResult MagazineAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MagazineAdd ([Bind(Include ="Name,Image,Details")] Magazine magazine, HttpPostedFileBase ImageFile )
        {
            if(ImageFile != null)
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

        public ActionResult ManageRsetaurant()
        {

            return View(db.Restaurants.ToList());
        }


        [HttpGet]
        public ActionResult RestaurantAdd()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Name"); ;

            return View();
        }
        [HttpPost]
        public ActionResult RestaurantAdd([Bind(Include = "Name,Address,Phone,picture,LocationId,CostPerOrder,Cuisine")] Restaurant restaurant , HttpPostedFileBase ImageFile , int? LocationId)
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
                return RedirectToAction("ManageRsetaurant");
            }
            return View();
        }


        [HttpGet]
        public ActionResult ManageRestaurantEdit(int? id)
        {
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
        public ActionResult ManageRestaurantEdit([Bind(Include = "RestaurantId,Name,Address,Phone,Picture,Location,PopularMenu,CostPerOrder,Time,Cuisine,Extra,Discount,Username,Password,IsActive,Visible")] Restaurant restaurant)
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
    }
}