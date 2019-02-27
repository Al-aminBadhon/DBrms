using DBrms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace DBrms.Controllers
{
    public class AdminController : Controller
    {
        dbrmsEntities1 db = new dbrmsEntities1();

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
        public ActionResult SliderAdd([Bind(Include = "Name,Image,Details,IsActive")] Slider slider , HttpPostedFileBase ImageFile)

        {
            if (ImageFile != null )
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




        public ActionResult CategoryBuffet()
        {
            

            return View();
        }
        public ActionResult Newspanel()
        {
            

            return View(db.Newspanels.ToList());
        }


        [HttpGet]
        public ActionResult NewspanelAdd()
        {
           

            return View();
        }
        [HttpPost]
        public ActionResult NewspanelAdd([Bind(Include = "Name,Image,Details,IsActive")] Newspanel newspanel , HttpPostedFileBase ImageFile )
        {
            if (ImageFile != null)
            {
                String filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                String extension = Path.GetExtension(ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                newspanel.Image = "/Image/" + filename;
                filename = Path.Combine(Server.MapPath("/Image/"), filename);
                ImageFile.SaveAs(filename);

                db.Newspanels.Add(newspanel);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Newspanel");

            }

            return View();
        }

        public ActionResult TradingRestaurant()
        {


            return View(db.TradingRestaurants.ToList());
        }

        [HttpGet]
        public ActionResult TradingRestaurantAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TradingRestaurantAdd([Bind(Include ="Name,Image,IsActive")] TradingRestaurant tradingRestaurant, HttpPostedFileBase Imagefile)
        {
            if(Imagefile != null)
            {
                String filename = Path.GetFileNameWithoutExtension(Imagefile.FileName);
                String extension = Path.GetExtension(Imagefile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                tradingRestaurant.Image = "/Image/" + filename;
                filename = Path.Combine(Server.MapPath("/Image/"), filename);
                Imagefile.SaveAs(filename);

                db.TradingRestaurants.Add(tradingRestaurant);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("TradingRestaurant");

            }
            return View();
        }
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

            return View();
        }
        [HttpPost]
        public ActionResult RestaurantAdd([Bind(Include = "Name,Address,Phone,picture,Location,CostPerFood,Cuisine")] Restaurant restaurant , HttpPostedFileBase ImageFile )
        {
            if (ImageFile != null)
            {
                String fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                String extension = Path.GetExtension(ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                restaurant.picture = "/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("/Image/"), fileName);
                ImageFile.SaveAs(fileName);

                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Restaurants");
            }
            return View();
        }
    }
}