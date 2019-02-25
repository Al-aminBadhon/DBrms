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
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Newspanel()
        {
            

            return View();
        }


        [HttpGet]
        public ActionResult NewspanelAdd()
        {
           

            return View();
        }
        //[HttpPost]
        ////public ActionResult NewspanelAdd([Bind(Include = "")])
        ////{


        ////    return View();
        ////}
        ///
        public ActionResult TradingRestaurant()
        {


            return View();
        }

        public ActionResult Review()
        {


            return View();
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
                magazine.Image = "/Image/" + ImageFile;
                fileName = Path.Combine(Server.MapPath("/Image/"), fileName);
                ImageFile.SaveAs(fileName);

                db.Magazines.Add(magazine);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Magazine");
            }
            return View();
        }
    }
}